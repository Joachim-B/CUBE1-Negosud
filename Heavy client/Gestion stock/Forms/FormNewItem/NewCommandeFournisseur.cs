using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormIndividual;
using Gestion_stock.Forms.FormLists;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Windows.Forms;

namespace Gestion_stock.Forms.FormNewItem
{
    public partial class NewCommandeFournisseur : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = "Nouvelle commande";
        private string tabID = "NouvelleCommande";

        // ID et données de la commande
        private SupplierCommand newCommand;
        private int idFournisseur;

        // Autres données
        int selectedFournisseur = -1;
        int selectedArticle = -1;

        List<SupplierName> listeNomFournisseurs = ApiUtils.GetSuppliersName();
        List<Article>? everyArticleList = new List<Article>();
        List<Article> articlesSupplierList = new List<Article>();

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

        public NewCommandeFournisseur()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            newCommand = new SupplierCommand();

            FillComboBoxData();
            everyArticleList = GetArticles();
            AddEvents();
        }

        #region Constructor Methods

        private void FillComboBoxData()
        {
            foreach (SupplierName fournisseur in listeNomFournisseurs)
            {
                txtFournisseur.Items.Add(fournisseur.Name);
            }
        }

        private void AddEvents()
        {
            // Evènements pour filter les textbox de chiffres
            this.txtCoutTransport.KeyPress += new KeyPressEventHandler(CustomEvents.FilterDecimals);
        }

        #endregion

        #endregion

        #region Events

        #region Save / Reload

        private void SavePage(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmCommandCreation())
            {
                return;
            }

            if (!ControlData())
            {
                return;
            }

            // Ecriture des données
            newCommand = new SupplierCommand()
            {
                IDSupplier = idFournisseur,
                CommandDate = DateTime.UtcNow,
                IDCommandType = 1,
                IDCommandStatus = 2,
                TransportCost = Convert.ToDecimal(txtCoutTransport.Text),
                TotalCost = Convert.ToDecimal(txtCoutTransport.Text)
            };

            // Liste des articles commandés
            for (int i = 0; i < dgvPanier.Rows.Count; i++)
            {
                int.TryParse(dgvPanier.Rows[i].Cells["IDArticle"].Value.ToString(), out int idArticle);

                int.TryParse(dgvPanier.Rows[i].Cells["Quantite"].Value.ToString(), out int quantity);

                newCommand.SupplierCommandList.Add(new SupplierCommandList
                {
                    IDArticle = idArticle,
                    Quantity = quantity
                });
            }

            if (!CreateNewSupplierCommand())
            {
                CustomMethods.DisplayError("Erreur lors de la création d'une nouvelle commande. Vérifiez la connexion avec l'API.");
                return;
            }

            CustomMethods.DisplayInformation("Un nouveau fournisseur a été créé avec succès !");
            ReloadPage(this, EventArgs.Empty);
        }

        private void ReloadPage(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmDataReload())
            {
                return;
            }

            txtCoutTransport.Text = "0";
            txtFournisseur.SelectedIndex = -1;
            txtIDFournisseur.Text = null;
            dgvPanier.Rows.Clear();
            txtTotalTTC.Text = "0";
        }

        #endregion

        #region Changement fournisseur

        /// <summary>
        /// Evenement quand le nom du fournisseur a été changé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FournisseurChanged(object sender, EventArgs e)
        {
            if (dgvPanier.Rows.Count > 0 && txtFournisseur.SelectedIndex >= 0)
            {
                DialogResult alert = MessageBox.Show("Vous allez perdre toutes les données. Continuer ?", "Avertissement",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (alert == DialogResult.No)
                {
                    txtFournisseur.SelectedIndex = selectedFournisseur;
                    return;
                }
            }

            this.dgvPanier.Rows.Clear();
            selectedFournisseur = txtFournisseur.SelectedIndex;

            if (everyArticleList == null)
            {
                return;
            }

            if (txtFournisseur.SelectedIndex < 0)
            {
                idFournisseur = -1;
                this.pnlContainerBtnRows.Visible = false;
            }
            else
            {
                idFournisseur = listeNomFournisseurs[selectedFournisseur].IDSupplier;
                this.pnlContainerBtnRows.Visible = true;

                // Sélection des articles du fournisseur
                articlesSupplierList = everyArticleList.Where(r => r.IDSupplier == idFournisseur).ToList();

                // Remplissage des combobox du tableau
                if (dgvPanier.Columns["Reference"] is DataGridViewComboBoxColumn referenceColumn
                    && dgvPanier.Columns["Nom"] is DataGridViewComboBoxColumn nomColumn)
                {
                    referenceColumn.Items.Clear();
                    nomColumn.Items.Clear();
                    for (int i = 0; i < articlesSupplierList.Count; i++)
                    {
                        referenceColumn.Items.Add(articlesSupplierList[i].Reference);
                        nomColumn.Items.Add(articlesSupplierList[i].Name);
                    }
                }
            }

            txtIDFournisseur.Text = idFournisseur.ToString();
        }

        #endregion

        #region Grid Events

        private void ArticlesCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            selectedArticle = e.RowIndex;
        }

        /// <summary>
        /// Méthode qui se lance la première fois qu'on édite les valeurs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterEditingFirstTime(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPanier.CurrentCell.ColumnIndex == dgvPanier.Columns["Quantite"].Index
                && e.Control is TextBox tb)
            {
                string quantiteValue = tb.Text;
                tb.KeyPress -= new KeyPressEventHandler(CustomEvents.FilterIntegers);
                tb.KeyPress += new KeyPressEventHandler(CustomEvents.FilterIntegers);
            }
            else if ((dgvPanier.CurrentCell.ColumnIndex == dgvPanier.Columns["Reference"].Index
                || dgvPanier.CurrentCell.ColumnIndex == dgvPanier.Columns["Nom"].Index)
                && e.Control is ComboBox cb)
            {
                cb.SelectedIndexChanged -= new EventHandler(ArticleChanged);
                cb.SelectedIndexChanged += new EventHandler(ArticleChanged);
            }
        }

        private void ArticleChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb)
            {
                return;
            }

            int selectedReference = cb.SelectedIndex;

            DataGridViewCellCollection articleRow = dgvPanier.Rows[selectedArticle].Cells;

            articleRow["IDArticle"].Value = articlesSupplierList[selectedReference].IDArticle;
            articleRow["Reference"].Value = articlesSupplierList[selectedReference].Reference;
            articleRow["Nom"].Value = articlesSupplierList[selectedReference].Name;
            articleRow["Annee"].Value = articlesSupplierList[selectedReference].WineYear;
            articleRow["PrixAchat"].Value = articlesSupplierList[selectedReference].BoxBuyingPrice;

            articleRow["Quantite"].Value = 0;

            CalculArticleTotal(selectedArticle);

            dgvPanier.CurrentCell = articleRow["Quantite"];
        }

        private void dgvPanier_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPanier.Columns["Quantite"].Index)
            {
                CalculArticleTotal(selectedArticle);
            }
        }

        #endregion

        #region Add / Remove Grid lines

        private void AddArticleLine(object sender, EventArgs e)
        {
            this.dgvPanier.Rows.Add();
            int lastRowIndex = dgvPanier.Rows.Count - 1;
            this.dgvPanier.CurrentCell = dgvPanier.Rows[lastRowIndex].Cells[1];
            selectedArticle = lastRowIndex;
        }

        private void RemoveArticleLine(object sender, EventArgs e)
        {
            if (selectedArticle == -1)
            {
                return;
            }

            this.dgvPanier.Rows.RemoveAt(selectedArticle);
            selectedArticle = -1;
        }

        #endregion

        #endregion

        #region Manage Data

        private void CoutTransportChanged(object sender, EventArgs e)
        {
            CalculCommandeTotal();
        }

        private void CalculArticleTotal(int rowIndex)
        {
            if (rowIndex < 0)
            {
                return;
            }

            if (dgvPanier.Rows[rowIndex].Cells["PrixAchat"].Value == null
                || dgvPanier.Rows[rowIndex].Cells["Quantite"].Value == null)
            {
                dgvPanier.Rows[rowIndex].Cells["PrixTotal"].Value = 0;
                return;
            }

            try
            {
                decimal quantite = Convert.ToInt32(dgvPanier.Rows[rowIndex].Cells["Quantite"].Value);
                decimal prixAchat = Convert.ToDecimal(dgvPanier.Rows[rowIndex].Cells["PrixAchat"].Value);

                dgvPanier.Rows[rowIndex].Cells["PrixTotal"].Value = quantite * prixAchat;
            }
            catch (Exception)
            {
                return;
            }

            CalculCommandeTotal();
        }

        private void CalculCommandeTotal()
        {
            try
            {
                decimal.TryParse(txtCoutTransport.Text, out decimal coutTransport);

                decimal totalArticles = dgvPanier.Rows.Cast<DataGridViewRow>().Select(r => Convert.ToDecimal(r.Cells["PrixTotal"].Value)).Sum();

                txtTotalTTC.Text = (coutTransport + totalArticles).ToString();
            }
            catch(Exception)
            {
                return;
            }

        }

        private bool ControlData()
        {
            if (txtFournisseur.SelectedIndex < 0)
            {
                CustomMethods.DisplayError("Veuillez attribuer la commande à un fournisseur.");
                return false;
            }

            if (dgvPanier.Rows.Count < 1)
            {
                CustomMethods.DisplayError("Veuillez ajouter des articles à la commande.");
                return false;
            }

            return true;
        }

        #endregion

        #region Other Methods

        #endregion

        #region Request Methods

        private List<Article>? GetArticles()
        {
            List<Article> requestedObject = new List<Article>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Article");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Article>>(json) is IEnumerable<Article> requestResult)
                    {
                        requestedObject = requestResult.ToList();
                    }
                }
            }).Wait();

            return requestedObject;
        }

        private bool CreateNewSupplierCommand()
        {
            bool result = false;

            string json = JsonSerializer.Serialize(newCommand);
            StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PostAsync("SupplierCommand", content);

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
