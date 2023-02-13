using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormIndividual;
using Gestion_stock.Forms.FormLists.Model;
using Gestion_stock.Forms.FormNewItem;
using Gestion_stock.NegosudData;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormLists
{
    public class CommandesFournisseur : DefaultForm
    {
        #region Variables

        private string formTitle = "Commandes fournisseurs";
        private string tabID = "CommandesFournisseur";
        private bool showAddButton = true;
        private string addButtonName = "Nouvelle commande";
        private bool hideFirstColumn = false;

        private List<object>? query = GetSupplierCommandsHeaders();

        private ColumnGridDesign[] columnDesign =
        {
            new ("ID", 100, 'L'),
            new ("Fournisseur", 200, 'L'),
            new ("Coût total", 100, 'R'),
            new ("Date de la commande", 150, 'L'),
            new ("Type de commande", 150, 'L'),
            new ("Statut", 150, 'L')
        };

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new CommandeFournisseurIndiv(id);
        }

        protected override Form GetNewItemForm()
        {
            return new NewCommandeFournisseur();
        }

        #endregion

        #region Constructor

        public CommandesFournisseur()
        {
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        #endregion

        #region Requests Methods

        private static List<object>? GetSupplierCommandsHeaders()
        {
            List<SupplierCommand> requestedObject = new List<SupplierCommand>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("SupplierCommand/Headers");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<SupplierCommand>>(json) is IEnumerable<SupplierCommand> requestResult)
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
                from supplierCommand in requestedObject
                select new
                {
                    supplierCommand.IDSupplierCommand,
                    Supplier = supplierCommand.Supplier.Name,
                    TotalCost = Math.Round(supplierCommand.TotalCost, 2),
                    supplierCommand.CommandDate,
                    CommandType = supplierCommand.CommandType.Name,
                    CommandStatus = supplierCommand.CommandStatus.Name,
                };

            return result.ToList();
        }

        #endregion
    }
}
