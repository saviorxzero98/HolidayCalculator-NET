using System;
using System.Globalization;
using System.Threading;

namespace HolidayCalculator.Core
{
    public static class CalendarHelper
    {
        #region DateTime

        /// <summary>
        /// Create date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime Create(int year, int month, int day,
                                      int hour = 0, int minute = 0, int second = 0)
        {
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Find first date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="weekOfMonth"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FindFirstDate(int year, int month, int weekOfMonth, DayOfWeek dayOfWeek,
                                             int hour = 0, int minute = 0, int second = 0)
        {
            if (weekOfMonth < 1 || weekOfMonth > 5)
            {
                return default(DateTime);
            }

            var firstDayOfMonth = new DateTime(year, month, 1);
            var daysNeeded = (int)dayOfWeek - (int)firstDayOfMonth.DayOfWeek;

            if (daysNeeded < 0)
            {
                daysNeeded = daysNeeded + 7;
            }

            var resultedDay = (daysNeeded + 1) + (7 * (weekOfMonth - 1));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return default(DateTime);
            }
            else
            {
                return new DateTime(year, month, resultedDay, hour, minute, second);
            }
        }

        /// <summary>
        /// Find last date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="weekOfMonth"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FindLastDate(int year, int month, int weekOfMonth, DayOfWeek dayOfWeek,
                                            int hour = 0, int minute = 0, int second = 0)
        {
            if (weekOfMonth < 1 || weekOfMonth > 5)
            {
                return default(DateTime);
            }

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var lastDayOfMonth = new DateTime(year, month, daysInMonth);
            var daysNeeded = (int)lastDayOfMonth.DayOfWeek - (int)dayOfWeek;

            if (daysNeeded < 0)
            {
                daysNeeded += 7;
            }

            var resultedDay = daysInMonth - ((daysNeeded) + (7 * (weekOfMonth - 1)));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return default(DateTime);
            }
            else
            {
                return new DateTime(year, month, resultedDay, hour, minute, second);
            }
        }

        /// <summary>
        /// Get First Day Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfYear(DateTime date)
        {
            return FirstDayOfYear(date).AddYears(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Get First Day Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime date)
        {
            return FirstDayOfMonth(date).AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Day Of Week
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            return FirstTimeOfDay(date).AddDays((int)DayOfWeek.Sunday - (int)dayOfWeek);
        }

        /// <summary>
        /// Last Day Of Week
        /// </summary>
        public static DateTime LastDayOfWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            return LastTimeOfDay(date).AddDays((int)DayOfWeek.Saturday - (int)dayOfWeek);
        }

        /// <summary>
        /// First Millisecond Of Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfDay(DateTime date)
        {
            return FirstTimeOfDay(date).AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Hour
        /// </summary>
        public static DateTime LastTimeOfHour(DateTime date)
        {
            return FirstTimeOfHour(date).AddHours(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Minute
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfMinute(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Minute
        /// </summary>
        public static DateTime LastTimeOfMinute(DateTime date)
        {
            return FirstTimeOfMinute(date).AddMinutes(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Second
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfSecond(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
        }

        /// <summary>
        /// Last Millisecond Of Second
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfSecond(DateTime date)
        {
            return FirstTimeOfSecond(date).AddSeconds(1).AddMilliseconds(-1);
        }

        #endregion


        #region Date Name

        /// <summary>
        /// Get month name
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetMonthString(DateTime date)
        {
            byte index = (byte)Convert.ToDateTime(date).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var month = info.MonthNames[index];
            return month;
        }
        /// <summary>
        /// Get month name
        /// </summary>
        /// <param name="date"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetMonthString(DateTime date, string location)
        {
            byte index = (byte)Convert.ToDateTime(date).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var month = info.MonthNames[index];
            return month;
        }

        /// <summary>
        /// Get week name
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(DateTime date)
        {
            byte index = (byte)Convert.ToDateTime(date).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }
        /// <summary>
        /// Get week name
        /// </summary>
        /// <param name="date"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(DateTime date, string location)
        {
            byte index = (byte)Convert.ToDateTime(date).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }

        #endregion
    }
}
