using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormLists;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormIndividual
{
    public partial class FournisseurIndiv : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = string.Empty;
        private string tabID = string.Empty;

        // ID et données de l'article
        private int IDSupplier;
        private Supplier fournisseur;

        // Données parallèles
        private List<SupplierName> listeNomFournisseurs;

        #endregion

        #region Public Variables

        public string FormTitle
        {
            get { return formTitle; }
            set
            {
                formTitle = value;
                lbFormTitle.Text = value;
            }
        }
        public string TabID { get => tabID; set => tabID = value; }

        #endregion

        #region Constructor

        public FournisseurIndiv(int IDFournisseur)
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            this.IDSupplier = IDFournisseur;
            fournisseur = new Supplier();

            // Récupération des fournisseurs
            listeNomFournisseurs = ApiUtils.GetSuppliersName();

            InitializeItemData();
        }

        #region Constructor Methods

        private void GetPageInfo()
        {
            Supplier? queryResult = GetItemData();

            if (queryResult is null)
            {
                CustomMethods.DisplayError("Erreur, aucun fournisseur trouvé.");
            }
            else
            {
                fournisseur = queryResult;
            }
        }

        private void InitializeItemData()
        {
            GetPageInfo();

            if (fournisseur != null)
            {
                FormTitle = string.Format("Fournisseur {0}", fournisseur.Name);
                tabID = "Fournisseur" + IDSupplier;
                WritePageInfo();
            }
            else
            {
                CustomMethods.DisplayError("Page inconnue !");
            }
        }

        private void WritePageInfo()
        {
            txtID.Text = fournisseur.IDSupplier.ToString();
            txtNom.Text = fournisseur.Name;
            txtEmail.Text = fournisseur.Email;
            txtAdresse.Text = fournisseur.Address;
            txtCodePostal.Text = fournisseur.PostalCode;
            txtVille.Text = fournisseur.Town;
            txtPays.Text = fournisseur.Country;
        }

        #endregion

        #endregion

        #region Events

        private void SavePage(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmDataSave())
            {
                return;
            }

            if (!IsDataValid())
            {
                return;
            }

            UpdateValues();

            if (UpdateSupplier())
            {
                CustomMethods.DisplayInformation("Le fournisseur " + fournisseur.Name + " a été modifié avec succès !");
                listeNomFournisseurs = ApiUtils.GetSuppliersName();
            }
            else
            {
                CustomMethods.DisplayError("Erreur lors de l'enregistrement des données. Vérifiez la connexion avec l'API.");
            }
        }

        private void ReloadPage(object sender, EventArgs e)
        {
            if (CustomMethods.ConfirmDataReload())
            {
                InitializeItemData();
                RemoveErrors();
            }
        }

        private void ListeArticlesLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int idFournisseur = IDSupplier;
            string? fournisseurName = fournisseur.Name;

            if (fournisseurName is null)
            {
                CustomMethods.DisplayError("Fournisseur non-défini");
                return;
            }

            if (this.TopLevelControl is not NegoSUD negoSUD)
            {
                CustomMethods.DisplayError("Page principale inaccessible.");
                return;
            }

            negoSUD.AddTabIfNotExists(new Articles(idFournisseur, fournisseurName));
        }

        #endregion

        #region Save Data Methods

        private bool IsDataValid()
        {
            List<Control> controlsToCheck;

            // Suppression des erreurs affichées
            RemoveErrors();

            // Sélection des champs textbox et combobox de la partie gauche
            controlsToCheck = this.pnlLeftData.Controls.Cast<Control>().Where(c => c is TextBox).ToList();

            // Contrôle des données
            if (!CheckControlsValue(controlsToCheck))
            {
                return false;
            }

            if (FournisseurNameExists())
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Ce nom est déjà attribué à un autre fournisseur, êtes-vous sûr de vouloir changer le nom ?",
                    "Fournisseur existant", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dialogResult != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckControlsValue(List<Control> controlsToCheck)
        {
            bool dataValidity = true;
            foreach (Control control in controlsToCheck)
            {
                // Contrôle des textbox
                if (control is TextBox && string.IsNullOrEmpty(control.Text))
                {
                    this.errorProvider.SetError(control, "La valeur du champ n'est pas valide.");
                    dataValidity = false;
                }
            }

            return dataValidity;
        }

        /// <summary>
        /// Enregistre les valeurs dans l'objet
        /// </summary>
        private void UpdateValues()
        {
            fournisseur.Name = txtNom.Text;
            fournisseur.Email = txtEmail.Text;
            fournisseur.Address = txtAdresse.Text;
            fournisseur.PostalCode = txtCodePostal.Text;
            fournisseur.Town = txtVille.Text;
            fournisseur.Country = txtPays.Text;
        }

        /// <summary>
        /// Détecte si le nom rentré existe déjà
        /// </summary>
        /// <returns></returns>
        private bool FournisseurNameExists()
        {
            if (string.IsNullOrEmpty(txtNom.Text)
                || txtNom.Text == fournisseur.Name)
            {
                return false;
            }

            int namesCount = listeNomFournisseurs.Where(f => f.Name == txtNom.Text).Count();

            // Si il existe déjà un nom similaire, return true;
            if (namesCount > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Other Methods

        private void RemoveErrors()
        {
            errorProvider.Clear();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        #endregion

        #region Request Methods

        private Supplier? GetItemData()
        {
            Supplier? requestedObject = null;
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Supplier/" + IDSupplier);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<Supplier>(json) is Supplier requestResult)
                    {
                        requestedObject = requestResult;
                    }
                }
            }).Wait();

            return requestedObject;
        }

        private bool UpdateSupplier()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(fournisseur);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("Supplier/" + IDSupplier, content);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        #endregion
    }
}
