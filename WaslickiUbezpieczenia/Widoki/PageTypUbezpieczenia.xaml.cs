using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WaslickiUbezpieczenia.Klasy;

namespace WaslickiUbezpieczenia.Widoki {
    public partial class PageTypUbezpieczenia : Page {
        public PageTypUbezpieczenia() {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
            if(!(sender is Button przycisk)) return;

            switch (przycisk.Tag) {
                case "Domy":
                    Debug.WriteLine("Otwieram ubezpieczenia domów");
                    NavigationService?.Navigate(new PageDane(TypUbezpieczenia.Domy));
                    break;
                case "Samochody":
                    Debug.WriteLine("Otwieram ubezpieczenia samochodów");
                    NavigationService?.Navigate(new PageDane(TypUbezpieczenia.Samochody));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Pliki_OnClick(object sender, RoutedEventArgs e) => Pliki.OtworzSciezkeBazy();
    }
}
