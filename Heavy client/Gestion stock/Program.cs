using Gestion_stock.API;
using Gestion_stock.MainForm;
using Gestion_stock.Utils;
using System.Diagnostics;

namespace Gestion_stock
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!ApiConnection.CreateUrlConnectionFileIfNotExist())
            {
                return;
            }

            // Initialisation du projet
            if (InitializeProject())
            {
                ApplicationConfiguration.Initialize();

                if (ApiConnection.EstablishApiConnection())
                {
                    Application.Run(new Login());
                }
                else
                {
                    Application.Run(new APIUrl());
                }
            }
        }

        private static bool InitializeProject()
        {
            return Utils.CustomFont.LoadFonts();
        }
    }
}