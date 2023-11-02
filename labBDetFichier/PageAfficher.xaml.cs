using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace labBDetFichier
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAfficher : Page
    {
        private ObservableCollection<Materiel> materiaux;
        public PageAfficher()
        {
            this.InitializeComponent();
            materiaux = new ObservableCollection<Materiel>();

            this.materiaux = Singleton.getInstance().getListMateriel();
            gdvMateriaux.ItemsSource = this.materiaux;
        }


        private async void load_fichier_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.getInstance().getWindow());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            if (monFichier != null)
            {
                //ouvre le fichier et lit le contenu
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                /*boucle permettant de lire chacune des lignes du fichier
                * et de remplir une liste d'objets de type CLient
                */
                foreach (var ligne in lignes)
                {
                    var v = ligne.Split(";");

                    Singleton.getInstance().ajouter(new Materiel(v[0], v[1], v[2], v[3], v[4], Convert.ToDouble(v[5])));
                }

                //on peut mettre la liste de Clients comme source d'une listVie
                this.materiaux = Singleton.getInstance().getListMateriel();
                gdvMateriaux.ItemsSource = this.materiaux;
            }
        }

        private async void exporter_Click(object sender, RoutedEventArgs e)
        {

            var picker = new Windows.Storage.Pickers.FileSavePicker();

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(Singleton.getInstance().getWindow());
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);

            if (nom_fichier.Text != "")
            {
                picker.SuggestedFileName = nom_fichier.Text;
            }
            else
            {
                picker.SuggestedFileName = "materiaux";
            }
         
            picker.FileTypeChoices.Add("Fichier csv", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

           if(monFichier != null)
            {
                List<Materiel> liste = new List<Materiel>();

                foreach (Materiel item in this.materiaux)
                {
                    liste.Add(item);
                }

                // La fonction ToString de la classe Client retourne: nom + ";" + prenom

                await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);
            }

        }
    }
}
