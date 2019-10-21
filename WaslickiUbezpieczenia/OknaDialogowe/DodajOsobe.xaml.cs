using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WaslickiUbezpieczenia.Klasy;

namespace WaslickiUbezpieczenia.OknaDialogowe {
    public partial class DodajOsobe : Window {
        #region Zmienne

        public UbezpieczeniaString UbezpieczenieReturn { get; private set; }
        public bool Status { get; private set; }

        #endregion

        #region Konstruktory

        public DodajOsobe() {
            InitializeComponent();
            var centrowanie = new Pozycjonowanie_okna_dialogowego(this);
        }

        #endregion

        #region Metody


        #endregion


        #region Obsługa pozostałych elementów UI

        private void Zatwierdz_bt_Click(object sender, RoutedEventArgs e) {
            var o = new UbezpieczeniaString {
                Imie            = Imie_tb.Text,
                Nazwisko        = Nazwisko_tb.Text,
                NumerTelefonu   = Telefon_tb.Text,
                Opis            = Opis_tb.Text,
                DataRozpoczecia = Data_rozpoczecia_tb.SelectedDate?.ToString("yyyy-MM-dd"),
                DataZakonczenia = Data_zakonczenia_tb.SelectedDate?.ToString("yyyy-MM-dd"),
                Firma           = Firma_tb.Text,
                Skladka         = Skladka_tb.Text,
                Szyfrowane      = false
            };

            UbezpieczenieReturn = o;
            Status = true;
            Close();
        }

        private void Anuluj_bt_Click(object sender, RoutedEventArgs e) {
            Status = false;
            Close();
        }

        #endregion


        private void Skladka_tb_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
            if (!(sender is TextBox tb)) return;
            if (!(tb.Text + e.Text).CanParse<decimal>())
                e.Handled = true;
        }
    }
}