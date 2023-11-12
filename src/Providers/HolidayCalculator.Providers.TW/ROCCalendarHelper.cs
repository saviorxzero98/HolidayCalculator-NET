using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HolidayCalculator.Providers.TW
{
    /// <summary>
    /// 中華民國年分轉換
    /// </summary>
    public static class ROCCalendarHelper
    {
        public const string LocalName = "zh-TW";
        public const int FirstYear = 1912;

        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToROCDate(DateTime date)
        {
            if (date.Year < FirstYear)
            {
                return default(DateTime);
            }

            var tawianCalendar = new TaiwanCalendar();
            int year = tawianCalendar.GetYear(date);

            return new DateTime(year, date.Month, date.Day, date.Hour, date.Month, date.Second);
        }
        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime ToROCDate(int year, int month, int day,
                                         int hour = 0, int minute = 0, int second = 0)
        {
            return ToROCDate(new DateTime(year, month, day, hour, month, minute, second));
        }

        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FromROCDate(DateTime date)
        {
            var tawianCalendar = new TaiwanCalendar();
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Month, date.Second, tawianCalendar);
        }
        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FromROCDate(int year, int month, int day,
                                           int hour = 0, int minute = 0, int second = 0)
        {
            return FromROCDate(new DateTime(year, month, day, hour, month, minute, second));
        }

        /// <summary>
        /// 轉民國年字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToROCDateString(this DateTime value, string format)
        {
            CultureInfo culture = new CultureInfo(LocalName);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return value.ToString(format, culture);
        }

        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(LocalName);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, string format, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(LocalName);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (DateTime.TryParseExact(rocDateString, format, culture, DateTimeStyles.None, out date))
            {
                return true;
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="formats"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseROCDate(string rocDateString, IEnumerable<string> formats, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(LocalName);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (formats != null && formats.ToArray().Length != 0)
            {
                if (DateTime.TryParseExact(rocDateString, formats.ToArray(), culture, DateTimeStyles.None, out date))
                {
                    return true;
                }
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
    }
}
