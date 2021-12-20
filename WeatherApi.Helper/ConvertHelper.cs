using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WeatherApi.Helpers
{
    public class ConvertHelper
    {
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
            return dtDateTime;
        }

        public static string ChangeDelimeter(double value)
        {
            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            return value.ToString(nfi);
        }
    }
}
