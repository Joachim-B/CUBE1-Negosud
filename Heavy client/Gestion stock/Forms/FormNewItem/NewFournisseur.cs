using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormNewItem
{
    public partial class NewFournisseur : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = "Nouveau fournisseur";
        private string tabID = "Fournisseur";

        // ID et données de l'article
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

        public NewFournisseur()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            fournisseur = new Supplier();

            // Récupération des fournisseurs
            listeNomFournisseurs = ApiUtils.GetSuppliersName();

            ResetPageInfo();
        }

        #region Constructor Methods

        private void ResetPageInfo()
        {
            txtNom.Text = null;
            txtEmail.Text = null;
            txtAdresse.Text = null;
            txtCodePostal.Text = null;
            txtVille.Text = null;
            txtPays.Text = null;
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

            if (CreateNewSupplier())
            {
                ResetPageInfo();
                CustomMethods.DisplayInformation("Un nouveau fournisseur a été créé avec succès !");
                listeNomFournisseurs = ApiUtils.GetSuppliersName();
            }
            else
            {
                CustomMethods.DisplayError("Erreur lors de la création d'un nouveau fournisseur. Vérifiez la connexion avec l'API.");
            }
        }

        private void ReloadPage(object sender, EventArgs e)
        {
            if (CustomMethods.ConfirmDataReload())
            {
                ResetPageInfo();
                RemoveErrors();
            }
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
                    "Ce nom de fournisseur existe déjà, êtes-vous sûr de vouloir créer un fournisseur portant le même nom ?",
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
            if (string.IsNullOrEmpty(txtNom.Text))
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

        #region Requests Methods

        private bool CreateNewSupplier()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(fournisseur);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("Supplier", content);

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
