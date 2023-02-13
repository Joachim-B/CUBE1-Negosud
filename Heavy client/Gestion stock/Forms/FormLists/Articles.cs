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
    public class Articles : DefaultForm
    {
        #region Variables

        private string formTitle = "Articles";
        private string tabID = "articles";
        private bool showAddButton = true;
        private string addButtonName = "Nouvel article";
        private bool hideFirstColumn = true;

        private List<object>? query;
        ColumnGridDesign[]? columnDesign;

        private void LoadMainGrid()
        {
            query = GetAllArticles();

            ColumnGridDesign[] design =
            {
                new("Référence", 100, 'L'),
                new("Nom", 300, 'L'),
                new("Domaine", 300, 'L'),
                new("Famille de vin", 150, 'L'),
                new("Année", 100, 'R'),
                new("Quantité restante (carton)", 100, 'R'),
                new("Quantité restante (unité)", 100, 'R'),
                new("Prix TTC carton (€)", 100, 'R'),
                new("Prix d'achat carton (€)", 100, 'R'),
                new("TVA (%)", 100, 'R'),
                new("Description", 500, 'L')
            };

            columnDesign = design;
        }

        private void LoadGridBasedOnSupplier(int IDFournisseur)
        {
            query = GetArticlesOfSupplier(IDFournisseur);

            ColumnGridDesign[] design =
            {
                new("Référence", 100, 'L'),
                new("Nom", 300, 'L'),
                new("Famille de vin", 150, 'L'),
                new("Année", 100, 'R'),
                new("Quantité restante (carton)", 100, 'R'),
                new("Quantité restante (unité)", 100, 'R'),
                new("Prix TTC carton (€)", 100, 'R'),
                new("Prix d'achat carton (€)", 100, 'R'),
                new("TVA (%)", 100, 'R'),
                new("Description", 500, 'L')
            };

            columnDesign = design;
        }

        #endregion

        #region Override Methods

        protected override Form GetIndividualPage(string id)
        {
            return new ArticleIndiv(id);
        }

        protected override Form GetNewItemForm()
        {
            return new NewArticle();
        }

        #endregion

        #region Constructor

        public Articles()
        {
            LoadMainGrid();
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);
        }

        public Articles(int IDFournisseur, string fournisseurName)
        {
            formTitle = "Articles de " + fournisseurName;
            tabID = "ArticlesFournisseur" + IDFournisseur;

            LoadGridBasedOnSupplier(IDFournisseur);
            InitializePage(formTitle, tabID, showAddButton, addButtonName, hideFirstColumn, query, columnDesign);

        }

        #endregion

        #region Requests Methods

        private static List<object>? GetAllArticles()
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

            if (requestedObject.Count == 0)
            {
                return null;
            }

            IEnumerable<object> result =
                from article in requestedObject.AsEnumerable()
                select new
                {
                    article.IDArticle,
                    article.Reference,
                    article.Name,
                    Domain = article.Supplier.Name,
                    WineFamily = article.WineFamily.Name,
                    article.WineYear,
                    article.BoxStockQuantity,
                    article.UnitStockQuantity,
                    article.BoxPriceTTC,
                    article.BoxBuyingPrice,
                    article.TVA,
                    article.Description
                };

            return result.ToList();
        }

        private static List<object>? GetArticlesOfSupplier(int idSupplier)
        {
            List<Article> requestedObject = new List<Article>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Article/BySupplier?id=" + idSupplier.ToString());

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<Article>>(json) is IEnumerable<Article> requestResult)
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
                from article in requestedObject.AsEnumerable()
                select new
                {
                    article.IDArticle,
                    article.Reference,
                    article.Name,
                    WineFamily = article.WineFamily.Name,
                    article.WineYear,
                    article.BoxStockQuantity,
                    article.UnitStockQuantity,
                    article.BoxPriceTTC,
                    article.BoxBuyingPrice,
                    article.TVA,
                    article.Description
                };

            return result.ToList();
        }

        #endregion
    }
}
