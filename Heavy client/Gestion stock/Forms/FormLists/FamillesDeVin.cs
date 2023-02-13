using Gestion_stock.API;
using Gestion_stock.API.Models;
using Gestion_stock.Forms.FormLists.Model;
using Gestion_stock.Forms.FormNewItem;
using Gestion_stock.Utils.CustomControls;
using System.Data;

namespace Gestion_stock.Forms.FormLists
{
    public class FamillesDeVin : DefaultForm
    {
        #region Variables

        private string formTitle = "Familles de vin";
        private string tabID = "FamillesDeVin";
        private bool showAddButton = true;
        private string addButtonName = "Nouvelle famille de vin";
        private bool hideFirstColumn = true;

        private List<object>? query = GetWineFamilies();

        private ColumnGridDesign[] columnDesign =
        {
            new ("Nom", 200, 'L')
        };

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new Form();
        }

        protected override Form GetNewItemForm()
        {
            return new NewFamilleDeVin();
        }

        #endregion

        #region Constructor

        public FamillesDeVin()
        {
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        #endregion

        #region Requests Methods

        private static List<object>? GetWineFamilies()
        {
            List<WineFamily> wineFamilies = ApiUtils.GetWineFamiliesName();

            if (wineFamilies.Count == 0)
            {
                return null;
            }

            IEnumerable<object> result =
                from family in wineFamilies
                select new
                {
                    family.IDWineFamily,
                    family.Name
                };

            return result.ToList();
        }

        #endregion
    }
}
