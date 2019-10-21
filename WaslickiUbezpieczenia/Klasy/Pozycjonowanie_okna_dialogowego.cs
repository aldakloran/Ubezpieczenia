using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WaslickiUbezpieczenia.Klasy {
    public class Pozycjonowanie_okna_dialogowego {
        private readonly Window Okno;
        private Window Owner => Okno.Owner;

        public Pozycjonowanie_okna_dialogowego(Window okno, Task init = null) {
            Okno = okno;
            Okno.Visibility = Visibility.Collapsed;

            if (init != null) {
                Okno.Loaded += (se, ev) => {
                    Owner.LocationChanged += ReCenter;
                    Owner.SizeChanged += ReCenter;
                    Owner.PreviewKeyDown += Escape_close;
                };
                init.ContinueWith(x => ReCenter());
            }
            else {
                Okno.Loaded += (se, ev) => {
                    Owner.LocationChanged += ReCenter;
                    Owner.SizeChanged += ReCenter;
                    Owner.PreviewKeyDown += Escape_close;
                    ReCenter();
                };
            }

            Okno.Closed += (se, ev) => {
                Owner.LocationChanged -= ReCenter;
                Owner.SizeChanged -= ReCenter;
                Owner.PreviewKeyDown -= Escape_close;
            };
            Okno.SizeChanged += ReCenter;
            Okno.SizeChanged += ReCenter;
            Okno.PreviewKeyDown += Escape_close;

            okno.Focus();
        }

        private void Escape_close(object se = null, KeyEventArgs ev = null) {
            if (ev == null) return;

            if (ev.Key == Key.Escape)
                Okno.Close();
        }

        private void ReCenter(object se = null, EventArgs ev = null) {
            Debug.WriteLine(@"---# Ustawiam na środku");
            Okno.Dispatcher.Invoke(() => {
                switch (Okno.WindowState) {
                    case WindowState.Normal:
                        Okno.Left = Owner.Left + ((Owner.Width - Okno.Width) / 2);
                        Okno.Top = Owner.Top + ((Owner.Height - Okno.Height) / 2);
                        break;
                    case WindowState.Maximized:
                        Okno.Left = ((Owner.Width - Okno.Width) / 2);
                        Okno.Top = ((Owner.Height - Okno.Height) / 2);
                        break;
                    case WindowState.Minimized:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (Okno.Visibility == Visibility.Collapsed)
                    Okno.Visibility = Visibility.Visible;
            });
        }
    }
}
