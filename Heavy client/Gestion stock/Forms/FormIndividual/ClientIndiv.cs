using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Text.Json;

namespace Gestion_stock.Forms.FormIndividual
{
    public partial class ClientIndiv : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = string.Empty;
        private string tabID = string.Empty;

        // ID et données de l'article
        private string IDClient;
        private Client client;

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

        public ClientIndiv(string IDClient)
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            this.IDClient = IDClient;
            client = new Client();

            InitializeItemData();
        }

        #region Constructor Methods

        private void GetPageInfo()
        {
            Client? queryResult = GetItemData();

            if (queryResult is null)
            {
                CustomMethods.DisplayError("Erreur, aucun client trouvé.");
            }
            else
            {
                client = queryResult;
            }
        }

        private void InitializeItemData()
        {
            GetPageInfo();

            if (client != null)
            {
                FormTitle = $"Client \"{client.Firstname} {client.Lastname}\"";
                tabID = "Client" + IDClient;
                WritePageInfo();
            }
            else
            {
                CustomMethods.DisplayError("Page inconnue !");
            }
        }

        private void WritePageInfo()
        {
            txtID.Text = client.IDClient.ToString();
            txtNom.Text = client.Lastname;
            txtPrenom.Text = client.Firstname;
            txtEmail.Text = client.Email;
            txtAdresse.Text = client.Address;
            txtCodePostal.Text = client.PostalCode;
            txtVille.Text = client.Town;
            txtPays.Text = client.Country;
        }

        #endregion

        #endregion

        #region Events

        private void ReloadPage(object sender, EventArgs e)
        {
            InitializeItemData();
        }

        #endregion

        #region Requests

        private Client? GetItemData()
        {
            Client? requestedObject = null;
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Client/" + IDClient);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<Client>(json) is Client requestResult)
                    {
                        requestedObject = requestResult;
                    }
                }
            }).Wait();

            return requestedObject;
        }

        #endregion
    }
}
