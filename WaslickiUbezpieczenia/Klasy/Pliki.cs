using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WaslickiUbezpieczenia.Klasy {
    public static class Pliki {
        private static readonly string Sciezka = Path.Combine(Directory.GetCurrentDirectory(), "Database");

        public static void Zapisz(this IEnumerable<Ubezpieczenie> daneDoZapisania, TypUbezpieczenia typ) {
            typ.Backup();
            var dir = typ.OkreslSciezke();

            var dane = daneDoZapisania.Select(x => {
                var o = new UbezpieczeniaString(x);
                o.Szyfruj();

                return o;
            }).ToList();

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Include;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(dir))
            using (JsonWriter writer = new JsonTextWriter(sw)) {
                serializer.Serialize(writer, dane);
            }
        }

        public static List<Ubezpieczenie> Wczytaj(this TypUbezpieczenia typ) {
            var dir = typ.OkreslSciezke();

            if(!File.Exists(dir))
                typ.RestoreBackup();

            if (!File.Exists(dir))
                return new List<Ubezpieczenie>();

            using (Stream s = new FileStream(dir, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(s)) {
                var settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.NullValueHandling = NullValueHandling.Include;
                settings.Converters.Add(new JavaScriptDateTimeConverter());
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                var dane = JsonConvert.DeserializeObject<List<UbezpieczeniaString>>(sr.ReadToEnd(), settings);

                return dane.Select(x => {
                    if (x.Szyfrowane)
                        x.Odszyfruk();

                    return Ubezpieczenie.Parse(x);
                }).ToList();
            }

        }

        public static void OtworzSciezkeBazy() {
            if (!Directory.Exists(Sciezka))
                Directory.CreateDirectory(Sciezka);

            Process.Start(Sciezka);
        }

        private static string OkreslSciezkeBackup(this TypUbezpieczenia typ) {
            if (!Directory.Exists(Sciezka))
                Directory.CreateDirectory(Sciezka);

            switch (typ) {
                case TypUbezpieczenia.Samochody:
                    return Path.Combine(Sciezka, "Samochody.backup");
                case TypUbezpieczenia.Domy:
                    return Path.Combine(Sciezka, "Domy.backup"); ;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typ), typ, null);
            }
        }

        private static string OkreslSciezke(this TypUbezpieczenia typ) {
            if (!Directory.Exists(Sciezka))
                Directory.CreateDirectory(Sciezka);

            switch (typ) {
                case TypUbezpieczenia.Samochody:
                    return Path.Combine(Sciezka, "Samochody.lwdb");
                case TypUbezpieczenia.Domy:
                    return Path.Combine(Sciezka, "Domy.lwdb"); ;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typ), typ, null);
            }
        }

        private static void Backup(this TypUbezpieczenia typ) {
            var orgFile = typ.OkreslSciezke();
            if (!File.Exists(orgFile)) return;

            var backupFile = typ.OkreslSciezkeBackup();
            if (File.Exists(backupFile))
                File.Delete(backupFile);

            File.Move(orgFile, backupFile);
        }

        private static void RestoreBackup(this TypUbezpieczenia typ) {
            var backupFile = typ.OkreslSciezkeBackup();
            if (!File.Exists(backupFile)) return;

            var orgFile = typ.OkreslSciezke();
            if(File.Exists(orgFile))
                File.Delete(orgFile);

            File.Move(backupFile, orgFile);
        }
    }
}