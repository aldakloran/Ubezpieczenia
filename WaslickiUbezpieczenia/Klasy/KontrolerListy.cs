using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace WaslickiUbezpieczenia.Klasy {
    public static class KontrolaListy {
        private const DispatcherPriority Priorytet = DispatcherPriority.Background;
        private static readonly object Lock_me = new object();
        private static Task Zatrzymanie;

        public static void Cancel() {
            //Last_operation.ForEach(x => x?.Abort());
            //Zatrzymanie = Task.Factory.StartNew(() => {
            //    Parallel.ForEach(Last_operation, item => item?.Abort());
            //    Last_operation.Clear();
            //});


            foreach (var dispatcherOperation in Last_operation) {
                if (dispatcherOperation == null)
                    continue;

                if (dispatcherOperation.Status != DispatcherOperationStatus.Aborted && dispatcherOperation.Status != DispatcherOperationStatus.Completed)
                    dispatcherOperation.Abort();
            }

            Last_operation.Clear();
        }

        public static bool Czy_pracuje() {
            return Last_operation.AsParallel().Any(x => x.Status == DispatcherOperationStatus.Executing || x.Status == DispatcherOperationStatus.Pending);
        }

        public static IList<string> Wyswietl<T>(IReadOnlyCollection<T> lista_do_wyswietlenia, ObservableCollection<T> aktualna_lista) {
            //lock (Lock_me) {
            return ScalListy(lista_do_wyswietlenia, aktualna_lista, true);
            //}
        }

        public static IList<string> WyswietlAll<T>(IReadOnlyCollection<T> lista_do_wyswietlenia, ObservableCollection<T> aktualna_lista) {
            //lock (Lock_me) {
            return ScalListy(lista_do_wyswietlenia, aktualna_lista, false);
            //}
        }

        public static DispatcherOperation Ostatnia_operacja => Last_operation.LastOrDefault();

        private static readonly List<DispatcherOperation> Last_operation = new List<DispatcherOperation>();
        private static IList<string> prop_diff;
        private static IList<string> ScalListy<T>(IReadOnlyCollection<T> lista_do_wyswietlenia, ICollection<T> Dane_wyswietl, bool one_by_one) {
            //Zatrzymanie?.Wait();
            Ostatnia_operacja?.Wait(); // oczekiwanie na wprowadzenie zmian z poprzedniego wywołania
            Last_operation.Where(x => x == null || x.Status == DispatcherOperationStatus.Aborted || x.Status == DispatcherOperationStatus.Completed).ToList().ForEach(x => Last_operation.Remove(x));

            prop_diff = new List<string>();

            if (lista_do_wyswietlenia == null) {    // znacznie szybciej bangla niz reczne wywalanie itemow
                Last_operation?.Add(Application.Current.Dispatcher.BeginInvoke(Priorytet, new Action(Dane_wyswietl.Clear)));
                return prop_diff;
            }

            var do_wywalenia = Dane_wyswietl.Except(lista_do_wyswietlenia.OfType<T>(), new CustomObjectEqueal<T>()).ToList();    // ustalam co wywalić
            var do_dodania = lista_do_wyswietlenia.Except(Dane_wyswietl.OfType<T>(), new CustomObjectEqueal<T>()).ToList();      // ustalam co dodać

            //int dodane = 0;
            //int wywalone = 0;

            if (one_by_one) {
                foreach (T item in do_wywalenia)
                    Last_operation?.Add(Application.Current.Dispatcher.BeginInvoke(Priorytet, new Action(() => {
                        Dane_wyswietl.Remove(item);
                        //dodane++;
                    })));

                foreach (T item in do_dodania)
                    Last_operation?.Add(Application.Current.Dispatcher.BeginInvoke(Priorytet, new Action(() => {
                        Dane_wyswietl.Add(item);
                        //wywalone++;
                    })));
            }
            else {
                Last_operation?.Add(Application.Current.Dispatcher.BeginInvoke(Priorytet, new Action(() => {
                    foreach (T item in do_wywalenia)
                        Dane_wyswietl.Remove(item);

                    foreach (T item in do_dodania)
                        Dane_wyswietl.Add(item);
                })));
            }

            if (!prop_diff.Any() && do_dodania.Count > 0)
                prop_diff.Add("Add");
            if (!prop_diff.Any() && do_wywalenia.Count > 0)
                prop_diff.Add("Remove");

            return prop_diff;
        }

        class CustomObjectEqueal<T> : EqualityComparer<T> {
            public override bool Equals(T x, T y) {
                if (x is IEquatable<T> || y is IEquatable<T>) {
                    bool test = x?.Equals(y) ?? false;


                    return test;
                }

                throw new NotSupportedException();
            }

            public override int GetHashCode(T obj) {
                if (obj is IEquatable<T> equatable)
                    return equatable.GetHashCode();

                throw new NotSupportedException();
            }
        }
    }
}
