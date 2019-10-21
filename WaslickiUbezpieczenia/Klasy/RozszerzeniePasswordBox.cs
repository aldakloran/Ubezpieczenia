using System.Windows;
using System.Windows.Controls;

namespace WaslickiUbezpieczenia.Klasy {
    //Rozszerzenie pola typu PasswordBox o możliwość umieszczenia placeholder-a
    public class RozszerzeniePasswordBox : DependencyObject {


        public static bool GetJestPlaceholder(DependencyObject obj) {
            return (bool)obj.GetValue(JestPlaceholderProperty);
        }

        public static void SetJestPlaceholder(DependencyObject obj, bool value) {
            obj.SetValue(JestPlaceholderProperty, value);
        }


        //Rejestracja włąściwości dołączonej włączającej obsługę placeholdera - rejestrowana jest metoda wywołania zwrotnego obsługująca zmianę długości wpisanego hasła
        public static readonly DependencyProperty JestPlaceholderProperty =
                            DependencyProperty.RegisterAttached("JestPlaceholder", typeof(bool), typeof(RozszerzeniePasswordBox),
                                new UIPropertyMetadata
                                                     (false,
                                                        //Metoda call back wywoływana w czasie ustawiania wartości "JestPlaceholder"
                                                        //(ustawiane jest to w szablonie kontrolki)
                                                        (d, e) => {
                                                            //Następna Lambda - tym razem dodanie delegata do listy wywołań zdarzenia PasswordChange
                                                            ((PasswordBox)d).PasswordChanged += (s, ev) => {
                                                                PasswordBox poleHasla = (PasswordBox)s;
                                                                SetDlugoscHasla(poleHasla, poleHasla.Password.Length);
                                                            };
                                                        }
                                                     )
                                                 );




        public static int GetDlugoscHasla(DependencyObject obj) {
            return (int)obj.GetValue(DlugoscHaslaProperty);
        }

        public static void SetDlugoscHasla(DependencyObject obj, int value) {
            obj.SetValue(DlugoscHaslaProperty, value);
        }

        //Rejestracja właściwości dołączonej zawierającej długość aktualnie wpisanego hasła - jeśli 0, triger włącza widoczność tekstu placeholder-a
        public static readonly DependencyProperty DlugoscHaslaProperty =
            DependencyProperty.RegisterAttached("DlugoscHasla", typeof(int), typeof(RozszerzeniePasswordBox), new UIPropertyMetadata(0));


    }
}
