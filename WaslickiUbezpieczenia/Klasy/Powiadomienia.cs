using System;
using System.Diagnostics;
using Notifications.Wpf;

namespace WaslickiUbezpieczenia.Klasy {
    public static class Powiadomienia {
        private static NotificationManager notificationManager = new NotificationManager();
        private static int licznik = 0;

        public static void WyswietlPowiadomienie(string text, string title, NotificationType typ) {
            notificationManager.Show(new NotificationContent {
                Title   = title,
                Message = text,
                Type    = typ
            });
        }

        public static void WyswietlPowiadomienie(string text, string title, NotificationType typ, int ile) {
            notificationManager.Show(new NotificationContent {
                Title   = title,
                Message = text,
                Type    = typ
            }, expirationTime: TimeSpan.FromSeconds(10).Add(TimeSpan.FromMilliseconds(licznik++ * 60)));

            if (licznik != ile) return;

            Debug.WriteLine(@"Zeruje licznik powiadomień");
            licznik = 0;
        }

        public static void Dispose() {
            notificationManager = null;
        }
    }
}