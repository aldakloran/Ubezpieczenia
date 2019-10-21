using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace WaslickiUbezpieczenia.Klasy {
    public static class Schowek {
        /// <summary>
        /// <para>Ładowanie danych ze schowka do okiektu danego typu</para>
        /// 
        /// Dane ładowane z programu Excel (wiersze rozdzielane jako "\r\n", kolumny jako "\t").<br />
        /// Dane ładowane do obiektu z wykorzystaniem publicznych seterów i wypluwane w postaci kilejki emumeratorów
        /// <param name="ilosc_wklejanych_kolumn">Maksymalna ilość kolum, która ma być wklejona. Domyślnie 1024 kolumny.</param>
        /// </summary>
        public static IEnumerable<T> PasteFromExcelToClass<T>(int ilosc_wklejanych_kolumn = 1024) where T : class {
            string text = Clipboard.GetText();

            if (string.IsNullOrEmpty(text)) yield break;

            foreach (string wiersz in text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)) {
                var cells = wiersz.Split('\t');
                int licznik = 0;

                var new_object = Activator.CreateInstance(typeof(T), true) as T;
                //TypedReference reference = __makeref(NewStruct);
                foreach (PropertyInfo field in typeof(T).GetProperties().OrderBy(x => x.MetadataToken)) {
                    if (!field.CanWrite)
                        continue;

                    if (licznik < cells.Length && licznik < ilosc_wklejanych_kolumn) {
                        string val = cells[licznik];

                        TypeCode typ = Type.GetTypeCode(field.PropertyType);
                        if (field.PropertyType.IsGenericType && field.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                            typ = Type.GetTypeCode(field.PropertyType.GetGenericArguments()[0]);
                        }

                        switch (typ) {
                            case TypeCode.String:
                                field.SetValue(new_object, val);

                                break;
                            case TypeCode.Boolean:
                                if (bool.TryParse(val, out bool res1))
                                    field.SetValue(new_object, res1);

                                break;
                            case TypeCode.Byte:
                                if (byte.TryParse(val, out byte res2))
                                    field.SetValue(new_object, res2);

                                break;
                            case TypeCode.Char:
                                if (char.TryParse(val, out char res3))
                                    field.SetValue(new_object, res3);

                                break;
                            case TypeCode.DateTime:
                                if (DateTime.TryParse(val, out DateTime res4))
                                    field.SetValue(new_object, res4);

                                break;
                            case TypeCode.Decimal:
                                if (val.CanGetDecimal())
                                    field.SetValue(new_object, val.GetDecimal());

                                break;
                            case TypeCode.Double:
                                if (val.CanGetDouble())
                                    field.SetValue(new_object, val.GetDouble());

                                break;
                            case TypeCode.Int16:
                                if (val.CanGetShort())
                                    field.SetValue(new_object, val.GetShort());

                                break;
                            case TypeCode.Int32:
                                if (val.CanGetInt())
                                    field.SetValue(new_object, val.GetInt());

                                break;
                            case TypeCode.Int64:
                                if (val.CanGetLong())
                                    field.SetValue(new_object, val.GetLong());

                                break;
                            case TypeCode.SByte:
                                if (sbyte.TryParse(val, out sbyte res10))
                                    field.SetValue(new_object, res10);

                                break;
                            case TypeCode.Single:
                                if (val.CanGetFloat())
                                    field.SetValue(new_object, val.GetFloat());

                                break;
                            case TypeCode.UInt16:
                                if (val.CanGetUShort())
                                    field.SetValue(new_object, val.GetUShort());

                                break;
                            case TypeCode.UInt32:
                                if (val.CanGetUInt())
                                    field.SetValue(new_object, val.GetUInt());

                                break;
                            case TypeCode.UInt64:
                                if (val.CanGetULong())
                                    field.SetValue(new_object, val.CanGetULong());

                                break;
                            default:
                                Debug.WriteLine(@"Inny typ");
                                try {
                                    object new_val = Convert.ChangeType(val, field.PropertyType);
                                    field.SetValue(new_object, new_val);
                                }
                                catch {
                                    // ignored
                                }

                                break;
                        }
                    }

                    licznik++;
                }

                yield return new_object;
            }
        }

        /// <summary>
        /// <para>Ładowanie danych ze schowka do danej struktury</para>
        /// 
        /// Dane ładowane z programu Excel (wiersze rozdzielane jako "\r\n", kolumny jako "\t").<br />
        /// Dane ładowane do struktury bezpośrednio do publicznych atrybutów i wypluwane w postaci kilejki emumeratorów
        /// </summary>
        public static IEnumerable<T> PasteFromExcelToStruct<T>(int ilosc_wklejanych_kolumn = 1024) where T : struct {
            string text = Clipboard.GetText();

            if (string.IsNullOrEmpty(text)) yield break;

            foreach (string wiersz in text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)) {
                var cells = wiersz.Split('\t');
                int licznik = 0;

                var new_struct = new T();
                TypedReference reference = __makeref(new_struct);
                foreach (FieldInfo field in typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public).OrderBy(x => x.MetadataToken)) {
                    if (licznik < cells.Length && licznik < ilosc_wklejanych_kolumn) {
                        string val = cells[licznik];

                        TypeCode typ = Type.GetTypeCode(field.FieldType);
                        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                            typ = Type.GetTypeCode(field.FieldType.GetGenericArguments()[0]);
                        }

                        switch (typ) {
                            case TypeCode.String:
                                field.SetValueDirect(reference, val);

                                break;
                            case TypeCode.Boolean:
                                if (bool.TryParse(val, out bool res1))
                                    field.SetValueDirect(reference, res1);

                                break;
                            case TypeCode.Byte:
                                if (byte.TryParse(val, out byte res2))
                                    field.SetValueDirect(reference, res2);

                                break;
                            case TypeCode.Char:
                                if (char.TryParse(val, out char res3))
                                    field.SetValueDirect(reference, res3);

                                break;
                            case TypeCode.DateTime:
                                if (DateTime.TryParse(val, out DateTime res4))
                                    field.SetValueDirect(reference, res4);

                                break;
                            case TypeCode.Decimal:
                                if (val.CanGetDecimal())
                                    field.SetValueDirect(reference, val.GetDecimal());

                                break;
                            case TypeCode.Double:
                                if (val.CanGetDouble())
                                    field.SetValueDirect(reference, val.GetDouble());

                                break;
                            case TypeCode.Int16:
                                if (val.CanGetShort())
                                    field.SetValueDirect(reference, val.GetShort());

                                break;
                            case TypeCode.Int32:
                                if (val.CanGetInt())
                                    field.SetValueDirect(reference, val.GetInt());

                                break;
                            case TypeCode.Int64:
                                if (val.CanGetLong())
                                    field.SetValueDirect(reference, val.GetLong());

                                break;
                            case TypeCode.SByte:
                                if (sbyte.TryParse(val, out sbyte res10))
                                    field.SetValueDirect(reference, res10);

                                break;
                            case TypeCode.Single:
                                if (val.CanGetFloat())
                                    field.SetValueDirect(reference, val.GetFloat());

                                break;
                            case TypeCode.UInt16:
                                if (val.CanGetUShort())
                                    field.SetValueDirect(reference, val.GetUShort());

                                break;
                            case TypeCode.UInt32:
                                if (val.CanGetUInt())
                                    field.SetValueDirect(reference, val.GetUInt());

                                break;
                            case TypeCode.UInt64:
                                if (val.CanGetULong())
                                    field.SetValueDirect(reference, val.GetULong());

                                break;
                            default:
                                Debug.WriteLine(@"Inny typ");
                                try {
                                    object new_val = Convert.ChangeType(val, field.FieldType);
                                    field.SetValueDirect(reference, new_val);
                                }
                                catch {
                                    // ignored
                                }

                                break;
                        }
                    }

                    licznik++;
                }

                yield return new_struct;
            }
        }

        /// <summary>
        /// <para>Funkcja eksperymentalna</para>
        /// 
        /// Umożliwia parsowanie stringa na "dowolny" typ wyjściowy oraz zwraca czy operacja się udała
        /// </summary>
        public static bool GenericTryParse<T>(this string input, out T value) {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.IsValid(input)) {
                value = (T)converter.ConvertFromString(input);
                return true;
            }
            value = default(T);
            return false;
        }

        public static string ListaNaString<T>(this ICollection<T> lista) where T : class {
            var sb_ret = new StringBuilder();
            var sb_header = new StringBuilder();
            var sb_content = new StringBuilder();

            var all_fields = typeof(T).GetProperties().OrderBy(x => x.MetadataToken);
            foreach (PropertyInfo field in all_fields) {
                sb_header.Append((field.Name + '\t').Replace('\n', ' '));
            }

            sb_ret.AppendLine(sb_header.ToString());

            foreach (T o in lista) {
                foreach (var field in all_fields) {
                    if (!field.CanRead) continue;

                    sb_content.Append((field.GetValue(o)?.ToString() + '\t').Replace('\n', ' '));
                }

                sb_content.Append("\r\n");
            }

            sb_ret.AppendLine(sb_content.ToString());

            return sb_ret.ToString();
        }

        public static string ListaNaString<T>(this ICollection<T> lista, params string[] parametry) where T : class {
            var sb_ret = new StringBuilder();
            var sb_header = new StringBuilder();
            var sb_content = new StringBuilder();

            foreach (var param in parametry) {
                sb_header.Append((param + '\t').Replace('\n', ' '));
            }

            sb_ret.AppendLine(sb_header.ToString());

            foreach (var item in lista) {
                foreach (var param in parametry) {
                    var o = typeof(T).GetProperty(param);

                    if (o != null)
                        sb_content.Append((o.GetValue(item)?.ToString() + '\t').Replace('\n', ' '));
                    else
                        sb_content.Append('\t');
                }

                sb_content.Append("\r\n");
            }

            sb_ret.AppendLine(sb_content.ToString());

            return sb_ret.ToString();
        }
    }
}
