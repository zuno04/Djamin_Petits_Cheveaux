using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace Djamin_Petits_Cheveaux
{
    public class Joueur
    {
        [DataMember]
        int nbCubes; //nombreCubes
        [DataMember]
        int[] contreGamer, positionJoueur, positionOnTable;
        [DataMember]
        bool[] startJeux;
        /*[DataMember]
        bool[] helpParking;*/
        [DataMember]
        Random Aléatoire;

        public void save(string filename) //Deserialize
        {
            using (var stream = new FileStream(filename, FileMode.Create))
            {
                var XML = new XmlSerializer(typeof(Joueur));
                XML.Serialize(stream, this);
                stream.Close();
            }
        }
        public Joueur load(string filename) //Deserialize
        {
            //string filePath = Path.GetFullPath(filename);
            //string filePath =@filename;

            Joueur joueur_charge;

            XmlSerializer serializer = new XmlSerializer(typeof(Joueur));

            using (Stream reader = new FileStream(filename, FileMode.Open))
            {
                // Call the Deserialize method to restore the object's state.
                joueur_charge = (Joueur)serializer.Deserialize(reader);

                reader.Close();
                return joueur_charge;
            }
        }
        public Joueur()
        {
            contreGamer = new int[5] { 52, 52, 52, 52, 52 };//contre Joueur
            positionJoueur = new int[4] { 0, 0, 0, 0 };//positionJoueur

            startJeux = new bool[4] { true, true, true, true };
            //helpParking = new bool[4] { true, true, true, true };//Aide au stationnement
            Aléatoire = new Random(); 
        }
        public int NbCubes //Numéro de cube
        {
            set { nbCubes = value; }
            get { return nbCubes; }
        }
        public bool[] StartJeux // Démarrer les jeux
        {
            set { startJeux = value; }
            get { return startJeux; }
        }
        /*public bool[] HelpParking //Aide au stationnement
        {
            set { helpParking = value; }
            get { return helpParking; }
        }*/
        public int[] ContreGamer //Contre-joueur
        {
            set { contreGamer = value; }
            get { return contreGamer; }
        }
        public int[] PositionJoueur //Joueur de position
        {
            set { positionJoueur = value; }
            get { return positionJoueur; }
        }
        public int[] PositionOnTable //Position sur la table
        {
            set { positionOnTable = value; }
            get { return positionOnTable; }
        }
        public void SimulRotation(PictureBox pictBox, Image[] image) //Simulation de rotation
        {
            nbCubes = Aléatoire.Next(1, 7); 
            pictBox.Image = image[nbCubes];
        }
    }
}
