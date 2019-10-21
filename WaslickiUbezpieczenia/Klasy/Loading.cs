using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WaslickiUbezpieczenia.Klasy {
    public class Loading : IDisposable {
        private readonly string ID = DateTime.Now.Ticks.ToString();
        private const DispatcherPriority Priority = DispatcherPriority.Normal;
        private Window okno_glowne;
        private int max;
        private Grid ladownie_siatka;
        private ProgressBar ladowanie_bar;
        private int aktualny_progress;
        private bool nieskonczony;

        public bool AutoHiede { get; set; } = true;

        public Loading(int max = int.MaxValue, Window okno = null) {
            Application.Current.Dispatcher.Invoke(Priority, new Action(() => {
                okno_glowne = okno ?? Application.Current.MainWindow;
                this.max = max > 0 ? max - 1 : 10;
                this.nieskonczony = max < 0;
                this.aktualny_progress = 0;

                Utworzenie_ladowania();
            }));

            Timer aTimer = new Timer(80);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e) {
            try {
                ((Timer)sender).Enabled = false;
                Application.Current.Dispatcher.Invoke(Priority, new Action(() => {
                    if (ladownie_siatka == null) return;

                    Debug.WriteLine(@"Pokazuje ekran ładowania");
                    ladownie_siatka.Visibility = Visibility.Visible;
                }));
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Set_max(int max) {
            try {
                if (okno_glowne == null) return;

                if (max < 0) {
                    Application.Current.Dispatcher.Invoke(Priority, new Action(() => {
                        if (ladowanie_bar != null) {
                            ladowanie_bar.Maximum = 10;
                            ladowanie_bar.Value = 0;
                            ladowanie_bar.IsIndeterminate = true;
                        }

                        this.max = 10;
                        this.aktualny_progress = 0;
                    }));
                }
                else {
                    Application.Current.Dispatcher.Invoke(Priority, new Action(() => {
                        if (ladowanie_bar != null) {
                            ladowanie_bar.Maximum = max;
                            ladowanie_bar.Value = 0;
                            ladowanie_bar.IsIndeterminate = false;
                        }

                        this.max = max;
                        this.aktualny_progress = 0;
                    }));
                }

            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Next() {
            try {
                if (okno_glowne == null) return;

                aktualny_progress++;
                Application.Current.Dispatcher.Invoke(Priority, new Action(() => Set_value(aktualny_progress)));
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Set_value(int value) {
            try {
                if (okno_glowne == null) return;

                Application.Current.Dispatcher?.Invoke(Priority, new Action(() => {
                    try {
                        if (value < max)
                            ladowanie_bar.Value = value;
                        else if (AutoHiede)
                            Close();
                    }
                    catch {
                        //ignore
                    }
                }));
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Utworzenie_ladowania() {
            try {
                if (Narzedzia.Szukaj_w_oknie<Grid>(okno_glowne).Any(x => x.Name == "MainGrid" + ID)) return;

                Grid nowa_siatka = new Grid {
                    Background = new SolidColorBrush(Color.FromArgb(102, 0, 0, 0)),
                    Name = "MainGrid" + ID,
                    Visibility = Visibility.Hidden
                };

                Grid inne_grid = new Grid {
                    Background = new ImageBrush() {
                        Opacity = 0.7d
                    },
                    Height = 30d,
                    Width = okno_glowne.Width / 3
                };

                ProgressBar progress = new ProgressBar {
                    Maximum = max,
                    Name = @"PROGRESSBAR_" + ID,
                    Margin = new Thickness(3),
                    IsIndeterminate = nieskonczony
                };

                inne_grid.Children.Add(progress);
                nowa_siatka.Children.Add(inne_grid);

                Debug.WriteLine(@"Dodaje loading do okna");
                okno_glowne.Szukaj_w_oknie<Grid>().FirstOrDefault()?.Children.Add(nowa_siatka);

                ladownie_siatka = nowa_siatka;
                ladowanie_bar = progress;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Close() {
            try {
                if (okno_glowne == null) return;

                Debug.WriteLine(@"---# zamykam ładowanie");
                Application.Current.Dispatcher.Invoke(Priority, new Action(() => {
                    Narzedzia.Szukaj_w_oknie<Grid>(okno_glowne).FirstOrDefault()?.Children.Remove(ladownie_siatka);

                    max = 0;
                    aktualny_progress = 0;
                    okno_glowne = null;
                    ladownie_siatka = null;
                    ladowanie_bar = null;
                }));
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Dispose() => Close();
    }
}
