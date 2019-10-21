using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Notifications.Wpf;
using WaslickiUbezpieczenia.Klasy;
using WaslickiUbezpieczenia.OknaDialogowe;

namespace WaslickiUbezpieczenia.Widoki {
    public partial class PageDane : Page, IZapis {
        #region Zmienne

        private readonly CollectionViewSource ViewSource = new CollectionViewSource();
        private readonly ObservableCollection<Ubezpieczenie> DaneWyswietl = new ObservableCollection<Ubezpieczenie>();

        public TypUbezpieczenia Typ { get; private set; }

        #endregion

        #region Konstruktory

        public PageDane(TypUbezpieczenia typ) {
            InitializeComponent();

            Typ = typ;

            ViewSource.Source = DaneWyswietl;
            DaneDg.ItemsSource = ViewSource.View;

            ((ListCollectionView) ViewSource.View).SortDescriptions.Add(new SortDescription(nameof(Ubezpieczenie.PozostaleDni), ListSortDirection.Ascending));

            LadujDane(typ);
        }

        #endregion

        #region Metody

        private void AplikujFiltry() {
            var imie = Imie_tb.Text?.ToUpper();
            var nazwisko = Nazwisko_tb.Text?.ToUpper();
            var telefon = Telefon_tb.Text?.ToUpper();
            var opis = Opis_tb.Text?.ToUpper();
            var data1 = Data_rozpoczecia_tb.SelectedDate;
            var data2 = Data_zakonczenia_tb.SelectedDate;
            var firma = Firma_tb.Text?.ToUpper();

            if (imie.IsNullOrEmpty() && nazwisko.IsNullOrEmpty() && telefon.IsNullOrEmpty() && opis.IsNullOrEmpty() && data1 == null && data2 == null && firma.IsNullOrEmpty()) {
                ((ListCollectionView) ViewSource.View).Filter = null;
                return;
            }

            ((ListCollectionView) ViewSource.View).Filter = o => {
                if (!(o is Ubezpieczenie item)) return false;

                var b1 = imie.IsNullOrEmpty() || (item.Imie?.ToUpper().Contains(imie) ?? false);
                var b2 = nazwisko.IsNullOrEmpty() || (item.Nazwisko?.ToUpper().Contains(nazwisko) ?? false);
                var b3 = telefon.IsNullOrEmpty() || (item.NumerTelefonu?.ToUpper().Contains(telefon) ?? false);
                var b4 = opis.IsNullOrEmpty() || (item.Opis?.ToUpper().Contains(opis) ?? false);

                var b5 = data1 == null || item.DataRozpoczecia >= data1.Value;
                var b6 = data2 == null || item.DataZakonczenia >= data2.Value;

                var b7 = firma.IsNullOrEmpty() || (item.Firma?.ToUpper().Contains(firma) ?? false);

                return b1 && b2 && b3 && b4 && b5 && b6 && b7;
            };
        }

        private Task LadujDane(TypUbezpieczenia typ) {
            return Task.Factory.StartNew(() => {
                using (new Loading(-1)) {
                    var o = typ.Wczytaj();
                    KontrolaListy.Wyswietl(o, DaneWyswietl);

                    var doPowiadomienia = o.Where(x => x.PozostaleDni <= 10).OrderBy(x => x.PozostaleDni).ToList();
                    foreach (var osoba in doPowiadomienia) {
                        NotificationType typPowiadomienia;

                        if (osoba.PozostaleDni <= 2)
                            typPowiadomienia = NotificationType.Error;
                        else if (osoba.PozostaleDni <= 7)
                            typPowiadomienia = NotificationType.Warning;
                        else
                            typPowiadomienia = NotificationType.Information;

                        Powiadomienia.WyswietlPowiadomienie($"Pozostało {osoba.PozostaleDni} dni ubezpieczenia", $"{osoba.Imie} {osoba.Nazwisko}", typPowiadomienia, doPowiadomienia.Count);
                    }
                }
            });
        }

        private void Wroc() {
            Zapisz();

            NavigationService?.GoBack();
            NavigationService?.RemoveBackEntry();
        }

        #endregion

        #region Obsługa elementów UI

        private void Bt_wroc_Click(object sender, RoutedEventArgs e) => Wroc();

        private void Bt_dodaj_OnClick(object sender, RoutedEventArgs e) {
            var o = Okna.OpenOne<DodajOsobe>();
            o.Closed += (sender1, args) => {
                if (!o.Status) return;

                var nowyItem = o.UbezpieczenieReturn;
                nowyItem.Id = (DaneWyswietl.Any() ? (DaneWyswietl.Max(x => x.Id) + 1) : 1).ToString();

                DaneWyswietl.Add(Ubezpieczenie.Parse(nowyItem));
            };
        }

        private void Bt_usun_OnClick(object sender, RoutedEventArgs e) {
            var okno = Okna.Message("Czy na pewno skasować wybrane wiersze?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (okno != MessageBoxResult.Yes) return;

            foreach (var item in DaneDg.SelectedItems.OfType<Ubezpieczenie>().ToList()) {
                DaneWyswietl.Remove(item);
            }
        }

        private void Bt_szukaj_OnClick(object sender, RoutedEventArgs e) {
            switch (KontenerFiltrow.Visibility) {
                case Visibility.Visible:
                    var sb1 = this.FindResource("Hide_storyboard") as Storyboard;
                    sb1?.Begin();
                    break;
                case Visibility.Collapsed:
                    KontenerFiltrow.Visibility = Visibility.Visible;
                    var sb2 = this.FindResource("Show_storyboard") as Storyboard;
                    sb2?.Begin();
                    break;
                case Visibility.Hidden:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Hide_Completed(object sender, EventArgs e) {
            KontenerFiltrow.Visibility = Visibility.Collapsed;
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e) => AplikujFiltry();
        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e) => AplikujFiltry();

        private void Bt_wklej_Click(object sender, RoutedEventArgs e) {
            var okno = Okna.Message("Czy na pewno wkleić dane ze schowka?", "Potwierdzenie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (okno != MessageBoxResult.Yes) return;

            var id = DaneWyswietl.Any() ? (DaneWyswietl.Max(x => x.Id) + 1) : 1;
            foreach (var item in Schowek.PasteFromExcelToClass<ExcelWklej>()) {
                var ubezpieczenie = new UbezpieczeniaString {
                    Nazwisko        = item.Nazwisko,
                    Imie            = item.Imie,
                    Firma           = item.Firma,
                    Skladka         = item.Skladka,
                    NumerTelefonu   = item.NumerTelefonu,
                    Opis            = item.Opis,
                    DataRozpoczecia = item.DataRozpoczecia,
                    DataZakonczenia = item.DataZakonczenia,
                    Id              = (id++).ToString()
                };

                var o = Ubezpieczenie.Parse(ubezpieczenie);
                DaneWyswietl.Add(o);
            }
        }

        #endregion

        #region Implementation of IZapis

        public void Zapisz() {
            using (new Loading(-1)) {
                DaneWyswietl.ToList().Zapisz(Typ);
            }
        }

        #endregion


        
    }
}