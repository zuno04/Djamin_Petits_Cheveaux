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
    public partial class FicApropos : Form
    {
        public FicApropos()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = "Deux à quatre joueurs disposent chacun d'un ou plusieurs pions-chevaux, et jouent en lançant un dé à tour de rôle." +
                " Un joueur doit d'abord réaliser un 6 avec le dé pour pouvoir sortir un cheval de son écurie. " +
                "Il doit ensuite lui faire parcourir toutes les cases situées à la périphérie du plateau, " +
                "en le faisant avancer d'un nombre de cases égal au résultat du dé. " +
                "Les pions sont avancés dans le sens des aiguilles d'une montre. \n" +
                "Il existe différents cas particuliers. Ainsi, lorsqu'un cheval arrive sur une case occupée par un concurrent," +
                " il le renvoie dans son écurie (le départ). En revanche, si le joueur arrive sur une case occupée par un autre cheval de sa couleur, " +
                "il arrête son pion juste derrière. Enfin, un 6 obtenu sur le dé permet de rejouer. \n " +
                "Après que son pion ait fait le tour du plateau, le joueur doit faire le chiffre exact " +
                "sur le dé de sorte à ce qu'il s'arrête devant son escalier. " +
                "Par exemple, si le pion-cheval est situé à trois cases du bas de l'escalier, " +
                "le joueur doit obtenir un 3 sur son dé pour emmener son pion en bas de l'escalier. \n " +
                "La victoire est remportée par le premier joueur qui arrive à amener, selon les variantes, " +
                "un ou plusieurs de ses pions-chevaux à la coupe. Chaque joueur est libre de faire sortir le nombre de chevaux qu'il désire," +
                " mais il ne peut déplacer qu'un seul cheval par tour.";

            panel1.Controls.Add(label1);
        }
    }
}
