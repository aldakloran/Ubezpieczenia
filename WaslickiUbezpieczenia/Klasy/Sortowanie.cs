using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WaslickiUbezpieczenia.Klasy {
    internal class SortowanieLinq<T> : IComparer<T> where T : class {
        #region Zmienne

        private readonly List<string> _SortMemberPath;
        private Sortowanie _sort;

        #endregion

        #region Konstruktory

        public SortowanieLinq() {
            _sort = new Sortowanie();
        }

        public SortowanieLinq(ListSortDirection direction) {
            _sort = new Sortowanie(direction);
        }

        public SortowanieLinq(ListSortDirection direction, params string[] SortMemberPath) {
            _sort = new Sortowanie(direction);
            _SortMemberPath = SortMemberPath.ToList();
        }

        #endregion

        #region Implementation of IComparer<in T>

        public int Compare(T x, T y) {
            if (!_SortMemberPath.Any())
                return _sort.Compare(x?.ToString(), y?.ToString());

            string val1 = string.Empty;
            string val2 = string.Empty;

            foreach (var sort_path in _SortMemberPath) {
                foreach (PropertyInfo property in typeof(T).GetProperties().Where(u => u.Name == sort_path)) {
                    val1 += " " + property.GetValue(x)?.ToString();
                    val2 += " " + property.GetValue(y)?.ToString();
                }
            }

            return _sort.Compare(val1.Trim(), val2.Trim());
        }

        #endregion
    }

    internal class Sortowanie : IComparer, IComparer<string> {
        #region Zmienne

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        private static extern int StrCmpLogicalW(string x, string y);

        private readonly ListSortDirection _direction;
        private readonly List<string> _SortMemberPath;

        #endregion

        #region Konstruktory

        public Sortowanie() {
            _direction = ListSortDirection.Ascending;
            _SortMemberPath = new List<string>();
        }

        public Sortowanie(ListSortDirection direction) {
            _direction = direction;
            _SortMemberPath = new List<string>();
        }

        public Sortowanie(ListSortDirection direction, params string[] SortMemberPath) {
            _direction = direction;
            _SortMemberPath = SortMemberPath.ToList();
        }

        #endregion

        #region Metody

        public int Compare(string x, string y) {
            switch (_direction) {
                case ListSortDirection.Ascending:
                    return StrCmpLogicalW(x, y);
                case ListSortDirection.Descending:
                    return StrCmpLogicalW(y, x);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int Compare(object x, object y) {
            if (!_SortMemberPath.Any())
                return Compare(x?.ToString(), y?.ToString());

            string val1 = string.Empty;
            string val2 = string.Empty;

            foreach (var sort_path in _SortMemberPath) {
                foreach (PropertyInfo property in x.GetType().GetProperties()) {
                    if (property.Name == sort_path) {
                        val1 += string.IsNullOrEmpty(val1) ? property.GetValue(x)?.ToString() : " " + property.GetValue(x)?.ToString();
                        val2 += string.IsNullOrEmpty(val2) ? property.GetValue(y)?.ToString() : " " + property.GetValue(y)?.ToString();
                    }
                }
            }

            return Compare(val1.Trim(), val2.Trim());
        }

        #endregion
    }
}