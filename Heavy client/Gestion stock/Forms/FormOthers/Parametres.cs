using Gestion_stock.API;
using Gestion_stock.NegosudData;
using Gestion_stock.NegosudData.Interfaces;
using Gestion_stock.Utils;
using System.Data;

namespace Gestion_stock.Forms.FormOthers
{
    public partial class Parametres : Form, IPageNegosud
    {
        #region Private Variables

        // Infos de base
        private string formTitle = "Paramètres";
        private string tabID = "parametres";

        #endregion

        #region Public Variables

        public string FormTitle
        {
            get { return formTitle; }
            set
            {
                formTitle = value;
                lbFormTitle.Text = value;
            }
        }
        public string TabID { get => tabID; set => tabID = value; }

        #endregion

        #region Constructor

        public Parametres()
        {
            InitializeComponent();
            Initialize.Design(this);
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;

            ResetPageInfo();
        }

        #region Constructor Methods

        private void ResetPageInfo()
        {
            txtAncienUtilisateur.Text = null;
            txtNouveauUtilisateur.Text = null;
            txtAncienMdp.Text = null;
            txtNouveauMdp.Text = null;
            txtConfirmMdp.Text = null;
        }

        #endregion

        #endregion

        #region Events

        private void SaveMdp(object sender, EventArgs e)
        {
            if (!CustomMethods.ConfirmDataSave())
            {
                return;
            }

            if (!IsDataValid())
            {
                return;
            }

            if (!ChangeUserCredentials())
            {
                CustomMethods.DisplayError("Erreur pour changer les identifiants, vérifiez leur validité.");
            }
            else
            {
                ResetPageInfo();
                CustomMethods.DisplayInformation("Changement des identifiants réussi !");
            }
        }

        #endregion

        #region Save Data Methods

        private bool IsDataValid()
        {
            RemoveErrors();

            if (txtNouveauMdp.Text != txtConfirmMdp.Text)
            {
                errorProvider.SetError(txtConfirmMdp, "Mot de passe différent du premier.");
                return false;
            }

            return true;
        }

        #endregion

        #region Other Methods

        private void RemoveErrors()
        {
            errorProvider.Clear();
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        #endregion

        #region Request Method

        private bool ChangeUserCredentials()
        {
            bool result = false;
            Task.Run(async () =>
            {
                HttpResponseMessage response =
                    await ApiConnection.Client.PutAsync($"Connection/ChangeLoginHeavyClient?oldUsername={txtAncienUtilisateur.Text}&oldPassword={txtAncienMdp.Text}&newUsername={txtNouveauUtilisateur.Text}&newPassword={txtNouveauMdp.Text}", null);

                if (response.IsSuccessStatusCode)
                {
                    result = true;
                }
            }).Wait();

            return result;
        }

        #endregion
    }
}
