using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Djamin_Petits_Cheveaux
{
    public partial class FicChoixJeux : Form
    {
        public FicChoixJeux()
        {
            InitializeComponent();
        }

        private void bModeJeu_Click(object sender, EventArgs e)
        {
            if(rbEnLigne.Checked)
            {
                FicEnLigne ficEnLigne = new FicEnLigne();
                this.Hide();
                ficEnLigne.Show();  
            }

            if(rbHorsLigne.Checked)
            {
                EcranChoixJoeur ecranChoixJoeur = new EcranChoixJoeur();
                this.Hide();
                ecranChoixJoeur.Show();
            }
        }

        private void rbHorsLigne_CheckedChanged(object sender, EventArgs e)
        {
            if(rbHorsLigne.Checked) 
                rbEnLigne.Checked = false;
        }
        private void rbEnLigne_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEnLigne.Checked)
                rbHorsLigne.Checked = false;
        }
    }
}
