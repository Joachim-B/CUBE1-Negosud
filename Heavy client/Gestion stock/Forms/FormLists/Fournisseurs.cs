using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormIndividual;
using Gestion_stock.Forms.FormLists.Model;
using Gestion_stock.Forms.FormNewItem;
using Gestion_stock.Utils.CustomControls;
using System.Data;
using System.Text.Json;

namespace Gestion_stock.Forms.FormLists
{
    public class Fournisseurs : DefaultForm
    {
        #region Variables

        private string formTitle = "Fournisseurs";
        private string tabID = "fournisseurs";
        private bool showAddButton = true;
        private string addButtonName = "Nouveau fournisseur";
        private bool hideFirstColumn = true;

        private List<object>? query = GetSuppliers();

        private ColumnGridDesign[] columnDesign =
        {
            new ("Nom" , 250, 'L'),
            new ("Adresse" , 400, 'L'),
            new ("Code postal" , 100, 'L'),
            new ("Ville" , 200, 'L'),
            new ("Pays" , 200, 'L'),
            new ("Adresse mail" , 300, 'L')
        };

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new FournisseurIndiv(Convert.ToInt32(id));
        }

        protected override Form GetNewItemForm()
        {
            return new NewFournisseur();
        }

        #endregion

        #region Constructor

        public Fournisseurs()
        {
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        #endregion

        #region Requests Methods

        private static List<object>? GetSuppliers()
        {
            List<Supplier> requestedObject = new List<Supplier>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Supplier");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Supplier>>(json) is IEnumerable<Supplier> requestResult)
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
                from supplier in requestedObject
                select new
                {
                    supplier.IDSupplier,
                    supplier.Name,
                    supplier.Address,
                    supplier.PostalCode,
                    supplier.Town,
                    supplier.Country,
                    supplier.Email
                };

            return result.ToList();
        }

        #endregion
    }
}
