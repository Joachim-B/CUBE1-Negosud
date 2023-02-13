using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormLists;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormNewItem
{
    public partial class NewArticle : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = "Nouvel article";
        private string tabID = "NewArticle";

        // Données de l'item
        private Article newArticle;

        // Données parallèles
        private List<SupplierName> listeNomFournisseurs;
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

        public NewArticle()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            newArticle = new Article();

            // Récupération des familles de vin
            listeFamillesDeVin = ApiUtils.GetWineFamiliesName();

            // Récupération des fournisseurs
            listeNomFournisseurs = ApiUtils.GetSuppliersName();

            // Remplissage des données des combobox
            FillComboBoxData();

            AddEvents();
            ResetPageInfo();
        }

        #region Constructor Methods

        private void FillComboBoxData()
        {
            foreach (WineFamily famille in listeFamillesDeVin)
            {
                txtFamille.Items.Add(famille.Name);
            }

            foreach (SupplierName fournisseur in listeNomFournisseurs)
            {
                txtFournisseur.Items.Add(fournisseur.Name);
            }
        }

        private void ResetPageInfo()
        {
            txtReference.Text = null;
            txtNom.Text = null;
            txtFournisseur.Text = null;
            txtFamille.Text = null;
            txtAnnee.Text = null;
            txtLienImage.Text = null;
            txtDescription.Text = null;
            txtPrixTTC.Text = null;
            txtPrixTTCUnite.Text = null;
            txtPrixAchat.Text = null;
            txtTVA.Text = null;
            txtQuantiteCarton.Text = null;
            txtQuantiteUnite.Text = null;
            txtQuantiteMin.Text = null;
            txtQuantiteOptimale.Text = null;
            txtBouteillesParCarton.Text = null;
        }

        /// <summary>
        /// Ajout des évènements custom de la page
        /// </summary>
        private void AddEvents()
        {
            // Evènements pour filter les textbox de chiffres
            this.txtAnnee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtPrixTTC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterDecimals);
            this.txtPrixTTCUnite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterDecimals);
            this.txtPrixAchat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterDecimals);
            this.txtTVA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterDecimals);
            this.txtQuantiteCarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtQuantiteUnite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtQuantiteMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtQuantiteOptimale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtBouteillesParCarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
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

            if (CreateNewArticle())
            {
                ResetPageInfo();
                CustomMethods.DisplayInformation($"L'article \"{newArticle.Name}\" a été créé avec succès !");
            }
            else
            {
                CustomMethods.DisplayError("Erreur lors de la création d'un nouvel article. Vérifiez la connexion avec l'API.");
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

        /// <summary>
        /// Contrôle les données de la page et met en erreur les champs non-valides
        /// </summary>
        /// <returns></returns>
        private bool IsDataValid()
        {
            List<Control> controlsToCheck;
            bool isDataValid = true;

            // Suppression des erreurs affichées
            RemoveErrors();

            // Sélection des champs textbox et combobox de la partie gauche
            controlsToCheck = this.pnlLeftData.Controls.Cast<Control>().Where(c => c is TextBox or ComboBox).ToList();

            // Suppression de la liste des champs qu'on ne veut pas contrôler
            controlsToCheck.Remove(this.txtReference);
            controlsToCheck.Remove(this.txtLienImage);

            // Contrôle des données
            if (!CheckControlsValue(controlsToCheck))
            {
                isDataValid = false;
            }

            // Sélection des champs de la partie droite
            controlsToCheck = this.pnlRightData.Controls.Cast<Control>().Where(c => c is TextBox).ToList();

            if (!CheckControlsValue(controlsToCheck))
            {
                isDataValid = false;
                // Changement de la largeur du panel pour pouvoir afficher le logo erreur
                this.pnlRightData.Width = 315;
            }
            else
            {
                this.pnlRightData.Width = 295;
            }

            return isDataValid;
        }

        /// <summary>
        /// Contrôle les données d'une liste de contrôles
        /// </summary>
        /// <param name="controlsToCheck"></param>
        /// <returns></returns>
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

                // Contrôle des combobox
                else if (control is ComboBox combobox && combobox.SelectedItem == null)
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
            newArticle.Reference = txtReference.Text;
            newArticle.Name = txtNom.Text;
            newArticle.IDSupplier = GetFournisseurID();
            newArticle.IDWineFamily = GetFamilleDeVinID();
            newArticle.WineYear = Convert.ToInt32(txtAnnee.Text);
            newArticle.Description = txtDescription.Text;
            newArticle.ImageLink = txtLienImage.Text;
            newArticle.BoxPriceTTC = Convert.ToDecimal(txtPrixTTC.Text);
            newArticle.UnitPriceTTC = Convert.ToDecimal(txtPrixTTCUnite.Text);
            newArticle.BoxBuyingPrice = Convert.ToDecimal(txtPrixAchat.Text);
            newArticle.TVA = Convert.ToDecimal(txtTVA.Text);
            newArticle.BoxStockQuantity = Convert.ToInt32(txtQuantiteCarton.Text);
            newArticle.UnitStockQuantity = Convert.ToInt32(txtQuantiteUnite.Text);
            newArticle.BoxMinQuantity = Convert.ToInt32(txtQuantiteMin.Text);
            newArticle.BoxOptimalQuantity = Convert.ToInt32(txtQuantiteOptimale.Text);
            newArticle.BottleQuantityPerBox = Convert.ToInt32(txtBouteillesParCarton.Text);
        }

        #endregion

        #region Foreign Keys Methods

        private int GetFournisseurID()
        {
            // On trouve l'index du combobox sélectionné
            int comboBoxIndex = txtFournisseur.SelectedIndex;

            // Si l'index = -1, alors erreur
            if (comboBoxIndex < 0)
            {
                return Error();
            }

            // Sélection de l'id dans la liste des fournisseur. La liste des fournisseurs
            // est rangée dans le même ordre que la liste du combobox
            int id = listeNomFournisseurs[comboBoxIndex].IDSupplier;

            // Si l'id est nul, erreur

            return id;

            static int Error()
            {
                CustomMethods.DisplayError("ID du fournisseur introuvable");
                return -1;
            }
        }

        private int GetFamilleDeVinID()
        {
            int comboBoxIndex = txtFamille.SelectedIndex;

            if (comboBoxIndex < 0)
            {
                return Error();
            }

            int id = listeFamillesDeVin[comboBoxIndex].IDWineFamily;

            return id;

            static int Error()
            {
                CustomMethods.DisplayError("ID de la famille de vin introuvable");
                return -1;
            }
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

        private bool CreateNewArticle()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(newArticle);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("Article", content);

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
