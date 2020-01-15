using System;
using System.Globalization;

namespace Service.Extension
{
	public static class StringExtension
    {
		private static readonly IFormatProvider Formatter;
		private const NumberStyles Style = NumberStyles.Float;

		static StringExtension()
		{
			Formatter = new NumberFormatInfo {NumberDecimalSeparator = "."};
		}
		public static bool ToTryParseToDouble(this string value, out double ar)
		{
			return double.TryParse(value.Replace(",", "."), Style, Formatter, out ar);
		}

		public static double ConvertToDouble(this string value)
		{
			return value.ToTryParseToDouble(out double ar) ? ar : 0;
		}
	}
}