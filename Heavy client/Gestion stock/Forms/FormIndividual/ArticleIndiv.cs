using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormLists;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormIndividual
{
    public partial class ArticleIndiv : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = string.Empty;
        private string tabID = string.Empty;

        // ID et données de l'article
        private string IDArticle;
        private Article article;

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

        public ArticleIndiv(string IDArticle)
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            this.IDArticle = IDArticle;
            article = new Article();

            // Récupération des familles de vin
            listeFamillesDeVin = ApiUtils.GetWineFamiliesName();

            // Récupération des fournisseurs
            listeNomFournisseurs = ApiUtils.GetSuppliersName();

            // Remplissage des données des combobox
            FillComboBoxData();

            InitializeItemData();

            AddEvents();
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

        private void InitializeItemData()
        {
            GetPageInfo();

            if (article != null)
            {
                FormTitle = string.IsNullOrEmpty(article.Name) ? "" : article.Name;
                tabID = "Article" + IDArticle;
                WritePageInfo();
            }
            else
            {
                CustomMethods.DisplayError("Page inconnue !");
            }
        }

        private void GetPageInfo()
        {
            Article? queryResult = GetItemData();

            if (queryResult is null)
            {
                CustomMethods.DisplayError("Erreur, aucun article trouvé.");
            }
            else
            {
                article = queryResult;
            }
        }

        private void WritePageInfo()
        {
            txtReference.Text = article.Reference;
            txtNom.Text = article.Name;
            txtFournisseur.Text = GetFournisseurName();
            txtFamille.Text = GetFamilleDeVinName();
            txtAnnee.Text = article.WineYear.ToString();
            txtLienImage.Text = article.ImageLink;
            txtDescription.Text = article.Description;
            txtPrixTTC.Text = article.BoxPriceTTC.ToString();
            txtPrixTTCUnite.Text = article.UnitPriceTTC.ToString();
            txtPrixAchat.Text = article.BoxBuyingPrice.ToString();
            txtTVA.Text = article.TVA.ToString();
            txtQuantiteCarton.Text = article.BoxStockQuantity.ToString();
            txtQuantiteUnite.Text = article.UnitStockQuantity.ToString();
            txtQuantiteMin.Text = article.BoxMinQuantity.ToString();
            txtQuantiteOptimale.Text = article.BoxOptimalQuantity.ToString();
            txtBouteillesParCarton.Text = article.BottleQuantityPerBox.ToString();
        }

        /// <summary>
        /// Ajout des évènements custom de la page
        /// </summary>
        private void AddEvents()
        {
            // Evènements pour filter les textbox de chiffres
            this.txtAnnee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
            this.txtPrixTTC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterDecimals);
            this.txtPrixTTCUnite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CustomEvents.FilterIntegers);
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

            if (UpdateArticle())
            {
                CustomMethods.DisplayInformation("L'article \"" + article.Name + "\" a été modifié avec succès !");
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

        private void FournisseurLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int fournisseurIndex = txtFournisseur.SelectedIndex;
            if (fournisseurIndex < 0)
            {
                CustomMethods.DisplayError("Fournisseur non-défini");
                return;
            }
            int idFournisseur = listeNomFournisseurs[fournisseurIndex].IDSupplier;

            if (this.TopLevelControl is not NegoSUD negoSUD)
            {
                CustomMethods.DisplayError("Page principale inaccessible.");
                return;
            }

            negoSUD.AddTabIfNotExists(new FournisseurIndiv(idFournisseur));
        }

        #endregion

        #region Save Data Methods

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
            article.Reference = txtReference.Text;
            article.Name = txtNom.Text;
            article.IDWineFamily = GetFournisseurID();
            article.IDSupplier = GetFamilleDeVinID();
            article.WineYear = Convert.ToInt32(txtAnnee.Text);
            article.Description = txtDescription.Text;
            article.ImageLink = txtLienImage.Text;
            article.BoxPriceTTC = Convert.ToDecimal(txtPrixTTC.Text);
            article.UnitPriceTTC = Convert.ToDecimal(txtPrixTTCUnite.Text);
            article.BoxBuyingPrice = Convert.ToDecimal(txtPrixAchat.Text);
            article.TVA = Convert.ToDecimal(txtTVA.Text);
            article.BoxStockQuantity = Convert.ToInt32(txtQuantiteCarton.Text);
            article.BottleQuantityPerBox = Convert.ToInt32(txtBouteillesParCarton.Text);
            article.BoxMinQuantity = Convert.ToInt32(txtQuantiteMin.Text);
            article.BoxOptimalQuantity = Convert.ToInt32(txtQuantiteOptimale.Text);
        }

        #endregion

        #region Foreign Keys Methods

        private string? GetFournisseurName()
        {
            if (article == null)
            {
                return null;
            }

            SupplierName fournisseur = listeNomFournisseurs.Where(f => f.IDSupplier == article.IDSupplier).First();

            return fournisseur.Name;
        }

        private string? GetFamilleDeVinName()
        {
            if (article == null)
            {
                return null;
            }

            WineFamily familleDeVin = listeFamillesDeVin.Where(f => f.IDWineFamily == article.IDWineFamily).First();

            return familleDeVin.Name;
        }

        private int GetFournisseurID()
        {
            // On trouve l'index du combobox sélectionné
            int comboBoxIndex = txtFournisseur.SelectedIndex;

            // Si l'index = -1, alors erreur
            if (comboBoxIndex < 0)
            {
                CustomMethods.DisplayError("ID du fournisseur introuvable");
                return -1;
            }

            // Sélection de l'id dans la liste des fournisseur. La liste des fournisseurs
            // est rangée dans le même ordre que la liste du combobox
            int id = listeNomFournisseurs[comboBoxIndex].IDSupplier;

            // Si l'id est nul, erreur
            return id;
        }

        private int GetFamilleDeVinID()
        {
            int comboBoxIndex = txtFamille.SelectedIndex;

            if (comboBoxIndex < 0)
            {
                CustomMethods.DisplayError("ID de la famille de vin introuvable");
                return -1;
            }

            int id = listeFamillesDeVin[comboBoxIndex].IDWineFamily;

            return id;
        }

        #endregion

        #region Other Methods

        private void RemoveErrors()
        {
            errorProvider.Clear();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        #endregion

        #region Requests

        private Article? GetItemData()
        {
            Article? requestedObject = null;
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Article/" + IDArticle);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<Article>(json) is Article requestResult)
                    {
                        requestedObject = requestResult;
                    }
                }
            }).Wait();

            return requestedObject;
        }

        private bool UpdateArticle()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(article);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("Article/" + IDArticle, content);

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
