using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace WaslickiUbezpieczenia.Klasy {
    public static class Narzedzia {
        public static IEnumerable<T> Szukaj_w_oknie_Logical<T>(this DependencyObject depObj) where T : DependencyObject {
            if (depObj == null) yield break;

            foreach (var child in LogicalTreeHelper.GetChildren(depObj)) {
                if (child is T dependency_object) {
                    yield return dependency_object;
                }

                foreach (T childOfChild in Szukaj_w_oknie_Logical<T>(child as DependencyObject)) {
                    yield return childOfChild;
                }
            }
        }


        public static IEnumerable<T> Szukaj_w_oknie<T>(this DependencyObject depObj) where T : DependencyObject {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T dependency_object) {
                    yield return dependency_object;
                }

                foreach (T childOfChild in Szukaj_w_oknie<T>(child)) {
                    yield return childOfChild;
                }
            }
        }

        public static IEnumerable<UIElement> Wszystko_w_oknie(this DependencyObject depObj) {
            if (depObj == null)
                yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                yield return child as UIElement;

                foreach (UIElement childOfChild in Wszystko_w_oknie(child)) {
                    yield return childOfChild;
                }
            }
        }
    }
}