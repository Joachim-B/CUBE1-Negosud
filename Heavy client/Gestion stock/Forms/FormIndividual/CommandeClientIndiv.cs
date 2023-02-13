using Gestion_stock.API.Models;
using Gestion_stock.API;
using Gestion_stock.NegosudData;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;
using Gestion_stock.Forms.FormLists;

namespace Gestion_stock.Forms.FormIndividual
{
    public partial class CommandeClientIndiv : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = string.Empty;
        private string tabID = string.Empty;

        // ID et données de la commande
        private string IDClientCommand;
        private ClientCommand commande;
        private List<object>? articlesList;

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

        public CommandeClientIndiv(string IDArticle)
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            this.IDClientCommand = IDArticle;
            commande = new ClientCommand();

            InitializeItemData();
        }

        #region Constructor Methods

        private void InitializeItemData()
        {
            GetPageInfo();

            if (commande != null)
            {
                FormTitle = "Commande " + IDClientCommand;
                tabID = "Commande" + IDClientCommand;
                WritePageInfo();
            }
            else
            {
                CustomMethods.DisplayError("Page inconnue !");
            }

            InitializeGridView();

            AddExtraCalculs();

            AddDesignDetails();
        }

        private void GetPageInfo()
        {
            ClientCommand? queryResult = GetItemData();

            if (queryResult is null)
            {
                CustomMethods.DisplayError("Erreur, aucune commande trouvée.");
            }
            else
            {
                commande = queryResult;
            }
        }

        private void WritePageInfo()
        {
            txtIDCommande.Text = commande.IDClientCommand.ToString();
            txtDateCommande.Text = commande.CommandDate.ToString("dd/MM/yyyy HH:mm");
            txtIDClient.Text = commande.IDClient.ToString();
            txtNomClient.Text = commande.Client.Lastname;
            txtPrenomClient.Text = commande.Client.Firstname;
            txtStatut.Text = commande.CommandStatus.Name;

            // if command status is "en cours"
            if (commande.IDCommandStatus == 2)
            {
                SetStatusEncours();
            }
            else
            {
                SetStatusClos();
            }
        }

        private void InitializeGridView()
        {
            articlesList = GetGridViewDatasource();
            selectedArticle = 0;

            this.dgvPanier.Columns.Clear();
            this.dgvPanier.AutoGenerateColumns = true;
            this.dgvPanier.AutoSize = true;

            this.dgvPanier.DataSource = articlesList;

            this.dgvPanier.Columns[0].Visible = false;

            this.cbFieldFilter.Items.Clear();

            // Initialisation du design des colonnes
            ColumnGridDesign[] columnDesign =
            {
                new ("Référence", 100, 'L'),
                new ("Nom", 200, 'L'),
                new ("Domaine", 200, 'L'),
                new ("Type de quantité", 150, 'L'),
                new ("Prix TTC unitaire (€)", 100, 'R'),
                new ("TVA (%)", 100, 'R'),
                new ("Quantité", 100, 'R'),
                new ("Total HT (€)", 100, 'R'),
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
            decimal totalHT = dgvPanier.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalPriceHT"].Value));
            decimal totalTTC = dgvPanier.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalPriceTTC"].Value));

            this.txtTotalHT.Text = totalHT.ToString();
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

        private void ClientLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.TopLevelControl is not NegoSUD negoSUD)
            {
                CustomMethods.DisplayError("Page principale inaccessible");
                return;
            }

            string? idClient = commande.IDClient.ToString();

            if (idClient is not null)
            {
                negoSUD.AddTabIfNotExists(new ClientIndiv(idClient));
            }
            else
            {
                CustomMethods.DisplayError("Identifiant du client vide");
                return;
            }
        }

        #endregion

        #region Requests

        private List<object> GetGridViewDatasource()
        {
            IEnumerable<object> datasource =
                from commandList in commande.ClientCommandList
                select new
                {
                    commandList.IDArticle,
                    commandList.Article.Reference,
                    commandList.Article.Name,
                    Supplier = commandList.Article.Supplier.Name,
                    QuantityType = commandList.QuantityType.Name,
                    UnitPrice = commandList.IDQuantityType == 1 ? commandList.Article.UnitPriceTTC : commandList.Article.BoxPriceTTC,
                    commandList.Article.TVA,
                    commandList.Quantity,
                    TotalPriceTTC = Math.Round((commandList.IDQuantityType == 1 ? commandList.Article.UnitPriceTTC : commandList.Article.BoxPriceTTC) * commandList.Quantity, 2),
                    TotalPriceHT = Math.Round((commandList.IDQuantityType == 1 ? commandList.Article.UnitPriceTTC : commandList.Article.BoxPriceTTC) / (1 + (commandList.Article.TVA / 100)) * commandList.Quantity, 2)
                };

            return datasource.ToList();
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
            // Pas de filtre si articlesList est null
            if (articlesList == null)
            {
                return;
            }

            // Pas de filtre si les 2 champs pour filtrer ne sont pas remplis
            if (this.cbFieldFilter.SelectedIndex == -1
                || string.IsNullOrEmpty(this.tbFilter.Text))
            {
                dgvPanier.DataSource = articlesList;
                dgvPanier.Refresh();
                return;
            }

            string columnToFilter = GetPropertyName();
            string filter = this.tbFilter.Text;

            // Filtre des données
            List<object> filteredRows = articlesList.Where(r => r.GetType().GetProperty(columnToFilter).GetValue(r).ToString().Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();

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

        private ClientCommand? GetItemData()
        {
            ClientCommand? requestedObject = null;
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("ClientCommand/" + IDClientCommand);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<ClientCommand>(json) is ClientCommand requestResult)
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
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("ClientCommand/Close?id=" + IDClientCommand, null);

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
                HttpResponseMessage response = await ApiConnection.Client.PutAsync("ClientCommand/Cancel?id=" + IDClientCommand, null);

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
