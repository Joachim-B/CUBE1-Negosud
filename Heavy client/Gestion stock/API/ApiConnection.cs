using Gestion_stock.Utils;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Gestion_stock.API
{
    public static class ApiConnection
    {
        private static string uri = string.Empty;
        private static string connectionUrlPath = string.Empty;

        private static HttpClient client = new();

        public static HttpClient Client
        {
            get
            {
                return client;
            }
        }

        public static string ConnectionUrlPath { get => connectionUrlPath; }
        public static string Uri { get => uri; set => uri = value; }

        #region Public Methods

        public static void LoadClient()
        {
            HttpClient newClient = new HttpClient();
            newClient.BaseAddress = new Uri(uri);
            newClient.DefaultRequestHeaders.Accept.Clear();
            newClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client = newClient;
        }

        /// <summary>
        /// Ping the URI and return true if connection has been made.
        /// </summary>
        /// <returns></returns>
        public static bool PingAPI()
        {
            if (uri.Contains("localhost:"))
            {
                int port = GetPortNumbers(uri);
                try
                {
                    using var client = new TcpClient("localhost", port);
                    return client.Connected;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(uri);
                    return reply.Status == IPStatus.Success;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static bool CreateUrlConnectionFileIfNotExist()
        {
            string connectionUrlDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string projectName = AppDomain.CurrentDomain.FriendlyName;

            while (Path.GetFileName(connectionUrlDirectory) != projectName)
            {
                if (!SelectParentDirectory(ref connectionUrlDirectory))
                {
                    CustomMethods.DisplayError("Erreur lors de la recherche du dossier parent");
                    return false;
                }
            }

            if (!SelectParentDirectory(ref connectionUrlDirectory))
            {
                CustomMethods.DisplayError("Erreur lors de la recherche du dossier parent");
                return false;
            }

            connectionUrlPath = Path.Combine(connectionUrlDirectory, "connection_url.txt");

            // Création du fichier .txt
            if (!File.Exists(connectionUrlPath))
            {
                File.Create(connectionUrlPath);
            }

            return true;

            static bool SelectParentDirectory(ref string connectionUrlDirectory)
            {
                DirectoryInfo? parentPath = Directory.GetParent(connectionUrlDirectory);
                if (parentPath == null)
                {
                    return false;
                }

                connectionUrlDirectory = parentPath.FullName;
                return true;
            }
        }

        public static bool EstablishApiConnection()
        {
            LoadConnectionUrl();

            if (PingAPI())
            {
                LoadClient();
                return true;
            }
            return false;
        }

        #endregion

        #region Private Methods

        private static void LoadConnectionUrl()
        {
            if (!File.Exists(connectionUrlPath))
            {
                uri = string.Empty;
            }

            using StreamReader sr = new StreamReader(connectionUrlPath);

            uri = sr.ReadToEnd();
        }

        private static int GetPortNumbers(string url)
        {
            int index = url.IndexOf("localhost:") + "localhost:".Length;

            StringBuilder sb = new StringBuilder();

            while (int.TryParse(url[index].ToString(), out int n))
            {
                sb.Append(n);
                index++;
            }

            if (int.TryParse(sb.ToString(), out int port))
            {
                return port;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
