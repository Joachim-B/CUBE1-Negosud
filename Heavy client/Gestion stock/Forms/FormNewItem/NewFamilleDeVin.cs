using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormLists;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace Gestion_stock.Forms.FormNewItem
{
    public partial class NewFamilleDeVin : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = "Nouvelle famille de vin";
        private string tabID = "NewFamilleDeVin";

        // Données de l'item
        private WineFamily newFamilleDeVin;

        // Données parallèles
        private List<WineFamily> listeFamillesDeVin;

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

        public NewFamilleDeVin()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            newFamilleDeVin = new WineFamily();

            // Récupération des familles de vin
            listeFamillesDeVin = ApiUtils.GetWineFamiliesName();

            ResetWineFamily();
        }

        #region Constructor Methods

        private void ResetWineFamily()
        {
            txtNom.Text = null;
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
            
            if (CreateNewWineFamily())
            {
                ResetWineFamily();
                CustomMethods.DisplayInformation("Une nouvelle famille de vin a été créée avec succès !");
            }
            else
            {
                CustomMethods.DisplayError("Erreur lors de la création de la nouvelle famille de vin. Vérifiez la connexion avec l'API.");
            }
        }

        private void ReloadPage(object sender, EventArgs e)
        {
            if (CustomMethods.ConfirmDataReload())
            {
                ResetWineFamily();
                RemoveErrors();
            }
        }

        #endregion

        #region Save Data Methods

        /// <summary>
        /// Contrôle les données de la page et met en erreur les champs non-valides
        /// </summary>
        /// <returns></returns>
        private bool IsDataValid()
        {
            RemoveErrors();

            // Contrôle des textbox
            if (string.IsNullOrEmpty(txtNom.Text))
            {
                this.errorProvider.SetError(txtNom, "La valeur du champ n'est pas valide.");
                return false;
            }

            if (FamilleDeVinNameExists())
            {
                this.errorProvider.SetError(txtNom, "Ce nom est déjà utilisé.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Enregistre les valeurs dans l'objet
        /// </summary>
        private void UpdateValues()
        {
            newFamilleDeVin.Name = txtNom.Text;
        }

        /// <summary>
        /// Détecte si le nom rentré existe déjà
        /// </summary>
        /// <returns></returns>
        private bool FamilleDeVinNameExists()
        {
            if (string.IsNullOrEmpty(txtNom.Text))
            {
                return false;
            }

            int namesCount = listeFamillesDeVin.Where(f => f.Name == txtNom.Text).Count();

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

        private bool CreateNewWineFamily()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(newFamilleDeVin);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("WineFamily", content);

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
