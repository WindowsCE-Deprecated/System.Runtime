// Ref: https://opennetcf.codeplex.com/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

#if NET35_CF
namespace System
#else
namespace Mock.System
#endif
{
    public static class Enum2
    {
        private static readonly Type SystemEnumType = typeof(Enum);

        /// <summary>
        /// Converts the specified value of a specified enumerated type to its equivalent string representation according to the specified format. 
        /// </summary>
        /// <remarks>
        /// The valid format values are: 
        /// "G" or "g" - If value is equal to a named enumerated constant, the name of that constant is returned; otherwise, the decimal equivalent of value is returned.
        /// For example, suppose the only enumerated constant is named, Red, and its value is 1. If value is specified as 1, then this format returns "Red". However, if value is specified as 2, this format returns "2".
        /// "X" or "x" - Represents value in hexadecimal without a leading "0x". 
        /// "D" or "d" - Represents value in decimal form.
        /// "F" or "f" - Behaves identically to "G" or "g", except the FlagsAttribute is not required to be present on the Enum declaration. 
        /// "V" or "v" - If value is equal to a named enumerated constant, the value of that constant is returned; otherwise, the decimal equivalent of value is returned.
        /// </remarks>
        /// <param name="enumType">The enumeration type of the value to convert.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">The output format to use.</param>
        /// <returns>A string representation of value.</returns>
        public static string Format(Type enumType, object value, string format)
        {
            if (enumType == null) throw new ArgumentNullException("enumType");
            if (value == null) throw new ArgumentNullException("value");
            if (format == null) throw new ArgumentNullException("format");
            if (!enumType.IsEnum) throw new ArgumentException("The argument enumType must be an System.Enum.");

            if (format.Length != 1)
                throw new FormatException("Invalid format");

            char formatCh = format[0];
            if (formatCh == 'G' || formatCh == 'g')
                return InternalFormat(enumType, value);
            if (formatCh == 'F' || formatCh == 'f')
                return InternalValuesFormat(enumType, value, false);
            if (formatCh == 'V' || formatCh == 'v')
                return InternalValuesFormat(enumType, value, true);
            if (formatCh == 'X' || formatCh == 'x')
                return InternalFormattedHexString(value);
            if (formatCh == 'D' || formatCh == 'd')
                return Convert.ToUInt64(value).ToString();

            throw new FormatException("Invalid format");
        }

        /// <summary>
        /// Retrieves the name of the constant in the specified enumeration that has the specified value.
        /// </summary>
        /// <param name="enumType">An enumeration type.</param>
        /// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
        /// <returns> A string containing the name of the enumerated constant in enumType whose value is value, or null if no such constant is found.</returns>
        /// <exception cref="ArgumentException"> enumType is not an System.Enum.  -or-  value is neither of type enumType nor does it have the same underlying type as enumType.</exception>
        public static string GetName(Type enumType, object value)
        {
            //check that the type supplied inherits from System.Enum
            if (enumType.BaseType != SystemEnumType)
            {
                //the type supplied does not derive from enum
                throw new ArgumentException(
                    "Cannot get name from type not derived from System.Enum",
                    nameof(enumType));
            }

            //get details of all the public static fields (enum items)
            FieldInfo[] fi = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            //cycle through the enum values
            foreach (FieldInfo thisField in fi)
            {
                object numericValue = 0;

                try
                {
                    //convert the enum value to the numeric type supplied
                    numericValue = Convert.ChangeType(thisField.GetValue(null), value.GetType(), null);
                }
                catch
                {
                    throw new ArgumentException();
                }

                //if value matches return the name
                if (numericValue.Equals(value))
                {
                    return thisField.Name;
                }
            }
            //if there is no match return null
            return null;
        }

        /// <summary>
        /// Retrieves an array of the names of the constants in a specified enumeration.
        /// </summary>
        /// <param name="enumType">An enumeration type.</param>
        /// <returns>A string array of the names of the constants in enumType. The elements of the array are sorted by the values of the enumerated constants.</returns>
        /// <exception cref="System.ArgumentException">enumType parameter is not an System.Enum</exception>
        public static string[] GetNames(Type enumType)
        {
            //check that the type supplied inherits from System.Enum
            if (enumType.BaseType != SystemEnumType)
            {
                //the type supplied does not derive from enum
                throw new ArgumentException(
                    "Cannot get names from type not derived from System.Enum",
                    nameof(enumType));
            }

            //get the public static fields (members of the enum)
            FieldInfo[] fi = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            //create a new enum array
            string[] names = new string[fi.Length];

            //populate with the values
            for (int iEnum = 0; iEnum < fi.Length; iEnum++)
            {
                names[iEnum] = fi[iEnum].Name;
            }

            //return the array
            return names;
        }

        public static Type GetUnderlyingType(Type enumType)
            => Enum.GetUnderlyingType(enumType);

        /// <summary>
        /// Retrieves an array of the values of the constants in a specified enumeration.
        /// </summary>
        /// <param name="enumType">An enumeration type.</param>
        /// <returns>An System.Array of the values of the constants in enumType. The elements of the array are sorted by the values of the enumeration constants.</returns>
        /// <exception cref="System.ArgumentException">enumType parameter is not an System.Enum</exception>
        public static Array GetValues(Type enumType)
        {
            //check that the type supplied inherits from System.Enum
            if (enumType.BaseType != SystemEnumType)
            {
                //the type supplied does not derive from enum
                throw new ArgumentException(
                    "Cannot get names from type not derived from System.Enum",
                    nameof(enumType));
            }

            //get the public static fields (members of the enum)
            FieldInfo[] fi = enumType.GetFields(BindingFlags.Static | BindingFlags.Public);

            //create a new enum array
            Array values = Array.CreateInstance(enumType, fi.Length);

            //populate with the values
            for (int iEnum = 0; iEnum < fi.Length; iEnum++)
            {
                values.SetValue(fi[iEnum].GetValue(null), iEnum);
            }

            //return the array
            return values;
        }

        public static bool HasFlag(this Enum e, Enum flag)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag));

            {
                var t_e = e.GetType();
                var t_flag = flag.GetType();
                if (t_e != t_flag)
                    throw new ArgumentException($"The type '{t_flag.Name}' does not match '{t_e.Name}");
            }

            ulong num = Convert.ToUInt64(flag);
            return ((Convert.ToUInt64(e) & num) == num);
        }

        public static bool IsDefined(Type enumType, object value)
            => Enum.IsDefined(enumType, value);

        public static object Parse(Type enumType, string value)
            => Enum.Parse(enumType, value, false);

        public static object Parse(Type enumType, string value, bool ignoreCase)
            => Enum.Parse(enumType, value, ignoreCase);

        public static object ToObject(Type enumType, object value)
            => Enum.ToObject(enumType, value);

        public static bool TryParse<TEnum>(string value, out TEnum result)
            where TEnum : struct
        {
            bool retVal = false;
            try
            {
                object parsed = Enum.Parse(typeof(TEnum), value, false);
                result = (TEnum)parsed;
                retVal = true;
            }
            catch (FormatException) { result = (TEnum)(object)0; }
            catch (InvalidCastException) { result = (TEnum)(object)0; }

            return retVal;
        }

        public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result)
            where TEnum : struct
        {
            bool retVal = false;
            try
            {
                object parsed = Enum.Parse(typeof(TEnum), value, ignoreCase);
                result = (TEnum)parsed;
                retVal = true;
            }
            catch (FormatException) { result = (TEnum)(object)0; }
            catch (InvalidCastException) { result = (TEnum)(object)0; }

            return retVal;
        }

        private static string InternalFormat(Type enumType, object value)
        {
            if (enumType.IsDefined(typeof(FlagsAttribute), false))
                return InternalFlagsFormat(enumType, value);

            string t = GetName(enumType, value);
            if (t == null)
                return value.ToString();
            
            return t;
        }

        private static string InternalFlagsFormat(Type enumType, object value)
        {
            string t = GetName(enumType, value);
            if (t == null)
                return value.ToString();

            return t;
        }

        private static string InternalValuesFormat(Type enumType, object value, bool showValues)
        {
            string[] names = null;

            if (!showValues)
                names = GetNames(enumType);

            ulong v = Convert.ToUInt64(value);
            Array e = GetValues(enumType);
            List<string> al = new List<string>();
            for (int i = 0; i < e.Length; i++)
            {
                ulong ev = (ulong)Convert.ChangeType(e.GetValue(i), typeof(ulong), null);
                if (i == 0 && ev == 0) continue;
                if ((v & ev) == ev)
                {
                    v -= ev;
                    if (showValues)
                        al.Add(ev.ToString());
                    else
                        al.Add(names[i]);
                }
            }

            if (v != 0)
                return value.ToString();


            string[] t = al.ToArray();
            return string.Join(", ", t);
        }

        private static string InternalFormattedHexString(object value)
        {
            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.SByte:
                    {
                        sbyte n = (sbyte)value;
                        return n.ToString("X2", null);
                    }
                case TypeCode.Byte:
                    {
                        byte n = (byte)value;
                        return n.ToString("X2", null);
                    }
                case TypeCode.Int16:
                    {
                        short n = (short)value;
                        return n.ToString("X4", null);
                    }
                case TypeCode.UInt16:
                    {
                        ushort n = (ushort)value;
                        return n.ToString("X4", null);
                    }
                case TypeCode.Int32:
                    {
                        int n = (int)value;
                        return n.ToString("X8", null);
                    }
                case TypeCode.UInt32:
                    {
                        uint n = (uint)value;
                        return n.ToString("X8", null);
                    }

            }

            throw new InvalidOperationException("Unknown enum type.");
        }
    }
}
