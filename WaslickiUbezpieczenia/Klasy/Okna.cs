using System;
using System.Diagnostics;
using System.Windows;

namespace WaslickiUbezpieczenia.Klasy {
    public static class Okna {
        public static Window MainWindow => Application.Current.Dispatcher.Invoke(() => Application.Current.MainWindow);
        public static WindowCollection WszystkieOkna => Application.Current.Dispatcher.Invoke(() => Application.Current.Windows);

        public static MessageBoxResult Message(string wiadomosc, string tytul, MessageBoxButton przycisk, MessageBoxImage obrazek) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(MainWindow, wiadomosc, tytul, przycisk, obrazek));
        }

        public static MessageBoxResult Message(string wiadomosc, string tytul, MessageBoxButton przycisk) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(MainWindow, wiadomosc, tytul, przycisk));
        }

        public static MessageBoxResult Message(string wiadomosc, string tytul) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(MainWindow, wiadomosc, tytul));
        }

        public static MessageBoxResult Message(string wiadomosc) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(MainWindow, wiadomosc));
        }

        public static MessageBoxResult Message(Window okno, string wiadomosc, string tytul, MessageBoxButton przycisk, MessageBoxImage obrazek) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(okno, wiadomosc, tytul, przycisk, obrazek));
        }

        public static MessageBoxResult Message(Window okno, string wiadomosc, string tytul, MessageBoxButton przycisk) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(okno, wiadomosc, tytul, przycisk));
        }

        public static MessageBoxResult Message(Window okno, string wiadomosc, string tytul) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(okno, wiadomosc, tytul));
        }

        public static MessageBoxResult Message(Window okno, string wiadomosc) {
            return Application.Current.Dispatcher.Invoke(() => MessageBox.Show(okno, wiadomosc));
        }

        public static T OpenOne<T>() where T : Window {
            Debug.WriteLine($@"-----------------=============#### Otwieram okno: {typeof(T).Name} ####=============-----------------");
            foreach (object item in WszystkieOkna) {
                if (!(item is T one)) continue;

                one.Focus();
                one.Visibility = Visibility.Visible;
                return one;
            }

            if (!(Activator.CreateInstance(typeof(T), true) is T okno)) return null;

            okno.Owner = MainWindow;
            okno.Show();
            okno.Focus();
            okno.Closed += (s, e) => okno.Owner.Focus();
            return okno;
        }

        public static T OpenOne<T>(params object[] args) where T : Window {
            Debug.WriteLine($@"-----------------=============#### Otwieram okno: {typeof(T).Name} ####=============-----------------");
            foreach (object item in WszystkieOkna) {
                if (!(item is T one)) continue;

                one.Focus();
                one.Visibility = Visibility.Visible;
                return one;
            }

            if (!(Activator.CreateInstance(typeof(T), args) is T okno)) return null;

            okno.Owner = MainWindow;
            okno.Show();
            okno.Focus();
            okno.Closed += (s, e) => okno.Owner.Focus();
            return okno;

        }

        public static T OpenOne<T>(Window owner) where T : Window {
            Debug.WriteLine($@"-----------------=============#### Otwieram okno: {typeof(T).Name} ####=============-----------------");
            foreach (object item in WszystkieOkna) {
                if (!(item is T one)) continue;

                one.Focus();
                one.Visibility = Visibility.Visible;
                return one;
            }

            if (!(Activator.CreateInstance(typeof(T), true) is T okno)) return null;

            okno.Owner = owner;
            okno.Show();
            okno.Focus();
            okno.Closed += (s, e) => okno.Owner.Focus();
            return okno;
        }

        public static T OpenOne<T>(Window owner, params object[] args) where T : Window {
            Debug.WriteLine($@"-----------------=============#### Otwieram okno: {typeof(T).Name} ####=============-----------------");
            foreach (object item in WszystkieOkna) {
                if (!(item is T one)) continue;

                one.Focus();
                one.Visibility = Visibility.Visible;
                return one;
            }

            if (!(Activator.CreateInstance(typeof(T), args) is T okno)) return null;

            okno.Owner = owner;
            okno.Show();
            okno.Focus();
            okno.Closed += (s, e) => okno.Owner.Focus();
            return okno;

        }

        public static void CloseAll<T>() where T : Window {
            foreach (object item in WszystkieOkna)
                if (item is T okno)
                    okno.Close();
        }

    }
}
