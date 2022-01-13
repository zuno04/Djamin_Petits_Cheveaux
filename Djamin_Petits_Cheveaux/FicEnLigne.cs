using System;

using System.Windows.Forms;
using EngineIOSharp.Common.Enum;
using Newtonsoft.Json.Linq;
using SocketIOSharp.Client;
using SocketIOSharp.Common;
using SocketIOSharp.Server;
using SocketIOSharp.Server.Client;

namespace Djamin_Petits_Cheveaux
{
    public partial class FicEnLigne : Form
    {
        public SocketIOServer sServeur; 
        public SocketIOClient sClient;
        private Byte[] bBuffer;
        public static string rouge = "Joueur 1", jaune = "Joueur 2", bleu = "", vert = "";
        public static int nbJoueur = 0;

        
        public FicEnLigne()
        {
            InitializeComponent();

            sServeur = null;
            sClient = null;
            bBuffer = new Byte[1024];

        }
        
        private void bEcouter_Click(object sender, EventArgs e)
        {
            bEcouter.Enabled = bConnecter.Enabled = false;
            bDeconnecter.Enabled = true;
            sClient = null;

            sServeur = new SocketIOServer(new SocketIOServerOption(8007));
            sServeur.Start();

            sServeur.OnConnection((SocketIOSocket socket) =>
            {
                Console.WriteLine("Client connected!");

                socket.On("input", (Data) =>
                {
                    foreach (JToken Token in Data)
                    {
                        Console.Write(Token + " ");
                    }

                    Console.WriteLine();
                    socket.Emit("echo", Data);
                });

                socket.On(SocketIOEvent.DISCONNECT, () =>
                {
                    Console.WriteLine("Client disconnected!");
                });
            });

        }     
        private void bConnecter_Click(object sender, EventArgs e)
        {
            if (tbServeur.Text.Length > 0)
            {
                bEcouter.Enabled = bConnecter.Enabled = false;
                bDeconnecter.Enabled = true;

                sClient = new SocketIOClient(new SocketIOClientOption(EngineIOScheme.http, "localhost", 8007));

                sClient.Connect();

                sClient.On("connection", () =>
                {
                    Console.WriteLine("Connected!");
                });

                sClient.On("disconnect", () =>
                {
                    Console.WriteLine("Disconnected!");
                });

            }
            else MessageBox.Show("Renseigner le serveur");
        }      

        private void bDeconnecter_Click(object sender, EventArgs e)
        {
            if (sServeur == null)
            {
                sClient.Close();
                bEcouter.Enabled = bConnecter.Enabled = true;
                bDeconnecter.Enabled = false;
            }
            else if (sClient == null)
            {
                sServeur.Stop();
                bEcouter.Enabled = bConnecter.Enabled = true;
                bDeconnecter.Enabled = false;
                sServeur = null;
            }
        }
        private void bDemarrer_Click(object sender, EventArgs e)
        {
            //sServeur.Send(Encoding.Unicode.GetBytes("Jeu lancé"));
            EcranPlateau plateau = new EcranPlateau();
            nbJoueur = 1;
            this.Hide();
            plateau.Show();
        }

        


    }
}
