using System;
using System.Collections.Generic;
using System.Linq;

namespace WaslickiUbezpieczenia.Klasy {
    public static class GeneratodDanychTestowych {
        public static IEnumerable<Ubezpieczenie> GenerujDane(int ilosc) {
            for (int i = 0; i < ilosc; i++) {
                var o = new Ubezpieczenie(i+1, $"imie {i}", $"nazwisko {i}", $"opis {i} ...", DateTime.Today.AddDays(i), DateTime.Today.AddDays(i), $"Firma {i}", i*10.2m);
                yield return o;
            }
        }
    }
}