using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labBDetFichier
{
    class Materiel
    {
        private string code;
        private string modele;
        private string meuble;
        private string categorie;
        private string couleur;
        private double prix;

        public Materiel(string code, string modele, string meuble, string categorie, string couleur, double prix)
        {
            this.code = code;
            this.modele = modele;
            this.meuble = meuble;
            this.categorie = categorie;
            this.couleur = couleur;
            this.prix = prix;
        }

        public string Code { get => code; set => code = value; }
        public string Modele { get => modele; set => modele = value; }
        public string Meuble { get => meuble; set => meuble = value; }
        public string Categorie { get => categorie; set => categorie = value; }
        public string Couleur { get => couleur; set => couleur = value; }
        public double Prix { get => prix; set => prix = value; }

        public override string ToString()
        {
            return $"{Code};{modele};{meuble};{categorie};{couleur};{prix}";
        }
    }
}
