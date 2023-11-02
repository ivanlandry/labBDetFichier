using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.WindowsAppSDK.Runtime.Packages;

namespace labBDetFichier
{
    class Singleton
    {
        ObservableCollection<Materiel> materiels;
        static Singleton instance = null;
         Window window = null;
        static MySqlConnection con = null;
        public Singleton()
        {
            materiels = new ObservableCollection<Materiel> ();
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2023_420326_gr02_2204463-ivan-landry-pombo-chedjou;Uid=2204463;Pwd=2204463;");
        }
       
        public static Singleton getInstance()
        {
            if (instance ==null)
                instance = new Singleton();
            return instance;
        }

        public  Window getWindow()
        {
            return window;
        }

        public void setWindow(Window _window)
        {
            window = _window;
        }

        public ObservableCollection<Materiel> getListMateriel()
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "select * from materiels";

            con.Open();

            MySqlDataReader r = commande.ExecuteReader();

            materiels.Clear();

            while (r.Read())
            {
                materiels.Add(new Materiel(r["code"].ToString(), r["modele"].ToString(), r["meuble"].ToString(), r["categorie"].ToString(), r["couleur"].ToString(), Convert.ToDouble(r["prix"])));
            }

            con.Close();

            return materiels;
        } 

        public void ajouter(Materiel materiel)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "insert into materiels(code,modele,meuble,categorie,couleur,prix) values(@code,@modele,@meuble,@categorie,@couleur,@prix)";

            commande.Parameters.AddWithValue("@code", materiel.Code);
            commande.Parameters.AddWithValue("@modele", materiel.Modele);
            commande.Parameters.AddWithValue("@meuble", materiel.Meuble);
            commande.Parameters.AddWithValue("@categorie", materiel.Categorie);
            commande.Parameters.AddWithValue("@couleur", materiel.Couleur);
            commande.Parameters.AddWithValue("@prix", materiel.Prix);

            con.Open();
            commande.Prepare();
            commande.ExecuteNonQuery();
            con.Close();
        }

        public void modifier(Materiel materiel,string ancien_code)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "update materiels set code=@code,modele=@modele,meuble=@meuble,categorie=@categorie,couleur=@couleur,prix=@prix where code=@ancien_code";

                commande.Parameters.AddWithValue("@code", materiel.Code);
                commande.Parameters.AddWithValue("@modele", materiel.Modele);
                commande.Parameters.AddWithValue("@meuble", materiel.Meuble);
                commande.Parameters.AddWithValue("@categorie", materiel.Categorie);
                commande.Parameters.AddWithValue("@couleur", materiel.Couleur);
                commande.Parameters.AddWithValue("@prix", materiel.Prix);
                commande.Parameters.AddWithValue("@ancien_code", ancien_code);

                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
               Console.WriteLine(ex);
            }
            con.Close();
        }
    }
}
