using Gestion_stock.API;
using Gestion_stock.NegosudData;
using Gestion_stock.Utils;
using System.Runtime.CompilerServices;

namespace Gestion_stock.MainForm
{
    public partial class APIUrl : Form
    {
        public APIUrl()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.AutoScaleMode = AutoScaleMode.Font;

            this.tbApiLink.Text = ApiConnection.Uri;
        }

        private void TryConnection(object sender, EventArgs e)
        {
            ApiConnection.Uri = tbApiLink.Text.EndsWith('/') ? tbApiLink.Text : tbApiLink.Text + '/';

            if (ApiConnection.PingAPI())
            {
                WriteNewUrlInFile();
                ApiConnection.LoadClient();

                this.Visible = false;
                new Login().ShowDialog();
                this.Close();
            }
            else
            {
                this.lbMessage.Text = "Connexion à l'API échouée.";
                this.lbMessage.Visible = true;
            }
        }

        private void WriteNewUrlInFile()
        {
            File.WriteAllText(ApiConnection.ConnectionUrlPath, ApiConnection.Uri);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
