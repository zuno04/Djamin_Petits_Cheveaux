using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Resources;
using Djamin_Petits_Cheveaux.Properties;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace Djamin_Petits_Cheveaux
{
    public partial class EcranPlateau : Form
    {
        private Socket sServeur, sClient;
        private Byte[] bBuffer;

        delegate void RenvoiVersInserer(string sTexte);

        private void InsererItermThread(string sTexte)
        {
            Thread ThreadInsererIterm = new Thread(new ParameterizedThreadStart(InsererIterm));
            ThreadInsererIterm.Start(sTexte);
        }
        private void InsererIterm(object oTexte)
        {
            if (lbEchange.InvokeRequired)
            {
                RenvoiVersInserer f = new RenvoiVersInserer(InsererIterm);
                Invoke(f, new object[] { (string)oTexte });
            }
            else
                lbEchange.Items.Insert(0, (string)oTexte);
        }

        public EcranPlateau()
        {
            InitializeComponent();

            sServeur = null;
            sClient = null;
            bBuffer = new Byte[1024];

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(EcranPlateau_KeyDown);
        }

        private IPAddress AdresseValide(string nomPC)
        {
            IPAddress ipReponse = null;
            if (nomPC.Length > 0)
            {
                IPAddress[] ipsMachine = Dns.GetHostEntry(nomPC).AddressList;
                for (int i = 0; i < ipsMachine.Length; i++)
                {
                    Ping ping = new Ping();
                    try
                    {
                        PingReply pingReponse = ping.Send(ipsMachine[i]);
                        if (pingReponse.Status == IPStatus.Success)
                            if (ipsMachine[i].AddressFamily == AddressFamily.InterNetwork)
                            {
                                ipReponse = ipsMachine[i];
                                break;
                            }
                    }
                    catch { }
                }
            }
            return ipReponse;
        }

        [DataMember]
        Joueur joueur0, joueur1, joueur2, joueur3;
        PictureBox[] plateau;
        Image[] dice;
        Button[] buttonJ0, buttonJ1, buttonJ2, buttonJ3;
        Button[][] xButton = new Button[4][];
        Bitmap[] diceBmp = new Bitmap[7] { Properties.Resources.dice0, Properties.Resources.dice1, Properties.Resources.dice2,
        Properties.Resources.dice2, Properties.Resources.dice4, Properties.Resources.dice5, Properties.Resources.dice6 };
        Bitmap[][] xBitmap = new Bitmap[16][];
        int bottonDice = 0, nbjoueur = 0;              
        bool startStop = true;
        int locX0 = 27, locY0 = 30, locX1 = 195, locY1 = 30, locX2 = 27, locY2 = 195, locX3 = 195, locY3 = 195;
        private void EcranPlateau_Load(object sender, EventArgs e)
        {
            joueur0 = new Joueur();
            joueur1 = new Joueur();
            joueur2 = new Joueur();
            joueur3 = new Joueur();
            nbjoueur = EcranChoixJoeur.nbJoueur; // recupère le nombre de joueur = min 2
            for (int i = 0; i <= 3; i++)
            {
                //positions de départ du joueur, player0 par défaut a des positions mises à zéro
                joueur1.PositionJoueur[i] = 12;
                joueur2.PositionJoueur[i] = 24;
                joueur3.PositionJoueur[i] = 36;
            }
            // positions pour chaque joueur du début à la fin de son mouvement sur le plateau, y compris le stationnement
            joueur0.PositionOnTable = new int[52] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
                                         26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51 };
            joueur1.PositionOnTable = new int[52] { 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35,
                                         36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 52, 53, 54, 55 };
            joueur2.PositionOnTable = new int[52] { 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47,
                                         0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 56, 57, 58, 59 };
            joueur3.PositionOnTable = new int[52] { 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,
                                         14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 60, 61, 62, 63 };
            buttonJ0 = new Button[4] { bR0, bR1, bR2, bR3 };
            buttonJ1 = new Button[4] { bJ0, bJ1, bJ2, bJ3 };
            buttonJ2 = new Button[4] { bB0, bB1, bB2, bB3 };
            buttonJ3 = new Button[4] { bV0, bV1, bV2, bV3 };
            // une série de boutons (une figurine qui joue en 4x4)
            xButton[0] = buttonJ0;
            xButton[1] = buttonJ1;
            xButton[2] = buttonJ2;
            xButton[3] = buttonJ3;

            dice = new Image[7];

            for (int i = 0; i < dice.Length; i++)
                dice[i] = diceBmp[i];
            plateau = new PictureBox[64];
            int x = 404, y = 12;

            for (int i = 0; i <= 47; i++)
            {
                plateau[i] = new PictureBox();
                plateau[i].Size = new Size(50, 50);
                plateau[i].BackColor = Color.WhiteSmoke;
                if ((i >= 1 && i <= 5) || (i >= 11 && i <= 12) || (i >= 18 && i <= 22))
                    y += 50 + 6;
                if ((i >= 6 && i <= 10) || (i >= 37 && i <= 41) || (i >= 47))
                    x += 50 + 6;
                if ((i >= 13 && i <= 17) || (i >= 23 && i <= 24) || (i >= 30 && i <= 34))
                    x -= 50 + 6;
                if ((i >= 25 && i <= 29) || (i >= 35 && i <= 36) || (i >= 42 && i <= 46))
                    y -= 50 + 6;

                plateau[i].Location = new Point(x, y);

                if (i == 0)
                {
                    plateau[i].BackgroundImage = Properties.Resources.down;
                    plateau[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (i == 12)
                {
                    plateau[i].BackgroundImage = Properties.Resources.left;
                    plateau[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (i == 24)
                {
                    plateau[i].BackgroundImage = Properties.Resources.up;
                    plateau[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (i == 36)
                {
                    plateau[i].BackgroundImage = Properties.Resources.right;
                    plateau[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                this.Controls.Add(plateau[i]);
            }
            //définition des dernerière cases
            plateau[48] = pbR1;
            plateau[49] = pbR2;
            plateau[50] = pbR3;
            plateau[51] = pbR4;

            plateau[52] = pbJ1;
            plateau[53] = pbJ2;
            plateau[54] = pbJ3;
            plateau[55] = pbJ4;

            plateau[56] = pbB1;
            plateau[57] = pbB2;
            plateau[58] = pbB3;
            plateau[59] = pbB4;

            plateau[60] = pbV1;
            plateau[61] = pbV2;
            plateau[62] = pbV3;
            plateau[63] = pbV4;

            DefFigure(EcranChoixJoeur.figure);
            pFinal.SendToBack();

            //recupère le nom des joeurs
            DefNomJoueur(EcranChoixJoeur.rouge, lRouge);
            DefNomJoueur(EcranChoixJoeur.jaune, lJaune);
            DefNomJoueur(EcranChoixJoeur.bleu, lBleu);
            DefNomJoueur(EcranChoixJoeur.vert, lVert);

            toolTip.SetToolTip(buttonReda0, "passer au joueur suivant");
            toolTip.SetToolTip(buttonReda1, "passer au joueur suivant");
            toolTip.SetToolTip(buttonReda2, "passer au joueur suivant");
            toolTip.SetToolTip(buttonReda3, "passer au joueur suivant");

            toolTip.SetToolTip(lEtatRouge, "Joueur rouge a la main");
            toolTip.SetToolTip(lEtatJaune, "Joueur jaune a la main");
            toolTip.SetToolTip(lEtatBleu, "Joueur Bleu a la main");
            toolTip.SetToolTip(lEtatVert, "Joueur vert a la main");

            this.Text = "Petits cheveaux";
        }
        public void TurnDece()
        {
            if (startStop)
            {
                startStop = false;
                timerDice.Start();
            }
            else
            {

                startStop = true;
                timerDice.Stop();
                   
            }
        }
        private void TimerDice_Tick(object sender, EventArgs e)
        {

            if (bottonDice == 0)
                joueur0.SimulRotation(pDice, dice); //Simulation de rotation pour chaque joueur            
            else if (bottonDice == 1)
                joueur1.SimulRotation(pDice, dice);
            else if (bottonDice == 2)
                joueur2.SimulRotation(pDice, dice);
            else
                joueur3.SimulRotation(pDice, dice);
        }
        private void pDice_Click(object sender, EventArgs e) // reservé au dice
        {
            TurnDece();
            if (bottonDice == 0 && timerDice.Enabled == false)

                ActiverDesactiver(joueur0, buttonJ0, buttonJ1, buttonJ2, buttonJ3, 'R');
                
            else if (bottonDice == 1 && timerDice.Enabled == false)
                ActiverDesactiver(joueur1, buttonJ1, buttonJ0, buttonJ2, buttonJ3, 'J');

            else if (bottonDice == 2 && timerDice.Enabled == false)
                ActiverDesactiver(joueur2, buttonJ2, buttonJ0, buttonJ1, buttonJ3, 'B');

            else if (bottonDice == 3 && timerDice.Enabled == false)
                ActiverDesactiver(joueur3, buttonJ3, buttonJ0, buttonJ1, buttonJ2, 'V');
        } 
        private void Dice_click(object sender, EventArgs e) // Click sur un pion(enlève le pion a sa position pour une nouvelle position)
        {
            Button b = (Button)sender;

            if (bottonDice == 0)
            {
                if (VerifJoueur(joueur0, b))
                {
                    b.Parent.Controls.Remove(b);
                    this.Controls.Add(b);
                    b.BringToFront();
                }
            }
            else if (bottonDice == 1)
            {
                if (VerifJoueur(joueur1, b))
                {
                    b.Parent.Controls.Remove(b);
                    this.Controls.Add(b);
                    b.BringToFront();
                }
            }
            else if (bottonDice == 2)
            {
                if (VerifJoueur(joueur2, b))
                {
                    b.Parent.Controls.Remove(b);
                    this.Controls.Add(b);
                    b.BringToFront();
                }
            }
            else
            {
                if (VerifJoueur(joueur3, b))
                {
                    b.Parent.Controls.Remove(b);
                    this.Controls.Add(b);
                    b.BringToFront();
                }
            }
        }
        private void buttonDeceTurn_Click(object sender, EventArgs e) // bouton pour Changer de ligne (validation pour lancer le dice)
        {
            Button b = (Button)sender;
            char bouton = b.Name[b.Name.Length - 1];

            bottonDice++;
            if (bottonDice == nbjoueur + 1)
                bottonDice = 0;

            b.Visible = false;
            pDice.Enabled = true;
            pFinal.BackColor = Color.Green;
            pDice_Click(null, null);

            if (nbjoueur == 1)
            {
                if (bouton == '0')
                { lEtatRouge.Visible = false; lEtatJaune.Visible = true; }
                else if (bouton == '1')
                { lEtatJaune.Visible = false; lEtatRouge.Visible = true; }
            }
            else if (nbjoueur == 2)
            {
                if (bouton == '0')
                { lEtatRouge.Visible = false; lEtatJaune.Visible = true; }
                else if (bouton == '1')
                { lEtatJaune.Visible = false; lEtatBleu.Visible = true; }
                else if (bouton == '2')
                { lEtatBleu.Visible = false; lEtatRouge.Visible = true; }
            }
            else
            {
                if (bouton == '0')
                { lEtatRouge.Visible = false; lEtatJaune.Visible = true; }
                else if (bouton == '1')
                { lEtatJaune.Visible = false; lEtatBleu.Visible = true; }
                else if (bouton == '2')
                { lEtatBleu.Visible = false; lEtatVert.Visible = true; }
                else if (bouton == '3')
                { lEtatVert.Visible = false; lEtatRouge.Visible = true; }
            }
        }       
        private bool VerifJoueur(Joueur jr, Button bouton) //Vérifier les joueurs
        {
            int index = Convert.ToInt32(bouton.Name[bouton.Name.Length - 1].ToString());
            char c = bouton.Name[bouton.Name.Length - 2]; //recupère la couleur du pion
            //Si c'est un 6 et qu'il n'est pas sur le plateau, mettez-le en position de départ
            if (index == 0 && jr.NbCubes == 6 && jr.StartJeux[index] == true)
            {
                jr.StartJeux[index] = false;
                jr.ContreGamer[index] = 0;
                bouton.Location = new Point(plateau[jr.PositionJoueur[index]].Location.X, plateau[jr.PositionJoueur[index]].Location.Y);       

                MangerJoueur(joueur0, joueur1, joueur2, joueur3, c, index);

                DesactivationBouttonJoueur(c);
                DésactiverAvtiverButtonReda(jr, c);
                return true;
            }
            else if (index == 1 && jr.NbCubes == 6 && jr.StartJeux[index] == true)
            {
                jr.StartJeux[index] = false;
                jr.ContreGamer[index] = 0;
                bouton.Location = new Point(plateau[jr.PositionJoueur[index]].Location.X, plateau[jr.PositionJoueur[index]].Location.Y);

                MangerJoueur(joueur0, joueur1, joueur2, joueur3, c, index);

                DesactivationBouttonJoueur(c);
                DésactiverAvtiverButtonReda(jr, c);
                return true;
            }
            else if (index == 2 && jr.NbCubes == 6 && jr.StartJeux[index] == true)
            {
                jr.StartJeux[index] = false;
                jr.ContreGamer[index] = 0;
                bouton.Location = new Point(plateau[jr.PositionJoueur[index]].Location.X, plateau[jr.PositionJoueur[index]].Location.Y);

                MangerJoueur(joueur0, joueur1, joueur2, joueur3, c, index);

                DesactivationBouttonJoueur(c);
                DésactiverAvtiverButtonReda(jr, c);
                return true;
            }
            else if (index == 3 && jr.NbCubes == 6 && jr.StartJeux[index] == true)
            {
                jr.StartJeux[index] = false;
                jr.ContreGamer[index] = 0;
                bouton.Location = new Point(plateau[jr.PositionJoueur[index]].Location.X, plateau[jr.PositionJoueur[index]].Location.Y);

                MangerJoueur(joueur0, joueur1, joueur2, joueur3, c, index);

                DesactivationBouttonJoueur(c);
                DésactiverAvtiverButtonReda(jr, c);
                return true;
            }
            else
            {
                int[] copie = (int[])jr.ContreGamer.Clone();
                Array.Sort(copie);

                int comparatif = jr.ContreGamer[index] + jr.NbCubes;
                int compare = copie[Array.IndexOf(copie, jr.ContreGamer[index]) + 1]; 
                //MessageBox.Show(compare.ToString());
                if (VerifEspacement(jr, copie) && jr.NbCubes == 6 && Array.Exists(jr.StartJeux, element => element == true)) //vérifie s'il y a des pions dans les 4 dernières cases
                {
                    //MessageBox.Show("case 6 " + jr.ContreGamer[index].ToString());
                    return false; 
                }
                //else if (comparatif >= 52) //annule le dépassement............modif
                //{
                //    return false;
                //}
                else
                {
                    int mouv = jr.ContreGamer[index];
                    if (comparatif >= 52)
                    {
                        jr.ContreGamer[index] = 51;
                    }
                    else
                    {
                        jr.ContreGamer[index] += jr.NbCubes;
                    }
                 
                    jr.PositionJoueur[index] = jr.PositionOnTable[jr.ContreGamer[index]]; //position sur le plateau
                    DesactivationBouttonJoueur(c);
                    //Simulation du mouvement des joueurs sur le plateau
                    #region Simulation du mouvement des joueurs sur le plateau
                    for (int i = mouv ; i <= jr.ContreGamer[index]; i++)
                    {
                        bouton.Location = new Point(plateau[jr.PositionOnTable[i]].Location.X, plateau[jr.PositionOnTable[i]].Location.Y);                       
                        Application.DoEvents();
                        Thread.Sleep(300);
                    }
                    #endregion

                    MangerJoueur(joueur0, joueur1, joueur2, joueur3, c, index);

                    DésactiverAvtiverButtonReda(jr, c);

                    //vérifier si le joueur a gagné
                    int[] copieVictoire = (int[])jr.ContreGamer.Clone(); // copie pour la victoire
                    Array.Sort(copieVictoire);
                    VerifGagnant(copieVictoire, c);

                    return true;
                }
            }
        }      
        private void MangerJoueur(Joueur jr0, Joueur jr1, Joueur jr2, Joueur jr3, char couleur, int index) //Mangez le joueur
        {
            int indexXY = 0, pointX = 0, pointY = 0, compteur = 0;
            bool manger = false;

            if (couleur == 'R')
            {
                for (int i = 0; i <= 3; i++)
                {
                    //si la position de la pièce du joueur en cours de lecture est égale à une position de la pièce d'un autre joueur
                    // et cette figure dans le champ se souvient de son index et prépare la figure à être retirée du champ
                    if (jr0.PositionJoueur[index] == jr1.PositionJoueur[i] && jr1.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 1;
                        manger = true;

                        jr1.ContreGamer[i] = 52;
                        jr1.PositionJoueur[i] = 12;
                        jr1.StartJeux[i] = true;
                        break;
                    }
                    if (jr0.PositionJoueur[index] == jr2.PositionJoueur[i] && jr2.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 2;
                        manger = true;

                        jr2.ContreGamer[i] = 52;
                        jr2.PositionJoueur[i] = 24;
                        jr2.StartJeux[i] = true;
                        break;
                    }
                    if (jr0.PositionJoueur[index] == jr3.PositionJoueur[i] && jr3.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 3;
                        manger = true;

                        jr3.ContreGamer[i] = 52;
                        jr3.PositionJoueur[i] = 36;
                        jr3.StartJeux[i] = true;
                        break;
                    }
                }
            }
            else if (couleur == 'J')
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (jr1.PositionJoueur[index] == jr0.PositionJoueur[i] && jr0.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 0;
                        manger = true;

                        jr0.ContreGamer[i] = 52;
                        jr0.PositionJoueur[i] = 0;
                        jr0.StartJeux[i] = true;
                        break;
                    }
                    if (jr1.PositionJoueur[index] == jr2.PositionJoueur[i] && jr2.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 2;
                        manger = true;

                        jr2.ContreGamer[i] = 52;
                        jr2.PositionJoueur[i] = 24;
                        jr2.StartJeux[i] = true;
                        break;
                    }
                    if (jr1.PositionJoueur[index] == jr3.PositionJoueur[i] && jr3.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 3;
                        manger = true;

                        jr3.ContreGamer[i] = 52;
                        jr3.PositionJoueur[i] = 36;
                        jr3.StartJeux[i] = true;
                        break;
                    }
                }
            }
            else if (couleur == 'B')
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (jr2.PositionJoueur[index] == jr0.PositionJoueur[i] && jr0.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 0;
                        manger = true;

                        jr0.ContreGamer[i] = 52;
                        jr0.PositionJoueur[i] = 0;
                        jr0.StartJeux[i] = true;
                        break;
                    }
                    if (jr2.PositionJoueur[index] == jr1.PositionJoueur[i] && jr1.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 1;
                        manger = true;

                        jr1.ContreGamer[i] = 52;
                        jr1.PositionJoueur[i] = 12;
                        jr1.StartJeux[i] = true;
                        break;
                    }
                    if (jr2.PositionJoueur[index] == jr3.PositionJoueur[i] && jr3.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 3;
                        manger = true;

                        jr3.ContreGamer[i] = 52;
                        jr3.PositionJoueur[i] = 36;
                        jr3.StartJeux[i] = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (jr3.PositionJoueur[index] == jr0.PositionJoueur[i] && jr0.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 0;
                        manger = true;

                        jr0.ContreGamer[i] = 52;
                        jr0.PositionJoueur[i] = 0;
                        jr0.StartJeux[i] = true;
                        break;
                    }
                    if (jr3.PositionJoueur[index] == jr1.PositionJoueur[i] && jr1.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 1;
                        manger = true;

                        jr1.ContreGamer[i] = 52;
                        jr1.PositionJoueur[i] = 12;
                        jr1.StartJeux[i] = true;
                        break;
                    }
                    if (jr3.PositionJoueur[index] == jr2.PositionJoueur[i] && jr2.StartJeux[i] == false)
                    {
                        indexXY = i;
                        compteur = 2;
                        manger = true;

                        jr2.ContreGamer[i] = 52;
                        jr2.PositionJoueur[i] = 24;
                        jr2.StartJeux[i] = true;
                        break;
                    }
                }
            }
            //condition lorsque la figurine est éjectée du jeu
            if (manger)
            {
                //définir l'emplacement correct de la figurine d'un joueur lorsqu'il est retiré du terrain ou lorsqu'il est expulsé du jeu
                if (indexXY == 0)
                {
                    pointX = this.locX0; pointY = this.locY0;
                }
                else if (indexXY == 1)
                {
                    pointX = this.locX1; pointY = this.locY1;
                }
                else if (indexXY == 2)
                {
                    pointX = this.locX2; pointY = this.locY2;
                }
                else if (indexXY == 3)
                {
                    pointX = this.locX3; pointY = this.locY3;
                }

                if (compteur == 0)    //cas où une pièce est jetée hors du joueur 0
                {
                    buttonJ0[indexXY].Location = new Point(pointX, pointY);

                    this.Controls.Remove(buttonJ0[indexXY]);
                    panel0.Controls.Add(buttonJ0[indexXY]);
                    buttonJ0[indexXY].BringToFront();
                }
                else if (compteur == 1)   //cas où une pièce est jetée hors du joueur 1
                {
                    buttonJ1[indexXY].Location = new Point(pointX, pointY);

                    this.Controls.Remove(buttonJ1[indexXY]);
                    panel1.Controls.Add(buttonJ1[indexXY]);
                    buttonJ1[indexXY].BringToFront();
                }
                else if (compteur == 2)   //cas où une pièce est jetée hors du joueur 2
                {
                    buttonJ2[indexXY].Location = new Point(pointX, pointY);

                    this.Controls.Remove(buttonJ2[indexXY]);
                    panel2.Controls.Add(buttonJ2[indexXY]);
                    buttonJ2[indexXY].BringToFront();
                }
                else   //cas où une pièce est jetée hors du joueur 3
                {
                    buttonJ3[indexXY].Location = new Point(pointX, pointY);

                    this.Controls.Remove(buttonJ3[indexXY]);
                    panel3.Controls.Add(buttonJ3[indexXY]);
                    buttonJ3[indexXY].BringToFront();
                }
            }
        }
        private void VerifGagnant(int[] niz, char couleur) // Vérifiez le gagnant
        {
            //if (niz[0] == 48 && niz[1] == 49 && niz[2] == 50 && niz[3] == 51)
            if (niz[0] == 51 && niz[1] == 51 && niz[2] == 51 && niz[3] == 51)
            {
                EcranChoixJoeur start = new EcranChoixJoeur();

                if (couleur == 'R')
                {
                    DesactiverFinPartie();
                    if (MessageBox.Show(this,  lRouge.Text + " a gagné", "Souhaitez vous rejouer ?",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EcranChoixJoeur.nbJoueur = 0;
                        this.Hide();
                        start.Show();
                    }                             
                    else
                    {
                        MessageBox.Show("merci d'avoir participé à cette partie, à bientôt !");
                        Application.Exit();
                    }
                        
                   
                }
                else if (couleur == 'J')
                {
                    DesactiverFinPartie();
                    if (MessageBox.Show(this, lJaune.Text + " a gagné", "Souhaitez vous rejouer ?",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EcranChoixJoeur.nbJoueur = 0;
                        this.Hide();
                        start.Show();
                    }
                    else
                    {
                        MessageBox.Show("merci d'avoir participé à cette partie, à bientôt !");
                        Application.Exit();
                    }
                }
                else if (couleur == 'B')
                {
                    DesactiverFinPartie();
                    if (MessageBox.Show(this, lBleu.Text + " a gagné", "Souhaitez vous rejouer ?",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EcranChoixJoeur.nbJoueur = 0;
                        this.Hide();
                        start.Show();
                    }
                    else
                    {
                        MessageBox.Show("merci d'avoir participé à cette partie, à bientôt !");
                        Application.Exit();
                    }
                }
                else
                {
                    DesactiverFinPartie();
                    if (MessageBox.Show(this, lVert.Text + " a gagné", "Souhaitez vous rejouer ?",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        EcranChoixJoeur.nbJoueur = 0;
                        this.Hide();
                        start.Show();
                    }
                    else
                    {
                        MessageBox.Show("merci d'avoir participé à cette partie, à bientôt !");
                        Application.Exit();
                    }
                }
            }
        }
        private void DesactiverFinPartie() // Désactiver la fin de partie
        {
            buttonReda0.Visible = false;
            buttonReda1.Visible = false;
            buttonReda2.Visible = false;
            buttonReda3.Visible = false;
            pDice.Enabled = false;
        }       
        private void DesactivationBouttonJoueur(char couleur)
        {
            // quand un joueur joue et qu'il n'y a pas de conditions pour tourner à nouveau faire sa désactivation
            if (couleur == 'R')
                DisableButtJoueur(buttonJ0);
            if (couleur == 'J')
                DisableButtJoueur(buttonJ1);
            if (couleur == 'B')
                DisableButtJoueur(buttonJ2);
            if (couleur == 'V')
                DisableButtJoueur(buttonJ3);
        }

        private void bEcouter_Click(object sender, EventArgs e)
        {
            bEcouter.Enabled = bConnecter.Enabled = false;
            bDeconnecter.Enabled = true;
            sClient = null;
            IPAddress ipServeur = AdresseValide(Dns.GetHostName());

            sServeur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sServeur.Bind(new IPEndPoint(ipServeur, 8001));
            sServeur.Listen(1);
            sServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), sServeur);
        }

        private void SurDemandeConnexion(IAsyncResult iAR)
        {
            if (sServeur != null)
            {
                Socket sTmp = (Socket)iAR.AsyncState;
                sClient = sTmp.EndAccept(iAR);
                sClient.Send(Encoding.Unicode.GetBytes("Connexion effectuée par " +
                    ((IPEndPoint)sClient.RemoteEndPoint).Address.ToString()));
                InintialiserReception(sClient);
            }
        }

        private void InintialiserReception(Socket soc)
        {
            soc.BeginReceive(bBuffer, 0, bBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
        }

        private void bConnecter_Click(object sender, EventArgs e)
        {
            if (tbServeur.Text.Length > 0)
            {
                bEcouter.Enabled = bConnecter.Enabled = false;
                bDeconnecter.Enabled = true;
                sClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sClient.Blocking = false;
                IPAddress IPServeur = AdresseValide(tbServeur.Text);
                sClient.BeginConnect(new IPEndPoint(IPServeur, 8001), new AsyncCallback(SurConnexion), sClient);
            }
            else MessageBox.Show("Renseigner le serveur");
        }

        private void SurConnexion(IAsyncResult iAR)
        {
            Socket Tmp = (Socket)iAR.AsyncState;
            if (Tmp.Connected)
                InintialiserReception(Tmp);
            else
                MessageBox.Show("Serveur inaccessible");
        }

        private void SurDemandeDeconnexion(IAsyncResult iAR)
        {
            Socket Tmp = (Socket)iAR.AsyncState;
            Tmp.EndDisconnect(iAR);
        }

        private void bDeconnecter_Click(object sender, EventArgs e)
        {
            if (sServeur == null)
            {
                sClient.Send(Encoding.Unicode.GetBytes("Deconnexion (client)"));
                sClient.Shutdown(SocketShutdown.Both);
                sClient.BeginDisconnect(false, new AsyncCallback(SurDemandeDeconnexion), sClient);
                bEcouter.Enabled = bConnecter.Enabled = true;
                bDeconnecter.Enabled = false;
            }
            else if (sClient == null)
            {
                sServeur.Close();
                bEcouter.Enabled = bConnecter.Enabled = true;
                bDeconnecter.Enabled = false;
                sServeur = null;
            }
        }

        private void Reception(IAsyncResult iAR)
        {
            if (sClient != null)
            {
                Socket Tmp = (Socket)iAR.AsyncState;
                if (Tmp.EndReceive(iAR) > 0)
                {
                    //lbEchange.Items.Insert(0, Encoding.Unicode.GetString(bBuffer));
                    InsererItermThread(Encoding.Unicode.GetString(bBuffer));
                    InintialiserReception(Tmp);
                }
                else
                {
                    Tmp.Disconnect(true);
                    Tmp.Close();
                    if (sServeur != null)
                        sServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), sServeur);
                    sClient = null;
                }
            }
        }

        private void DisableButtJoueur(Button[] buttonDisable)
        {
            for (int i = 0; i <= 3; i++)
                buttonDisable[i].Enabled = false;
        }
        private void DésactiverAvtiverButtonReda(Joueur jr, char couleur)// vérifie qui joue en fonction du dé
        {
            if (jr.NbCubes != 6)     //s'il ne s'agit pas d'un 6, autorisez le transfert de la ligne vers le joueur suivant
                DesactiverButtReda(couleur); 
            else
            {
                pDice.Enabled = true; //sinon laissez le tour sur le même joueur pour tourner à nouveau car elle était un 6 points
                pFinal.BackColor = Color.Green;
            }
        }
        private void DesactiverButtReda(char couleur) //  Désactiver Activer Reda(pion)
        {
            if (couleur == 'R')
            {
                buttonReda0.Visible = true; buttonReda1.Visible = false; buttonReda2.Visible = false; buttonReda3.Visible = false;
            }
            else if (couleur == 'J')
            {
                buttonReda0.Visible = false; buttonReda1.Visible = true; buttonReda2.Visible = false; buttonReda3.Visible = false;
            }
            else if (couleur == 'B')
            {
                buttonReda0.Visible = false; buttonReda1.Visible = false; buttonReda2.Visible = true; buttonReda3.Visible = false;
            }
            else if (couleur == 'V')
            {
                buttonReda0.Visible = false; buttonReda1.Visible = false; buttonReda2.Visible = false; buttonReda3.Visible = true;
            }
        } 
        private void ActiverDesactiver(Joueur iJoueur, Button[] buttonEnable, Button[] buttonDis1, Button[] buttonDis2, Button[] buttonDis3, char couleur) //Activer/desactive les boutons des joueurs 
        {
            int[] copie = (int[])iJoueur.ContreGamer.Clone();
            Array.Sort(copie);  // MessageBox.Show(string.Join(Environment.NewLine, copie));

            for (int i = 0; i <= 3; i++)
            {
                if (iJoueur.NbCubes == 6)
                {
                    if (VerifEspacement(iJoueur, copie) && VerifPionJoueur(iJoueur, iJoueur.StartJeux))
                    {
                        DesactivationBouttonJoueur(couleur);
                        pDice.Enabled = true;
                        pFinal.BackColor = Color.Green;
                        goto END;
                    }
                    else if (Array.Exists(iJoueur.ContreGamer, element => element == 0))
                    {
                        if (iJoueur.StartJeux[i] == false) // s'il s'agit d'un démarrage à 6 chiffres et occupé, n'autorisez que ceux qui sont sur le terrain
                            buttonEnable[i].Enabled = true;
                    }
                    else if (!Array.Exists(iJoueur.ContreGamer, element => element == 0))
                        buttonEnable[i].Enabled = true;   //si c'est un 6 et qu'il n'est pas occupé l'initiale autorise tout le monde
                }
                else if (iJoueur.NbCubes != 6)
                {
                    if (VerifEspacement(iJoueur, copie))
                    {
                        DesactivationBouttonJoueur(couleur);
                        DesactiverButtReda(couleur);
                        break;
                    }
                    else if (iJoueur.StartJeux[i] == false) //sinon 6 laissez seulement ceux sur le terrain
                        buttonEnable[i].Enabled = true;
                    else if (iJoueur.StartJeux[0] == true && iJoueur.StartJeux[1] == true && iJoueur.StartJeux[2] == true && iJoueur.StartJeux[3] == true)
                    {
                        DesactiverButtReda(couleur);  //si ce n'est pas un 6 points et que tout le monde dans la boîte interdit à tout le monde le joueur qui joue
                        break;
                    }
                }
            }

            pDice.Enabled = false;
            pFinal.BackColor = Color.Red;

            END:;
        } 
        private bool VerifEspacement(Joueur jr, int[] niz) // Vérifier l'espacement
        {
            // vérifie la distance entre les joueurs pour détecter la possibilité de mouvement,
            // si les chiffres d'un joueur ne peuvent pas bouger renvoie vrai
            /*for (int i = 0; i <= 3; i++)
                if (niz[i] + jr.NbCubes < niz[i + 1])
                    return false;
            MessageBox.Show("je suis ici");
            return true;*/
            return false;
        }
        private bool VerifPionJoueur(Joueur jr, bool[] niz) // Vérifiez tout sur le terrain
        {
            // vérifie si toutes les pièces d'un joueur sont sur le terrain, si elles sont vraies
            for (int i = 0; i <= 3; i++)
                if (niz[i] == true)
                    return false;

            return true;
        }
        private void DefNomJoueur(string nomJoueur, Label lNomJoueur) // Définir les noms de joueurs recupéré à EcranChoixJoeur
        {
            for (int i = 0; i <= nomJoueur.Length - 1; i++)
            {
                if (i == nomJoueur.Length - 1)
                    lNomJoueur.Text += nomJoueur[i].ToString();
                else
                    lNomJoueur.Text += nomJoueur[i].ToString() + "\n";
            }
        }
        private void DefFigure(int[] figNiz) // Définissez les chiffres
        {
            int position = 0;
            for (int i = 0; i < figNiz.Length; i++)
            {
                if (i == 0)
                    position = 0;
                else if (i == 1)
                    position = 4;
                else if (i == 2)
                    position = 8;
                else if (i == 3)
                    position = 12;

                if (figNiz[i] != -1)
                    for (int j = 0; j < xButton.Length; j++)
                        xButton[i][j].BackgroundImage = xBitmap[figNiz[i] + position][j];
            }
        }
        private void EcranPlateau_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "Vérifiez que vous avez sauvé votre partie", "Souhaitez vous quitter ?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                e.Cancel = false; 
                Application.Exit();
            }
            else
                e.Cancel = true;
        }
        private void bQuitter_Click(object sender, EventArgs e)
        {
            Close(); Application.Exit();
        }
        private void bApropos_Click(object sender, EventArgs e)
        {
            FicApropos t = new FicApropos();
            t.Owner = this;
            t.ShowDialog();
        }
        private void bSave_Click(object sender, EventArgs e)
        {
            string texte = " ";
            DialogResult dr = this.SFD.ShowDialog();
            
            if (dr == DialogResult.OK)
            {
                string path = this.SFD.FileName + ".txt";
                texte = lVert.Text + "\n ";
                texte += lRouge.Text + "\n ";
                texte += lBleu.Text + "\n ";
                texte += lJaune.Text + "\n ";
                texte += bR0.Location.X.ToString() + "\n ";
                texte += bR0.Location.Y.ToString() + "\n ";
                texte += bR1.Location.X.ToString() + "\n ";
                texte += bR1.Location.Y.ToString() + "\n ";
                texte += bR2.Location.X.ToString() + "\n ";
                texte += bR2.Location.Y.ToString() + "\n ";
                texte += bR3.Location.X.ToString() + "\n ";
                texte += bR3.Location.Y.ToString() + "\n ";
                texte += bJ0.Location.X.ToString() + "\n ";
                texte += bJ0.Location.Y.ToString() + "\n ";
                texte += bJ1.Location.X.ToString() + "\n ";
                texte += bJ1.Location.Y.ToString() + "\n ";
                texte += bJ2.Location.X.ToString() + "\n ";
                texte += bJ2.Location.Y.ToString() + "\n ";
                texte += bJ3.Location.X.ToString() + "\n ";
                texte += bJ3.Location.Y.ToString() + "\n ";
                texte += bB0.Location.X.ToString() + "\n ";
                texte += bB0.Location.Y.ToString() + "\n ";
                texte += bB1.Location.X.ToString() + "\n ";
                texte += bB1.Location.Y.ToString() + "\n ";
                texte += bB2.Location.X.ToString() + "\n ";
                texte += bB2.Location.Y.ToString() + "\n ";
                texte += bB3.Location.X.ToString() + "\n ";
                texte += bB3.Location.Y.ToString() + "\n ";
                texte += bV0.Location.X.ToString() + "\n ";
                texte += bV0.Location.Y.ToString() + "\n ";
                texte += bV1.Location.X.ToString() + "\n ";
                texte += bV1.Location.Y.ToString() + "\n ";
                texte += bV2.Location.X.ToString() + "\n ";
                texte += bV2.Location.Y.ToString() + "\n ";
                texte += bV3.Location.X.ToString() + "\n ";
                texte += bV3.Location.Y.ToString() + "\n ";


                File.WriteAllText(path, texte);
                #region Recupere le nom enregisté
                string result = Path.GetFileName(path);
                var replacement0 = path.Replace(result, "joueur0.xml");
                var replacement1 = path.Replace(result, "joueur1.xml");
                var replacement2 = path.Replace(result, "joueur2.xml");
                var replacement3 = path.Replace(result, "joueur3.xml");
                #endregion
                
                joueur0.save(replacement0); joueur1.save(replacement1); joueur2.save(replacement2); joueur3.save(replacement3);
            }

        }
        private void bOuvrir_Click(object sender, EventArgs e)
        {
            string[] text = null;
            DialogResult dr = OFD.ShowDialog();

            if (dr == DialogResult.OK)
            {         
                            
                string path = OFD.FileName;
                string doc = File.ReadAllText(path);
                #region Recupere le nom enregisté
                string result = Path.GetFileName(path);
                var replacement0 = path.Replace(result, "joueur0.xml");
                var replacement1 = path.Replace(result, "joueur1.xml");
                var replacement2 = path.Replace(result, "joueur2.xml");
                var replacement3 = path.Replace(result, "joueur3.xml");
                #endregion

                joueur0 = joueur0.load(replacement0);
                joueur1 = joueur1.load(replacement1);
                joueur2 = joueur2.load(replacement2);
                joueur3 = joueur3.load(replacement3);

                text = doc.Split(' ');
                lVert.Text = text[0];
                lRouge.Text = text[1];
                lBleu.Text = text[2];
                lJaune.Text = text[3];
                #region placer les pions 
                if (bR0.Location.X != int.Parse(text[4]) || bR0.Location.Y != int.Parse(text[5]))
                {
                    bR0.Parent.Controls.Remove(bR0);
                    bR0.Location = new Point(int.Parse(text[4]), int.Parse(text[5]));
                    this.Controls.Add(bR0); bR0.BringToFront();
                }
                if (bR1.Location.X != int.Parse(text[6]) || bR1.Location.Y != int.Parse(text[7]))
                {
                    bR1.Parent.Controls.Remove(bR1);
                    bR1.Location = new Point(int.Parse(text[6]), int.Parse(text[7]));
                    this.Controls.Add(bR1); bR1.BringToFront();
                }
                if (bR2.Location.X != int.Parse(text[8]) || bR2.Location.Y != int.Parse(text[9]))
                {
                    bR2.Parent.Controls.Remove(bR2);
                    bR2.Location = new Point(int.Parse(text[8]), int.Parse(text[9]));
                    this.Controls.Add(bR2); bR2.BringToFront();
                }
                if (bR3.Location.X != int.Parse(text[10]) || bR3.Location.Y != int.Parse(text[11]))
                {
                    bR3.Parent.Controls.Remove(bR3);
                    bR3.Location = new Point(int.Parse(text[10]), int.Parse(text[11]));
                    this.Controls.Add(bR3); bR3.BringToFront();
                }
                if (bJ0.Location.X != int.Parse(text[12]) || bJ0.Location.Y != int.Parse(text[13]))
                {
                    bJ0.Parent.Controls.Remove(bJ0);
                    bJ0.Location = new Point(int.Parse(text[12]), int.Parse(text[13]));
                    this.Controls.Add(bJ0); bJ0.BringToFront();
                }
                if (bJ1.Location.X != int.Parse(text[14]) || bJ1.Location.Y != int.Parse(text[15]))
                {
                    bJ1.Parent.Controls.Remove(bJ1);
                    bJ1.Location = new Point(int.Parse(text[14]), int.Parse(text[15]));
                    this.Controls.Add(bJ1); bJ1.BringToFront();
                }
                if (bJ2.Location.X != int.Parse(text[16]) || bJ2.Location.Y != int.Parse(text[17]))
                {
                    bJ2.Parent.Controls.Remove(bJ2);
                    bJ2.Location = new Point(int.Parse(text[16]), int.Parse(text[17]));
                    this.Controls.Add(bJ2); bJ2.BringToFront();
                }
                if (bJ3.Location.X != int.Parse(text[18]) || bJ3.Location.Y != int.Parse(text[19]))
                {
                    bJ3.Parent.Controls.Remove(bJ3);
                    bJ3.Location = new Point(int.Parse(text[18]), int.Parse(text[19]));
                    this.Controls.Add(bJ3); bJ3.BringToFront();
                }
                if (bB0.Location.X != int.Parse(text[20]) || bB0.Location.Y != int.Parse(text[21]))
                {
                    bB0.Parent.Controls.Remove(bB0);
                    bB0.Location = new Point(int.Parse(text[20]), int.Parse(text[21]));
                    this.Controls.Add(bB0); bB0.BringToFront();
                }
                if (bB1.Location.X != int.Parse(text[22]) || bB1.Location.Y != int.Parse(text[23]))
                {
                    bB1.Parent.Controls.Remove(bB1);
                    bB1.Location = new Point(int.Parse(text[22]), int.Parse(text[23]));
                    this.Controls.Add(bB1); bB1.BringToFront();
                }
                if (bB2.Location.X != int.Parse(text[24]) || bB2.Location.Y != int.Parse(text[25]))
                {
                    bB2.Parent.Controls.Remove(bB2);
                    bB2.Location = new Point(int.Parse(text[24]), int.Parse(text[25]));
                    this.Controls.Add(bB2); bB2.BringToFront();
                }
                if (bB3.Location.X != int.Parse(text[26]) || bB3.Location.Y != int.Parse(text[27]))
                {
                    bB3.Parent.Controls.Remove(bB3);
                    bB3.Location = new Point(int.Parse(text[26]), int.Parse(text[27]));
                    this.Controls.Add(bB3); bB3.BringToFront();
                }
                if (bV0.Location.X != int.Parse(text[28]) || bV0.Location.Y != int.Parse(text[29]))
                {
                    bV0.Parent.Controls.Remove(bV0);
                    bV0.Location = new Point(int.Parse(text[28]), int.Parse(text[29]));
                    this.Controls.Add(bV0); bV0.BringToFront();
                }
                if (bV1.Location.X != int.Parse(text[30]) || bV1.Location.Y != int.Parse(text[31]))
                {
                    bV1.Parent.Controls.Remove(bV1);
                    bV1.Location = new Point(int.Parse(text[30]), int.Parse(text[31]));
                    this.Controls.Add(bV1); bV1.BringToFront();
                }
                if (bV2.Location.X != int.Parse(text[32]) || bV2.Location.Y != int.Parse(text[33]))
                {
                    bV2.Parent.Controls.Remove(bV2);
                    bV2.Location = new Point(int.Parse(text[32]), int.Parse(text[33]));
                    this.Controls.Add(bV2); bV2.BringToFront();
                }
                if (bV3.Location.X != int.Parse(text[34]) || bV3.Location.Y != int.Parse(text[35]))
                {
                    bV3.Parent.Controls.Remove(bV3);
                    bV3.Location = new Point(int.Parse(text[34]), int.Parse(text[35]));
                    this.Controls.Add(bV3); bV3.BringToFront();
                }
                #endregion
            }
        }
        private void EcranPlateau_KeyDown(object sender, KeyEventArgs e)//forcerle dé à 6 pour sortir
        {
            if (e.KeyCode == Keys.A)
            {

                if (startStop == false)
                {
                    if (bottonDice == 0)
                    {
                        startStop = true;
                        joueur0.NbCubes = 6;
                        pDice.Image = Properties.Resources.dice6;
                        timerDice.Stop();
                        if (bottonDice == 0 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur0, buttonJ0, buttonJ1, buttonJ2, buttonJ3, 'R');
                    }

                    else if (bottonDice == 1)
                    {
                        startStop = true;
                        joueur1.NbCubes = 6;
                        pDice.Image = Properties.Resources.dice6;
                        timerDice.Stop();
                        if (bottonDice == 1 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur1, buttonJ1, buttonJ0, buttonJ2, buttonJ3, 'J');
                    }

                    else if (bottonDice == 2)
                    {
                        startStop = true;
                        joueur2.NbCubes = 6;
                        pDice.Image = Properties.Resources.dice6;
                        timerDice.Stop();
                        if (bottonDice == 2 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur2, buttonJ2, buttonJ0, buttonJ1, buttonJ3, 'B');
                    }
                    else
                    {
                        startStop = true;
                        joueur3.NbCubes = 6;
                        pDice.Image = Properties.Resources.dice6;
                        timerDice.Stop();
                        if (bottonDice == 3 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur3, buttonJ3, buttonJ0, buttonJ1, buttonJ2, 'V');
                    }
                }                
            }
            else if (e.KeyCode == Keys.Z)
            {

                if (startStop == false)
                {
                    if (bottonDice == 0)
                    {
                        startStop = true;
                        joueur0.NbCubes = 1;
                        pDice.Image = Properties.Resources.dice1;
                        timerDice.Stop();
                        if (bottonDice == 0 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur0, buttonJ0, buttonJ1, buttonJ2, buttonJ3, 'R');
                    }

                    else if (bottonDice == 1)
                    {
                        startStop = true;
                        joueur1.NbCubes = 1;
                        pDice.Image = Properties.Resources.dice1;
                        timerDice.Stop();
                        if (bottonDice == 1 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur1, buttonJ1, buttonJ0, buttonJ2, buttonJ3, 'J');
                    }

                    else if (bottonDice == 2)
                    {
                        startStop = true;
                        joueur2.NbCubes = 1;
                        pDice.Image = Properties.Resources.dice1;
                        timerDice.Stop();
                        if (bottonDice == 2 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur2, buttonJ2, buttonJ0, buttonJ1, buttonJ3, 'B');
                    }
                    else
                    {
                        startStop = true;
                        joueur3.NbCubes = 1;
                        pDice.Image = Properties.Resources.dice1;
                        timerDice.Stop();
                        if (bottonDice == 3 && timerDice.Enabled == false)
                            ActiverDesactiver(joueur3, buttonJ3, buttonJ0, buttonJ1, buttonJ2, 'V');
                    }
                }
            }
        }
    }
}
