using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HolidayCalculator.Core
{
    public class LunarCalendarConverter
    {
        /// <summary>
        /// Solar calendar to lunar calendar
        /// </summary>
        /// <param name="solarDate"></param>
        /// <returns></returns>
        public static LunarDateTime ToLunarDate(DateTime solarDate)
        {
            var lunisolar = new ChineseLunisolarCalendar();

            int year = lunisolar.GetYear(solarDate);
            int month = lunisolar.GetMonth(solarDate);
            int day = lunisolar.GetDayOfMonth(solarDate);
            int leapMonth = lunisolar.GetLeapMonth(year);
            bool isLeapMonth = false;

            if (leapMonth > 0)
            {
                if (month == leapMonth)
                {
                    month = leapMonth - 1;
                    isLeapMonth = true;
                }

                if (month > leapMonth)
                {
                    month = month - 1;
                }
            }

            int hour = solarDate.Hour;
            int minute = solarDate.Minute;
            int second = solarDate.Second;

            return new LunarDateTime(year, month, day, isLeapMonth, hour, minute, second);
        }
        
        /// <summary>
        /// Solar calendar to lunar calendar
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static LunarDateTime ToLunarDate(int year, int month, int day,
                                                int hour = 0, int minute = 0, int second = 0)
        {
            DateTime solarDate = Create(year, month, day, hour, minute, second);
            return ToLunarDate(solarDate);
        }

        /// <summary>
        /// Lunar calendar to solar calendar
        /// </summary>
        /// <param name="lunarDate"></param>
        /// <returns></returns>
        public static DateTime FromLunarDate(LunarDateTime lunarDate)
        {
            var lunisolar = new ChineseLunisolarCalendar();
            int year = lunarDate.Year;
            int month = lunarDate.Month;
            int day = lunarDate.Day;
            int leapMonth = lunisolar.GetLeapMonth(year);

            if (lunarDate.IsLeapMonth)
            {
                month = leapMonth;
            }
            else
            {
                if (leapMonth != 0 && month >= leapMonth)
                {
                    month = month + 1;
                }
            }

            int hour = lunarDate.Hour;
            int minute = lunarDate.Minute;
            int second = lunarDate.Second;
            return new DateTime(year, month, day, hour, minute, second, lunisolar);
        }
        
        /// <summary>
        /// Lunar calendar to solar calendar
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="isLeapMonth"></param>
        /// <param name="hours"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FromLunarDate(int year, int month, int day, bool isLeapMonth = false,
                                             int hours = 0, int minute = 0, int second = 0)
        {
            var lunisolar = new ChineseLunisolarCalendar();
            int leapMonth = lunisolar.GetLeapMonth(year);

            if (isLeapMonth)
            {
                month = leapMonth;
            }
            else
            {
                if (leapMonth != 0 && month >= leapMonth)
                {
                    month++;
                }
            }
            return new DateTime(year, month, day, hours, minute, second, lunisolar);
        }
    }
}
