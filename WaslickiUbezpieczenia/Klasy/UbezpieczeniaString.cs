using System;

namespace WaslickiUbezpieczenia.Klasy {
    public class UbezpieczeniaString {
        public string Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NumerTelefonu { get; set; }
        public string Opis { get; set; }
        public string DataRozpoczecia { get; set; }
        public string DataZakonczenia { get; set; }
        public string Firma { get; set; }
        public string Skladka { get; set; }


        public bool Szyfrowane { get; set; }

        public UbezpieczeniaString() {
            Id              = string.Empty;
            Imie            = string.Empty;
            Nazwisko        = string.Empty;
            NumerTelefonu   = string.Empty;
            Opis            = string.Empty;
            DataRozpoczecia = string.Empty;
            DataZakonczenia = string.Empty;
            Firma           = string.Empty;
            Skladka         = string.Empty;
        }

        public UbezpieczeniaString(Ubezpieczenie item) {
            Id              = item.Id.ToString();
            Imie            = item.Imie;
            Nazwisko        = item.Nazwisko;
            NumerTelefonu   = item.NumerTelefonu;
            Opis            = item.Opis;
            DataRozpoczecia = item.DataRozpoczecia?.ToString("yyyy-MM-dd");
            DataZakonczenia = item.DataZakonczenia?.ToString("yyyy-MM-dd");
            Firma           = item.Firma;
            Skladka         = item.Skladka?.ToString("0.##");
        }

        public void Szyfruj() {
            Szyfrowane = true;

            if (!string.IsNullOrEmpty(Id))
                Id = Id.Szyfruj(nameof(Id));

            if (!string.IsNullOrEmpty(Imie))
                Imie = Imie.Szyfruj(nameof(Imie));

            if (!string.IsNullOrEmpty(Nazwisko))
                Nazwisko = Nazwisko.Szyfruj(nameof(Nazwisko));

            if (!string.IsNullOrEmpty(NumerTelefonu))
                NumerTelefonu = NumerTelefonu.Szyfruj(nameof(NumerTelefonu));

            if (!string.IsNullOrEmpty(Opis))
                Opis = Opis.Szyfruj(nameof(Opis));

            if (!string.IsNullOrEmpty(DataRozpoczecia))
                DataRozpoczecia = DataRozpoczecia.Szyfruj(nameof(DataRozpoczecia));

            if (!string.IsNullOrEmpty(DataZakonczenia))
                DataZakonczenia = DataZakonczenia.Szyfruj(nameof(DataZakonczenia));

            if (!string.IsNullOrEmpty(Firma))
                Firma = Firma.Szyfruj(nameof(Firma));

            if (!string.IsNullOrEmpty(Skladka))
                Skladka = Skladka.Szyfruj(nameof(Skladka));
        }

        public void Odszyfruk() {
            Szyfrowane = false;

            if (!string.IsNullOrEmpty(Id))
                Id = Id.Odszyfruj(nameof(Id));

            if (!string.IsNullOrEmpty(Imie))
                Imie = Imie.Odszyfruj(nameof(Imie));

            if (!string.IsNullOrEmpty(Nazwisko))
                Nazwisko = Nazwisko.Odszyfruj(nameof(Nazwisko));

            if (!string.IsNullOrEmpty(NumerTelefonu))
                NumerTelefonu = NumerTelefonu.Odszyfruj(nameof(NumerTelefonu));

            if (!string.IsNullOrEmpty(Opis))
                Opis = Opis.Odszyfruj(nameof(Opis));

            if (!string.IsNullOrEmpty(DataRozpoczecia))
                DataRozpoczecia = DataRozpoczecia.Odszyfruj(nameof(DataRozpoczecia));

            if (!string.IsNullOrEmpty(DataZakonczenia))
                DataZakonczenia = DataZakonczenia.Odszyfruj(nameof(DataZakonczenia));

            if (!string.IsNullOrEmpty(Firma))
                Firma = Firma.Odszyfruj(nameof(Firma));

            if (!string.IsNullOrEmpty(Skladka))
                Skladka = Skladka.Odszyfruj(nameof(Skladka));
        }
    }
}