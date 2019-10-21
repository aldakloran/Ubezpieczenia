using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WaslickiUbezpieczenia.Klasy {
    public static class StringExtenstions {
        #region Parsery

        #region Parsowanie generyczne

        public static T Parse<T>(this string value) {
            switch (Type.GetTypeCode(typeof(T))) {
                case TypeCode.String:
                case TypeCode.Object:
                    return (T)Convert.ChangeType(value, typeof(T));
                case TypeCode.Int16:
                    return (T)Convert.ChangeType(value.GetShort(), typeof(T));
                case TypeCode.UInt16:
                    return (T)Convert.ChangeType(value.GetUShort(), typeof(T));
                case TypeCode.Int32:
                    return (T)Convert.ChangeType(value.GetInt(), typeof(T));
                case TypeCode.UInt32:
                    return (T)Convert.ChangeType(value.GetUInt(), typeof(T));
                case TypeCode.Int64:
                    return (T)Convert.ChangeType(value.GetLong(), typeof(T));
                case TypeCode.UInt64:
                    return (T)Convert.ChangeType(value.GetULong(), typeof(T));
                case TypeCode.Single:
                    return (T)Convert.ChangeType(value.GetFloat(), typeof(T));
                case TypeCode.Double:
                    return (T)Convert.ChangeType(value.GetDouble(), typeof(T));
                case TypeCode.Decimal:
                    return (T)Convert.ChangeType(value.GetDecimal(), typeof(T));
                case TypeCode.DateTime:
                    return (T)Convert.ChangeType(value.GetDateTime(), typeof(T));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool CanParse<T>(this string value) {
            switch (Type.GetTypeCode(typeof(T))) {
                case TypeCode.String:
                case TypeCode.Object:
                    return true;
                case TypeCode.Int16:
                    return value.CanGetShort();
                case TypeCode.UInt16:
                    return value.CanGetUShort();
                case TypeCode.Int32:
                    return value.CanGetInt();
                case TypeCode.UInt32:
                    return value.CanGetUInt();
                case TypeCode.Int64:
                    return value.CanGetLong();
                case TypeCode.UInt64:
                    return value.CanGetULong();
                case TypeCode.Single:
                    return value.CanGetFloat();
                case TypeCode.Double:
                    return value.CanGetDouble();
                case TypeCode.Decimal:
                    return value.CanGetDecimal();
                case TypeCode.DateTime:
                    return value.CanGetDateTime();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Parsowanie int

        public static int GetInt(this string value) {
            const int default_value = 0;

            if (!int.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out int result) &&
                !int.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetInt(this string value) {
            if (!int.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out int result) &&
                !int.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie short

        public static short GetShort(this string value) {
            const short default_value = 0;

            if (!short.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out short result) &&
                !short.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !short.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetShort(this string value) {
            if (!short.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out short result) &&
                !short.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !short.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie long

        public static long GetLong(this string value) {
            const long default_value = 0;

            if (!long.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out long result) &&
                !long.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetLong(this string value) {
            if (!long.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out long _) &&
                !long.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out long _) &&
                !long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long _)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie float

        public static float GetFloat(this string value) {
            const float default_value = 0;

            if (!float.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out float result) &&
                !float.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetFloat(this string value) {
            if (!float.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out float result) &&
                !float.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie double
        public static double GetDouble(this string value) {
            const double default_value = 0;

            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out double result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetDouble(this string value) {
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out double result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie decimal

        public static decimal GetDecimal(this string value) {
            const decimal default_value = 0;

            if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result) &&
                !decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetDecimal(this string value) {
            if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result) &&
                !decimal.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie uint

        public static long GetUInt(this string value) {
            const uint default_value = 0;

            if (!uint.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out uint result) &&
                !uint.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !uint.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetUInt(this string value) {
            if (!uint.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out uint _) &&
                !uint.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out uint _) &&
                !uint.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out uint _)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie ushort

        public static ushort GetUShort(this string value) {
            const ushort default_value = 0;

            if (!ushort.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out ushort result) &&
                !ushort.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !ushort.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetUShort(this string value) {
            if (!ushort.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out ushort _) &&
                !ushort.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out ushort _) &&
                !ushort.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ushort _)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie ulong

        public static ulong GetULong(this string value) {
            const ulong default_value = 0;

            if (!ulong.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out ulong result) &&
                !ulong.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !ulong.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetULong(this string value) {
            if (!ulong.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out ulong _) &&
                !ulong.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out ulong _) &&
                !ulong.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ulong _)) {

                return false;
            }

            return true;
        }

        #endregion

        #region Parsowanie DateTime

        public static DateTime GetDateTime(this string value) {
            DateTime default_value = new DateTime();

            if (!DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime result) &&
                !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out result) &&
                !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out result)) {

                result = default_value;
            }

            return result;
        }

        public static bool CanGetDateTime(this string value) {
            if (!DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime _) &&
                !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime _) &&
                !DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out DateTime _)) {

                return false;
            }

            return true;
        }

        #endregion

        #endregion


        #region Szyfrowanie

        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;

        public static string Szyfruj(this string plainText, string passPhrase) {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        public static string Odszyfruj(this string cipherText, string passPhrase) {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        #endregion

        #region Narzedzia

        public static bool IsNullOrEmpty(this string value) {
            return string.IsNullOrEmpty(value);
        }

        #endregion
    }
}