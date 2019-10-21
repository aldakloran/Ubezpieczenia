using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Services;
using System.Windows.Media.Imaging;
using WaslickiUbezpieczenia.Annotations;

namespace WaslickiUbezpieczenia.Klasy {
    public class Ubezpieczenie : IEquatable<Ubezpieczenie>, INotifyPropertyChanged {
        private DateTime? _data_rozpoczecia;
        private DateTime? _data_zakonczenia;

        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Firma { get; set; }
        public decimal? Skladka { get; set; }
        public string NumerTelefonu { get; set; }
        public string Opis { get; set; }

        public DateTime? DataRozpoczecia {
            get => _data_rozpoczecia;
            set {
                _data_rozpoczecia = value;
                On_property_changed(nameof(PozostaleDni));
            }
        }

        public DateTime? DataZakonczenia {
            get => _data_zakonczenia;
            set {
                _data_zakonczenia = value;
                On_property_changed(nameof(PozostaleDni));
            }
        }

        public double? PozostaleDni {
            get {
                if (DataZakonczenia == null) return null;

                var data1 = DataZakonczenia.Value;
                var data2 = DateTime.Today;

                return data1.Subtract(data2).TotalDays;
            }
        }

        public Ubezpieczenie(int id, string imie, string nazwisko, string opis, DateTime? data1, DateTime? data2, string firma, decimal? skladka) {
            Id              = id;
            Imie            = imie;
            Nazwisko        = nazwisko;
            Opis            = opis;
            DataRozpoczecia = data1;
            DataZakonczenia = data2;
            Firma           = firma;
            Skladka         = skladka;
        }

        private Ubezpieczenie() {
            Id              = 0;
            Imie            = string.Empty;
            Nazwisko        = string.Empty;
            Opis            = string.Empty;
            DataRozpoczecia = null;
            DataZakonczenia = null;
            Firma           = string.Empty;
            Skladka         = null;
        }


        public static Ubezpieczenie Parse(UbezpieczeniaString value) {
            if(value.Szyfrowane) value.Odszyfruk();
            
            var o = new Ubezpieczenie();

            if (!string.IsNullOrEmpty(value.Id) && value.Id.CanParse<int>())
                o.Id = value.Id.Parse<int>();

            if (!string.IsNullOrEmpty(value.Imie))
                o.Imie = value.Imie;

            if (!string.IsNullOrEmpty(value.Nazwisko))
                o.Nazwisko = value.Nazwisko;

            if (!string.IsNullOrEmpty(value.Opis))
                o.Opis = value.Opis;

            if (!string.IsNullOrEmpty(value.NumerTelefonu))
                o.NumerTelefonu = value.NumerTelefonu;

            if (!string.IsNullOrEmpty(value.DataRozpoczecia) && value.DataRozpoczecia.CanParse<DateTime>())
                o.DataRozpoczecia = value.DataRozpoczecia.Parse<DateTime>();

            if (!string.IsNullOrEmpty(value.DataZakonczenia) && value.DataZakonczenia.CanParse<DateTime>())
                o.DataZakonczenia = value.DataZakonczenia.Parse<DateTime>();

            if (!string.IsNullOrEmpty(value.Firma))
                o.Firma = value.Firma;

            if (!string.IsNullOrEmpty(value.Skladka) && value.Skladka.CanParse<decimal>())
                o.Skladka = value.Skladka.Parse<decimal>();

            return o;
        }

        #region Equality members

        public bool Equals(Ubezpieczenie other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Imie == other.Imie && Nazwisko == other.Nazwisko && Opis == other.Opis && Nullable.Equals(DataRozpoczecia, other.DataRozpoczecia) && Nullable.Equals(DataZakonczenia, other.DataZakonczenia) && NumerTelefonu == other.NumerTelefonu && Firma == other.Firma && Skladka == other.Skladka;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ubezpieczenie) obj);
        }

        public override int GetHashCode() {
            unchecked {
                int hashCode = Id.GetHashCode();
                return hashCode;
            }
        }

        #endregion

        #region On_property_changed members

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void On_property_changed([CallerMemberName] string property_name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property_name));
        }

        #endregion
    }
}