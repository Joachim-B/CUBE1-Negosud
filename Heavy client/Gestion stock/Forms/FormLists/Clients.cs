using Gestion_stock.API.Models;
using Gestion_stock.API;
using Gestion_stock.Forms.FormIndividual;
using Gestion_stock.Forms.FormLists.Model;
using Gestion_stock.Forms.FormNewItem;
using Gestion_stock.NegosudData;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormLists
{
    public class Clients : DefaultForm
    {
        #region Variables

        private string formTitle = "Clients";
        private string tabID = "Clients";
        private bool showAddButton = false;
        private string addButtonName = "";
        private bool hideFirstColumn = false;

        private List<object>? query = GetClients();

        private ColumnGridDesign[] columnDesign =
        {
            new ("ID Client", 100, 'L'),
            new ("Prénom", 200, 'L'),
            new ("Nom", 200, 'L'),
            new ("Adresse" , 350, 'L'),
            new ("Code postal" , 100, 'L'),
            new ("Ville" , 200, 'L'),
            new ("Pays" , 200, 'L'),
            new ("Adresse mail", 250, 'L'),
        };

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new ClientIndiv(id);
        }

        protected override Form GetNewItemForm()
        {
            return new NewArticle();
        }

        #endregion

        #region Constructor

        public Clients()
        {
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        #endregion

        #region Requests Methods

        private static List<object>? GetClients()
        {
            List<Client> requestedObject = new List<Client>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Client");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Client>>(json) is IEnumerable<Client> requestResult)
                    {
                        requestedObject = requestResult.ToList();
                    }
                }
            }).Wait();

            if (requestedObject.Count == 0)
            {
                return null;
            }

            IEnumerable<object> result =
                from client in requestedObject
                select new
                {
                    client.IDClient,
                    client.Firstname,
                    client.Lastname,
                    client.Address,
                    client.PostalCode,
                    client.Town,
                    client.Country,
                    client.Email
                };

            return result.ToList();
        }

        #endregion
    }
}
