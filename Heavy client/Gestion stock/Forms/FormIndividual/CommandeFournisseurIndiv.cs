using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormIndividual
{
    public partial class CommandeFournisseurIndiv : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = string.Empty;
        private string tabID = string.Empty;

        // ID et données de la commande
        private string IDSupplierCommand;
        private SupplierCommand commande;
        private List<object>? commandeList;

        // Autres données
        private int selectedArticle;

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

        public CommandeFournisseurIndiv(string IDCommande)
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            this.IDSupplierCommand = IDCommande;
            commande = new SupplierCommand();

            InitializeItemData();
        }

        #region Constructor Methods

        private void InitializeItemData()
        {
            GetSupplierCommand();

            if (commande != null)
            {
                FormTitle = "Commande " + IDSupplierCommand;
                tabID = "Commande" + IDSupplierCommand;
                WriteSupplierCommand();
            }
            else
            {
                CustomMethods.DisplayError("Page inconnue !");
            }

            InitializeGridView();

            AddExtraCalculs();

            AddDesignDetails();
        }

        private void GetSupplierCommand()
        {
            SupplierCommand? queryResult = GetItemData();

            if (queryResult is null)
            {
                CustomMethods.DisplayError("Erreur, aucune commande trouvée.");
            }
            else
            {
                commande = queryResult;
            }
        }

        private void WriteSupplierCommand()
        {
            txtIDCommande.Text = commande.IDSupplierCommand.ToString();
            txtDateCommande.Text = commande.CommandDate.ToString("dd/MM/yyyy HH:mm");
            txtIDFournisseur.Text = commande.IDSupplier.ToString();
            txtNomFournisseur.Text = commande.Supplier.Name;
            txtTypeCommande.Text = commande.CommandType.Name;
            txtCoutTransport.Text = commande.TransportCost.ToString();
            txtStatut.Text = commande.CommandStatus.Name;

            if (commande.IDCommandStatus == 1)
            {
                SetStatusClos();
            }
            else
            {
                SetStatusEncours();
            }
        }

        private void InitializeGridView()
        {
            commandeList = GetGridViewDatasource();
            selectedArticle = 0;

            this.dgvPanier.Columns.Clear();
            this.dgvPanier.AutoGenerateColumns = true;
            this.dgvPanier.AutoSize = true;

            this.dgvPanier.DataSource = commandeList;

            this.dgvPanier.Columns[0].Visible = false;

            this.cbFieldFilter.Items.Clear();

            // Initialisation du design des colonnes
            ColumnGridDesign[] columnDesign =
            {
                new ("Référence", 100, 'L'),
                new ("Nom", 200, 'L'),
                new ("Année", 100, 'R'),
                new ("Prix d'achat (€)", 100, 'R'),
                new ("Quantité", 100, 'R'),
                new ("Total TTC (€)", 100, 'R'),
            };

            // Ajout de chaque propriété de design à chaque colonne
            for (int i = 0; i < columnDesign.Length; i++)
            {
                int columnIndex = i + 1;

                dgvPanier.Columns[columnIndex].HeaderText = columnDesign[i].Name;
                dgvPanier.Columns[columnIndex].Width = columnDesign[i].ColumnWidth;
                if (columnDesign[i].TextAlignement == 'R')
                {
                    dgvPanier.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                this.cbFieldFilter.Items.Add(columnDesign[i].Name);
            }
        }

        private void AddExtraCalculs()
        {
            decimal totalTTC = dgvPanier.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalTTC"].Value));
            totalTTC += commande.TransportCost;

            this.txtTotalTTC.Text = totalTTC.ToString();
        }

        private void AddDesignDetails()
        {
            pnlTableContainer.AutoScroll = false;
            pnlTableContainer.VerticalScroll.Enabled = false;
            pnlTableContainer.VerticalScroll.Visible = false;
            pnlTableContainer.VerticalScroll.Maximum = 0;
            pnlTableContainer.AutoScroll = true;
        }

        private List<object> GetGridViewDatasource()
        {
            IEnumerable<object> datasource =
            from supplierCommandList in commande.SupplierCommandList
            select new
            {
                supplierCommandList.IDArticle,
                supplierCommandList.Article.Reference,
                supplierCommandList.Article.Name,
                supplierCommandList.Article.WineYear,
                supplierCommandList.Article.BoxBuyingPrice,
                supplierCommandList.Quantity,
                TotalTTC = Math.Round(supplierCommandList.Article.BoxBuyingPrice * supplierCommandList.Quantity, 2)
            };

            return datasource.ToList();
        }

        #endregion

        #endregion

        #region Events

        private void ArticleCellClicked(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            // Si l'utilisateur a cliqué sur le header
            if (rowIndex == -1)
            {
                return;
            }

            // Si la cellule cliquée n'est pas la cellule déjà sélectionnée
            if (rowIndex != selectedArticle)
            {
                selectedArticle = rowIndex;
            }
            else
            {
                // Ouverture de la page individuelle
                // On sélectionne l'id de la ligne sélectionnée (toujours la première colonne du tableau)
                string? rowID = dgvPanier.Rows[rowIndex].Cells[0].Value.ToString();

                if (rowID == null)
                {
                    CustomMethods.DisplayError("Erreur, pas d'ID");
                    return;
                }

                Form article = new ArticleIndiv(rowID);

                // Ouverture de la page
                if (this.TopLevelControl is NegoSUD topLevel)
                {
                    topLevel.AddTabIfNotExists(article);
                }
            }
        }

        private void BtnCloseCommandClicked(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmCloseCommand())
            {
                return;
            }

            if (!CloseCommand())
            {
                CustomMethods.DisplayError("Erreur lors de la clôture de la commande, vérifiez la connexion avec l'API.");
                return;
            }

            InitializeItemData();
            CustomMethods.DisplayInformation("Commande close avec succès !");
        }

        private void BtnCancelCommandClicked(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmCancelCommand())
            {
                return;
            }

            if (!CancelCommand())
            {
                CustomMethods.DisplayError("Erreur lors de l'annulation de la commande, vérifiez la connexion avec l'API.");
                return;
            }

            InitializeItemData();
            CustomMethods.DisplayInformation("Commande annulée avec succès !");
        }

        private void FournisseurLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.TopLevelControl is not NegoSUD negoSUD)
            {
                CustomMethods.DisplayError("Page principale inaccessible");
                return;
            }

            int idFournisseur = commande.IDSupplier;

            if (idFournisseur > -1)
            {
                negoSUD.AddTabIfNotExists(new FournisseurIndiv(idFournisseur));
            }
            else
            {
                CustomMethods.DisplayError("Identifiant du fournisseur vide");
                return;
            }
        }

        #endregion

        #region Filtering

        /// <summary>
        /// Tentative de filtrage des données (pas opérationnel)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterData(object sender, EventArgs e)
        {
            // Pas de filtre si la liste d'articles est nulle
            if (commandeList == null)
            {
                return;
            }

            // Pas de filtre si les 2 champs pour filtrer ne sont pas remplis
            if (this.cbFieldFilter.SelectedIndex == -1
                || string.IsNullOrEmpty(this.tbFilter.Text))
            {
                dgvPanier.DataSource = commandeList;
                dgvPanier.Refresh();
                return;
            }

            string columnToFilter = GetPropertyName();
            string filter = this.tbFilter.Text;

            // Filtre des données

            List<object>? filteredRows = commandeList.Where(r => r.GetType().GetProperty(columnToFilter).GetValue(r).ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filteredRows.Count == 0)
            {
                return;
            }

            dgvPanier.DataSource = filteredRows;
            dgvPanier.Refresh();

            if (dgvPanier.CurrentCell != null)
            {
                selectedArticle = dgvPanier.CurrentCell.RowIndex;
            }
            else
            {
                selectedArticle = -1;
            }

        }

        /// <summary>
        /// Retourne la propriété de l'objet à filtrer
        /// </summary>
        /// <param name="defaultForm"></param>
        /// <returns></returns>
        private string GetPropertyName()
        {
            int columnIndex = this.cbFieldFilter.SelectedIndex + 1;

            string columnName = dgvPanier.Columns[columnIndex].Name;

            return columnName;
        }

        #endregion

        #region Other Methods

        private void SetStatusEncours()
        {
            txtStatut.BackColor = Color.Gold;
            txtStatut.ForeColor = Color.Black;
        }

        private void SetStatusClos()
        {
            txtStatut.BackColor = Color.RoyalBlue;
            txtStatut.ForeColor = Color.White;
            btnCancel.Visible = false;
            btnClose.Visible = false;
        }

        #endregion

        #region Request Methods

        private SupplierCommand? GetItemData()
        {
            SupplierCommand? requestedObject = null;
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("SupplierCommand/" + IDSupplierCommand);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<SupplierCommand>(json) is SupplierCommand requestResult)
                    {
                        requestedObject = requestResult;
                    }
                }
            }).Wait();

            return requestedObject;
        }

        private bool CloseCommand()
        {
            bool result = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("SupplierCommand/Close?id=" + IDSupplierCommand, null);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        private bool CancelCommand()
        {
            bool result = false;

            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("SupplierCommand/Cancel?id=" + IDSupplierCommand, null);

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
