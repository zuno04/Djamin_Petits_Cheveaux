
namespace Djamin_Petits_Cheveaux
{
    partial class EcranChoixJoeur
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbRouge = new System.Windows.Forms.CheckBox();
            this.cbBleu = new System.Windows.Forms.CheckBox();
            this.cbJaune = new System.Windows.Forms.CheckBox();
            this.cbVert = new System.Windows.Forms.CheckBox();
            this.bValiser = new System.Windows.Forms.Button();
            this.tbRouge = new System.Windows.Forms.TextBox();
            this.tbJaune = new System.Windows.Forms.TextBox();
            this.tbBleu = new System.Windows.Forms.TextBox();
            this.tbVert = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cbRouge
            // 
            this.cbRouge.AutoSize = true;
            this.cbRouge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.cbRouge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRouge.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            this.cbRouge.ForeColor = System.Drawing.Color.Red;
            this.cbRouge.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbRouge.Location = new System.Drawing.Point(12, 26);
            this.cbRouge.Name = "cbRouge";
            this.cbRouge.Size = new System.Drawing.Size(123, 41);
            this.cbRouge.TabIndex = 1;
            this.cbRouge.Text = "Rouge";
            this.cbRouge.UseVisualStyleBackColor = true;
            this.cbRouge.CheckedChanged += new System.EventHandler(this.cbRouge_CheckedChanged);
            // 
            // cbBleu
            // 
            this.cbBleu.AutoSize = true;
            this.cbBleu.Enabled = false;
            this.cbBleu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.cbBleu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBleu.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            this.cbBleu.ForeColor = System.Drawing.Color.Blue;
            this.cbBleu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbBleu.Location = new System.Drawing.Point(12, 176);
            this.cbBleu.Name = "cbBleu";
            this.cbBleu.Size = new System.Drawing.Size(105, 41);
            this.cbBleu.TabIndex = 2;
            this.cbBleu.Text = "Bleu";
            this.cbBleu.UseVisualStyleBackColor = true;
            this.cbBleu.CheckedChanged += new System.EventHandler(this.cbBleu_CheckedChanged);
            // 
            // cbJaune
            // 
            this.cbJaune.AutoSize = true;
            this.cbJaune.Enabled = false;
            this.cbJaune.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.cbJaune.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbJaune.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            this.cbJaune.ForeColor = System.Drawing.Color.Yellow;
            this.cbJaune.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbJaune.Location = new System.Drawing.Point(12, 100);
            this.cbJaune.Name = "cbJaune";
            this.cbJaune.Size = new System.Drawing.Size(123, 41);
            this.cbJaune.TabIndex = 3;
            this.cbJaune.Text = "Jaune";
            this.cbJaune.UseVisualStyleBackColor = true;
            this.cbJaune.CheckedChanged += new System.EventHandler(this.cbJaune_CheckedChanged);
            // 
            // cbVert
            // 
            this.cbVert.AutoSize = true;
            this.cbVert.Enabled = false;
            this.cbVert.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.cbVert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVert.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold);
            this.cbVert.ForeColor = System.Drawing.Color.Green;
            this.cbVert.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbVert.Location = new System.Drawing.Point(12, 259);
            this.cbVert.Name = "cbVert";
            this.cbVert.Size = new System.Drawing.Size(105, 41);
            this.cbVert.TabIndex = 4;
            this.cbVert.Text = "Vert";
            this.cbVert.UseVisualStyleBackColor = true;
            this.cbVert.CheckedChanged += new System.EventHandler(this.cbVert_CheckedChanged);
            // 
            // bValiser
            // 
            this.bValiser.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bValiser.Location = new System.Drawing.Point(12, 341);
            this.bValiser.Name = "bValiser";
            this.bValiser.Size = new System.Drawing.Size(422, 44);
            this.bValiser.TabIndex = 5;
            this.bValiser.Text = "Valider";
            this.bValiser.UseVisualStyleBackColor = true;
            this.bValiser.Click += new System.EventHandler(this.bValiser_Click);
            // 
            // tbRouge
            // 
            this.tbRouge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRouge.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbRouge.Enabled = false;
            this.tbRouge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRouge.Location = new System.Drawing.Point(242, 38);
            this.tbRouge.Name = "tbRouge";
            this.tbRouge.Size = new System.Drawing.Size(192, 26);
            this.tbRouge.TabIndex = 6;
            // 
            // tbJaune
            // 
            this.tbJaune.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbJaune.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbJaune.Enabled = false;
            this.tbJaune.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbJaune.Location = new System.Drawing.Point(242, 112);
            this.tbJaune.Name = "tbJaune";
            this.tbJaune.Size = new System.Drawing.Size(192, 26);
            this.tbJaune.TabIndex = 7;
            // 
            // tbBleu
            // 
            this.tbBleu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBleu.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbBleu.Enabled = false;
            this.tbBleu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBleu.Location = new System.Drawing.Point(242, 191);
            this.tbBleu.Name = "tbBleu";
            this.tbBleu.Size = new System.Drawing.Size(192, 26);
            this.tbBleu.TabIndex = 8;
            // 
            // tbVert
            // 
            this.tbVert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbVert.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbVert.Enabled = false;
            this.tbVert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbVert.Location = new System.Drawing.Point(242, 271);
            this.tbVert.Name = "tbVert";
            this.tbVert.Size = new System.Drawing.Size(192, 26);
            this.tbVert.TabIndex = 9;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // EcranChoixJoeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 389);
            this.Controls.Add(this.tbVert);
            this.Controls.Add(this.tbBleu);
            this.Controls.Add(this.tbJaune);
            this.Controls.Add(this.tbRouge);
            this.Controls.Add(this.bValiser);
            this.Controls.Add(this.cbVert);
            this.Controls.Add(this.cbJaune);
            this.Controls.Add(this.cbBleu);
            this.Controls.Add(this.cbRouge);
            this.Name = "EcranChoixJoeur";
            this.Text = "Choix Nombre Joueur";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbRouge;
        private System.Windows.Forms.CheckBox cbBleu;
        private System.Windows.Forms.CheckBox cbJaune;
        private System.Windows.Forms.CheckBox cbVert;
        private System.Windows.Forms.Button bValiser;
        private System.Windows.Forms.TextBox tbRouge;
        private System.Windows.Forms.TextBox tbJaune;
        private System.Windows.Forms.TextBox tbBleu;
        private System.Windows.Forms.TextBox tbVert;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}

