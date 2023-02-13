namespace Gestion_stock.MainForm
{
    partial class APIUrl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIUrl));
            this.tbApiLink = new System.Windows.Forms.TextBox();
            this.lbApiTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.lbMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbApiLink
            // 
            resources.ApplyResources(this.tbApiLink, "tbApiLink");
            this.tbApiLink.Name = "tbApiLink";
            // 
            // lbApiTitle
            // 
            resources.ApplyResources(this.lbApiTitle, "lbApiTitle");
            this.lbApiTitle.Name = "lbApiTitle";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnPing
            // 
            resources.ApplyResources(this.btnPing, "btnPing");
            this.btnPing.BackColor = System.Drawing.Color.White;
            this.btnPing.Name = "btnPing";
            this.btnPing.UseVisualStyleBackColor = false;
            this.btnPing.Click += new System.EventHandler(this.TryConnection);
            // 
            // btnAnnuler
            // 
            resources.ApplyResources(this.btnAnnuler, "btnAnnuler");
            this.btnAnnuler.BackColor = System.Drawing.Color.White;
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // lbMessage
            // 
            resources.ApplyResources(this.lbMessage, "lbMessage");
            this.lbMessage.ForeColor = System.Drawing.Color.Red;
            this.lbMessage.Name = "lbMessage";
            // 
            // APIUrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnPing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbApiTitle);
            this.Controls.Add(this.tbApiLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "APIUrl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbApiLink;
        private Label lbApiTitle;
        private Label label1;
        private Button btnPing;
        private Button btnAnnuler;
        private Label lbMessage;
    }
}