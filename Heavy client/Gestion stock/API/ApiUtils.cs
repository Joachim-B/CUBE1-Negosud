using Gestion_stock.API.Models;
using System.Text.Json;

namespace Gestion_stock.API
{
    public static class ApiUtils
    {
        #region General Requests

        public static List<WineFamily> GetWineFamiliesName()
        {
            List<WineFamily> requestedObject = new List<WineFamily>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("WineFamily");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<IEnumerable<WineFamily>>(json) is IEnumerable<WineFamily> requestResult)
                    {
                        requestedObject = requestResult.ToList();
                    }
                }
            }).Wait();

            return requestedObject;
        }

        public static List<SupplierName> GetSuppliersName()
        {
            List<SupplierName> requestedObject = new List<SupplierName>();
            Task.Run(async () =>
            {
                HttpResponseMessage response = await ApiConnection.Client.GetAsync("Supplier/Names");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();

                    if (JsonSerializer.Deserialize<List<SupplierName>>(json) is List<SupplierName> requestResult)
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
