using Gestion_stock.API.Models;
using Gestion_stock.API;
using Gestion_stock.Forms.FormIndividual;
using Gestion_stock.Forms.FormLists.Model;
using Gestion_stock.NegosudData;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormLists
{
    public class CommandesClient : DefaultForm
    {
        #region Variables

        private string formTitle = "Commandes clients";
        private string tabID = "CommandesClient";
        private bool showAddButton = false;
        private string addButtonName = "";
        private bool hideFirstColumn = false;

        private List<object>? query = GetClientCommandsHeaders();

        private ColumnGridDesign[] columnDesign =
        {
            new ("ID Commande", 100, 'L'),
            new ("Nom", 200, 'L'),
            new ("Prénom", 200, 'L'),
            new ("Date de la commande", 150, 'L'),
            new ("Total TTC (€)", 100, 'R'),
            new ("Total HT (€)", 100, 'R'),
            new ("Statut", 150, 'L')
        };

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new CommandeClientIndiv(id);
        }

        protected override Form GetNewItemForm()
        {
            return new Form();
        }

        #endregion

        #region Constructor

        public CommandesClient()
        {
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        #endregion

        #region Requests Methods

        private static List<object>? GetClientCommandsHeaders()
        {
            List<ClientCommand> requestedObject = new List<ClientCommand>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("ClientCommand/Headers");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<ClientCommand>>(json) is IEnumerable<ClientCommand> requestResult)
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
                from clientCommand in requestedObject
                select new
                {
                    clientCommand.IDClientCommand,
                    clientCommand.Client.Firstname,
                    clientCommand.Client.Lastname,
                    clientCommand.CommandDate,
                    TotalCostTTC = Math.Round(clientCommand.TotalCostTTC, 2),
                    TotalCostHT = Math.Round(clientCommand.TotalCostHT, 2),
                    CommandStatus = clientCommand.CommandStatus.Name,
                };

            return result.ToList();
        }

        #endregion
    }
}
