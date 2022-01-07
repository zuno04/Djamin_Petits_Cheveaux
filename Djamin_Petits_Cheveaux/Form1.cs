using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Djamin_Petits_Cheveaux
{
    public partial class EcranChoixJoeur : Form
    {        
        public static int nbJoueur;
        public static int[] figure = new int[4] { -1, -1, -1, -1 };
        public static string rouge = "Joueur1", jaune = "Joueur2", bleu = "Joueur3", vert = "Joueur4";   
        public EcranChoixJoeur()
        {
            InitializeComponent();
        }      
        private void bValiser_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (cbRouge.Checked == true && tbRouge.Text == "")
                errorProvider.SetError(tbRouge, "Veiller entrer le nom du jour Rouge");
            else if (cbJaune.Checked == true && tbJaune.Text == "")
                errorProvider.SetError(tbJaune, "Veiller entrer le nom du jour Jaune");
            else if (cbBleu.Checked == true && tbBleu.Text == "")
                errorProvider.SetError(tbBleu, "Veiller entrer le nom du jour Bleu");
            else if (cbVert.Checked == true && tbVert.Text == "")
                errorProvider.SetError(tbVert, "Veiller entrer le nom du jour Vert");
            else if (nbJoueur < 1)
                errorProvider.SetError(bValiser, "il faut minimum 2 joueurs !!!");
            else
            {
                if (tbRouge.Text != string.Empty)
                    rouge = tbRouge.Text;
                if (tbJaune.Text != string.Empty)
                    jaune = tbJaune.Text;
                if (tbBleu.Text != string.Empty)
                    bleu = tbBleu.Text;
                if (tbVert.Text != string.Empty)
                    vert = tbVert.Text;

                EcranPlateau t = new EcranPlateau();
                //t.Owner = this;
                this.Hide();
                t.Show();
            }
        }
        private void DefJoueur(CheckBox cb1, CheckBox cb2, CheckBox cb3, CheckBox cb4, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4,
                                   Color couleur1, Color couleur2, int brIg1, int brIg2, bool r, bool y, bool b, bool g) //Définir les joueurs
        {
            errorProvider.Clear();

            if (cb1.Checked == true)
            {
                tb1.Enabled = true;
                tb1.BackColor = couleur1;

                if (y) cb2.Enabled = true;
                nbJoueur = brIg1;
            }
            else
            {
                if (r)
                {
                    tb1.BackColor = couleur2;
                    tb1.Enabled = false;
                    tb1.Text = string.Empty;
                }
                if (y)
                {
                    cb2.Enabled = false;
                    tb2.Enabled = false;
                    tb2.Text = string.Empty;
                }
                if (b)
                {
                    cb3.Enabled = false;
                    tb3.Enabled = false;
                    tb3.Text = string.Empty;
                }
                if (g)
                {
                    cb4.Enabled = false;
                    tb4.Enabled = false;
                    tb4.Text = string.Empty;
                }
                if (y) cb2.Checked = false;
                if (b) cb3.Checked = false;
                if (g) cb4.Checked = false;
                nbJoueur = brIg2;
            }
        }              
        private void cbRouge_CheckedChanged(object sender, EventArgs e)
        {
            DefJoueur(cbRouge, cbJaune, cbBleu, cbVert, tbRouge, tbJaune, tbBleu, tbVert, 
                Color.Red, SystemColors.Control, 0, 0, true, true, true, true);
        }      
        private void cbJaune_CheckedChanged(object sender, EventArgs e)
        {
            DefJoueur(cbJaune, cbBleu, cbVert, null, tbJaune, tbBleu, tbVert, null,
                Color.Yellow, SystemColors.Control, 1, 0, true, true, true, false);
        }
        private void cbBleu_CheckedChanged(object sender, EventArgs e)
        {
            DefJoueur(cbBleu, cbVert, null, null, tbBleu, tbVert, null, null,
                Color.Blue, SystemColors.Control, 2, 1, true, true, false, false);
        }
        private void cbVert_CheckedChanged(object sender, EventArgs e)
        {
            DefJoueur(cbVert, null, null, null, tbVert, null, null, null, 
                Color.Green, SystemColors.Control, 3, 2, true, false, false, false);
        }       
    }
}
