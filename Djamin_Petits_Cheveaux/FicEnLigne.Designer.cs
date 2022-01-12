namespace Djamin_Petits_Cheveaux
{
    partial class FicEnLigne
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
            this.lbEchange = new System.Windows.Forms.ListBox();
            this.bDeconnecter = new System.Windows.Forms.Button();
            this.bEcouter = new System.Windows.Forms.Button();
            this.bConnecter = new System.Windows.Forms.Button();
            this.tbServeur = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bDemarrer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbEchange
            // 
            this.lbEchange.FormattingEnabled = true;
            this.lbEchange.ItemHeight = 16;
            this.lbEchange.Location = new System.Drawing.Point(170, 289);
            this.lbEchange.Name = "lbEchange";
            this.lbEchange.Size = new System.Drawing.Size(367, 100);
            this.lbEchange.TabIndex = 15;
            // 
            // bDeconnecter
            // 
            this.bDeconnecter.Enabled = false;
            this.bDeconnecter.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDeconnecter.Location = new System.Drawing.Point(170, 224);
            this.bDeconnecter.Margin = new System.Windows.Forms.Padding(4);
            this.bDeconnecter.Name = "bDeconnecter";
            this.bDeconnecter.Size = new System.Drawing.Size(367, 43);
            this.bDeconnecter.TabIndex = 14;
            this.bDeconnecter.Text = "Deconnecter";
            this.bDeconnecter.UseVisualStyleBackColor = true;
            this.bDeconnecter.Click += new System.EventHandler(this.bDeconnecter_Click);
            // 
            // bEcouter
            // 
            this.bEcouter.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEcouter.Location = new System.Drawing.Point(170, 173);
            this.bEcouter.Margin = new System.Windows.Forms.Padding(4);
            this.bEcouter.Name = "bEcouter";
            this.bEcouter.Size = new System.Drawing.Size(367, 43);
            this.bEcouter.TabIndex = 13;
            this.bEcouter.Text = "Ecouter";
            this.bEcouter.UseVisualStyleBackColor = true;
            this.bEcouter.Click += new System.EventHandler(this.bEcouter_Click);
            // 
            // bConnecter
            // 
            this.bConnecter.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bConnecter.Location = new System.Drawing.Point(170, 126);
            this.bConnecter.Margin = new System.Windows.Forms.Padding(4);
            this.bConnecter.Name = "bConnecter";
            this.bConnecter.Size = new System.Drawing.Size(367, 43);
            this.bConnecter.TabIndex = 12;
            this.bConnecter.Text = "Connecter";
            this.bConnecter.UseVisualStyleBackColor = true;
            this.bConnecter.Click += new System.EventHandler(this.bConnecter_Click);
            // 
            // tbServeur
            // 
            this.tbServeur.Location = new System.Drawing.Point(170, 82);
            this.tbServeur.Multiline = true;
            this.tbServeur.Name = "tbServeur";
            this.tbServeur.Size = new System.Drawing.Size(367, 37);
            this.tbServeur.TabIndex = 11;
            this.tbServeur.Text = "Zuno11";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(307, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Adresse IP";
            // 
            // bDemarrer
            // 
            this.bDemarrer.Enabled = false;
            this.bDemarrer.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bDemarrer.Location = new System.Drawing.Point(170, 396);
            this.bDemarrer.Margin = new System.Windows.Forms.Padding(4);
            this.bDemarrer.Name = "bDemarrer";
            this.bDemarrer.Size = new System.Drawing.Size(367, 43);
            this.bDemarrer.TabIndex = 16;
            this.bDemarrer.Text = "Lancer la partie";
            this.bDemarrer.UseVisualStyleBackColor = true;
            this.bDemarrer.Visible = false;
            this.bDemarrer.Click += new System.EventHandler(this.bDemarrer_Click);
            // 
            // FicEnLigne
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 450);
            this.Controls.Add(this.bDemarrer);
            this.Controls.Add(this.lbEchange);
            this.Controls.Add(this.bDeconnecter);
            this.Controls.Add(this.bEcouter);
            this.Controls.Add(this.bConnecter);
            this.Controls.Add(this.tbServeur);
            this.Controls.Add(this.label1);
            this.Name = "FicEnLigne";
            this.Text = "EnLigne";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbEchange;
        private System.Windows.Forms.Button bDeconnecter;
        private System.Windows.Forms.Button bEcouter;
        private System.Windows.Forms.Button bConnecter;
        private System.Windows.Forms.TextBox tbServeur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bDemarrer;
    }
}