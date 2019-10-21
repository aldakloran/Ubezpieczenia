using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using WaslickiUbezpieczenia.Widoki;

namespace WaslickiUbezpieczenia {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            MainFrame.Navigate(new PageTypUbezpieczenia());
            Closed += (sender, args) => Environment.Exit(0);
        }


        private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
            Powiadomienia.Dispose();

            if (!(MainFrame.Content is IZapis trzebaZapisac)) return;
            trzebaZapisac.Zapisz();
            Debug.WriteLine("Zakończono zapisywanie danych");
        }
    }
}
