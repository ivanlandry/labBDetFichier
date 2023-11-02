using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace labBDetFichier
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouter : Page
    {
        private Materiel materiel;
        public PageAjouter()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is not null)
            {
                this.materiel = e.Parameter as Materiel;

                titre_page.Text = "Modifer le materiel";

                code.Text = materiel.Code;
                modele.Text = materiel.Modele;
                meuble.Text = materiel.Meuble;
                categorie.Text = materiel.Categorie;
                couleur.Text = materiel.Couleur;
                prix.Text = materiel.Prix.ToString();

                enregistrer.Content = "Modifier";
            }
        }

        private async void enregistrer_Click(object sender, RoutedEventArgs e)
        {
            string _code, _modele, _meuble, _categorie, _couleur, _prix;
            _code = code.Text;
            _modele = modele.Text;
            _meuble = meuble.Text;
            _couleur = couleur.Text;
            _prix = prix.Text;
            _categorie = categorie.Text;

            if (_code == "")
                erreurCode.Text = "le code est requis";
            else
            {
                if(_code.Length!=3)
                    erreurCode.Text = "le code doit etre de 3 chiffres";
                else
                {
                    try
                    {
                       int code = Convert.ToInt32(_code);
                        erreurCode.Text = "";
                    }
                    catch(Exception exception)
                    {
                        erreurCode.Text = "nombre invalide";
                    }
                }
                    
            }

            if (_modele == "")
                erreurModele.Text = "le modele est requis";
            else
                erreurModele.Text = "";

            if (_meuble == "")
                erreurMeuble.Text = "le meuble est requis";
            else
                erreurMeuble.Text = "";

            if (_couleur == "")
                erreurCouleur.Text = "la couleur est requis";
            else
                erreurCouleur.Text = "";

            if (_prix == "")
                erreurPrix.Text = "le prix est requis";
            else
            {
                try
                {
                    double prix = Convert.ToDouble(_prix);
                    erreurPrix.Text = "";
                }
                catch(Exception exception)
                {
                    erreurPrix.Text = "prix invalide";
                }
            }

            if (_categorie == "")
                erreurCategorie.Text = "la categorie est requise";
            else
                erreurCategorie.Text = "";

            if(erreurCode.Text=="" && erreurModele.Text=="" && erreurMeuble.Text=="" && erreurCategorie.Text=="" && erreurCouleur.Text=="" && erreurPrix.Text == "")
            {
                Materiel m = new Materiel(_code,_modele,_meuble,_categorie,_couleur,Convert.ToDouble(_prix));

                if (this.materiel == null)
                {
                    Singleton.getInstance().ajouter(m);
                }
                else
                {
                    Singleton.getInstance().modifier(m,this.materiel.Code);
                }
                this.clearField();

                ContentDialog dialog = new ContentDialog();
                dialog.XamlRoot = main.XamlRoot;
                dialog.Title = "Succes";
                dialog.CloseButtonText = "OK";
                dialog.Content = "materiel enregistré";

                var result = await dialog.ShowAsync();
            }
        }

        public void clearField()
        {
            code.Text = "";
            modele.Text = "";
            meuble.Text = "";
            categorie.Text = "";
            couleur.Text = "";
            prix.Text = "";
        }
    }
}
