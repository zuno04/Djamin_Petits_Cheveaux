namespace Djamin_Petits_Cheveaux
{
    partial class FicChoixJeux
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
            this.rbHorsLigne = new System.Windows.Forms.RadioButton();
            this.rbEnLigne = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.bModeJeu = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbHorsLigne
            // 
            this.rbHorsLigne.AutoSize = true;
            this.rbHorsLigne.Checked = true;
            this.rbHorsLigne.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHorsLigne.Location = new System.Drawing.Point(127, 142);
            this.rbHorsLigne.Name = "rbHorsLigne";
            this.rbHorsLigne.Size = new System.Drawing.Size(144, 33);
            this.rbHorsLigne.TabIndex = 0;
            this.rbHorsLigne.TabStop = true;
            this.rbHorsLigne.Text = "Hors ligne";
            this.rbHorsLigne.UseVisualStyleBackColor = true;
            this.rbHorsLigne.CheckedChanged += new System.EventHandler(this.rbHorsLigne_CheckedChanged);
            // 
            // rbEnLigne
            // 
            this.rbEnLigne.AutoSize = true;
            this.rbEnLigne.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbEnLigne.Location = new System.Drawing.Point(127, 191);
            this.rbEnLigne.Name = "rbEnLigne";
            this.rbEnLigne.Size = new System.Drawing.Size(122, 33);
            this.rbEnLigne.TabIndex = 1;
            this.rbEnLigne.Text = "En ligne";
            this.rbEnLigne.UseVisualStyleBackColor = true;
            this.rbEnLigne.CheckedChanged += new System.EventHandler(this.rbEnLigne_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.label1.Location = new System.Drawing.Point(73, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mode de jeu";
            // 
            // bModeJeu
            // 
            this.bModeJeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bModeJeu.Location = new System.Drawing.Point(108, 267);
            this.bModeJeu.Name = "bModeJeu";
            this.bModeJeu.Size = new System.Drawing.Size(163, 39);
            this.bModeJeu.TabIndex = 3;
            this.bModeJeu.Text = "Valider";
            this.bModeJeu.UseVisualStyleBackColor = true;
            this.bModeJeu.Click += new System.EventHandler(this.bModeJeu_Click);
            // 
            // FicChoixJeux
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 450);
            this.Controls.Add(this.bModeJeu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbEnLigne);
            this.Controls.Add(this.rbHorsLigne);
            this.Name = "FicChoixJeux";
            this.Text = "Connexion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbHorsLigne;
        private System.Windows.Forms.RadioButton rbEnLigne;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bModeJeu;
    }
}