using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Djamin_Petits_Cheveaux
{
    public partial class FicEnLigne : Form
    {
        private Socket sServeur, sClient;
        private Byte[] bBuffer;
        public static string rouge = "Joueur 1", jaune = "Joueur 2", bleu = "", vert = "";
        public static int nbJoueur = 0;

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
        public FicEnLigne()
        {
            InitializeComponent();

            sServeur = null;
            sClient = null;
            bBuffer = new Byte[1024];

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
            bDemarrer.Visible = true;
        }

        private void SurDemandeConnexion(IAsyncResult iAR)
        {
            if (sServeur != null)
            {
                Socket sTmp = (Socket)iAR.AsyncState;
                sClient = sTmp.EndAccept(iAR);
                sClient.Send(Encoding.Unicode.GetBytes("Connexion effectuée par " +
                    ((IPEndPoint)sClient.RemoteEndPoint).Address.ToString()));
                MessageBox.Show("sdedddddddd");
                

                InintialiserReception(sClient);
            }
        }

        private void InintialiserReception(Socket soc)
        {
            soc.BeginReceive(bBuffer, 0, bBuffer.Length, SocketFlags.None, new AsyncCallback(Reception), soc);
            //sServeur.Send(Encoding.Unicode.GetBytes("Client Connecté"));
            bDemarrer.Enabled = true;
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
            {
                InintialiserReception(Tmp);                
            }                
            else
                MessageBox.Show("Serveur inaccessible");
        }

        private void SurDemandeDeconnexion(IAsyncResult iAR)
        {
            Socket Tmp = (Socket)iAR.AsyncState;
            Tmp.EndDisconnect(iAR);
            //bDemarrer.Enabled = false;
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

        private void bDemarrer_Click(object sender, EventArgs e)
        {
            sClient.Send(Encoding.Unicode.GetBytes("Jeu lancé"));
            EcranPlateau plateau = new EcranPlateau();
            nbJoueur = 1;
            this.Hide();
            plateau.Show();
        }

        private void Reception(IAsyncResult iAR)
        {
            if (sClient != null)
            {
                Socket Tmp = (Socket)iAR.AsyncState;
                if (Tmp.EndReceive(iAR) > 0)
                {                   
                    InsererItermThread(Encoding.Unicode.GetString(bBuffer));
                    InintialiserReception(Tmp);
                }
                else
                {
                    Tmp.Disconnect(true);
                    Tmp.Close();
                    if (sServeur != null)
                    {
                        sServeur.BeginAccept(new AsyncCallback(SurDemandeConnexion), sServeur);
                        //bDemarrer.Enabled = true;
                    }
                    sClient = null;
                }
            }
        }


    }
}
