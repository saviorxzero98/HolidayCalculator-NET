using HolidayCalculator.Core;
using HolidayCalculator.Core.Entities;
using HolidayCalculator.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayCalculator.Providers.TW.Providers
{
    public class TaiwanCalendarProvider : ICalendarProvider
    {
        /// <summary>
        /// 是否為假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime date)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得指定年分的節日
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ICalendarDay> GetFestivals(int year)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得指定年份的假日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="includeWeekend"></param>
        /// <returns></returns>
        public List<ICalendarDay> GetHolidays(int year, bool includeWeekend)
        {
            var calendars = new List<ICalendarDay>();

            // 有固定補假規則的節日
            calendars.Add(new Festival("NewYear", CalendarHelper.Create(year, 1, 1), true, "中華民國開國紀念日"));
            calendars.Add(new Festival("PeaceMemorialDay", CalendarHelper.Create(year, 2, 28), true, "二二八和平紀念日"));
            calendars.Add(new Festival("ChildrenDay", CalendarHelper.Create(year, 4, 4), true, "兒童節"));
            calendars.Add(new Festival("NationalDay", CalendarHelper.Create(year, 10, 10), true, "中華民國國慶日"));

            if (year > 1901 && year < 2100)
            {
                // 清明節計算只計算1901年~2100年區間
                calendars.AddRange(GetQingmingFestival(year));

                // 因為.NET 農曆計算 (ChineseLunisolarCalendar) 只處理西元1901年~2100年區間
                calendars.Add(new Festival("QingmingFestival", LunarCalendarConverter.FromLunarDate(year, 5, 5), true, "端午節"));
                calendars.Add(new Festival("MoonFestival", LunarCalendarConverter.FromLunarDate(year, 8, 15), true, "中秋節"));
            }

            // 處理一般的補假
            calendars = FixHoliday(calendars);

            // 特殊補假規則的節日
            if (year > 1901 && year < 2100)
            {
                // 因為.NET 農曆計算 (ChineseLunisolarCalendar) 只處理西元1901年~2100年區間
                calendars.AddRange(GetChineseNewYear(year)));
            }

            // 排序時間
            calendars = calendars.OrderBy(c => c.Date).ToList();

            return calendars;
        }


        #region 取得節日

        /// <summary>
        /// 取得春節
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<ICalendarDay> GetChineseNewYear(int year)
        {
            var baseDate = LunarCalendarConverter.FromLunarDate(year, 1, 1);

            var calendars = new List<ICalendarDay>()
            {
                new Festival("ChineseNewYearEve", baseDate.AddDays(-1), true, "除夕"),
                new Festival("ChineseNewYear", baseDate, true, "大年初一"),
                new Festival("ChineseNewYear", baseDate.AddDays(1), true, "大年初二"),
                new Festival("ChineseNewYear", baseDate.AddDays(2), true, "大年初三"),
            };

            // 春節補假日計算 (除夕~大年初三逢週六、週日，大年初四或初五彈性補假)
            int fixDays = 0;
            foreach (var calendar in calendars)
            {
                switch (calendar.Date.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        fixDays++;
                        break;
                }
            }

            switch (fixDays)
            {
                case 1:
                    calendars.Add(new Festival("ChineseNewYear", baseDate.AddDays(3), true, "大年初四"));
                    break;
                case 2:
                    calendars.Add(new Festival("ChineseNewYear", baseDate.AddDays(3), true, "大年初四"));
                    calendars.Add(new Festival("ChineseNewYear", baseDate.AddDays(4), true, "大年初四"));
                    break;
            }
            return calendars;
        }


        /// <summary>
        /// 取得清明節
        /// </summary>
        /// <param name="year"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<ICalendarDay> GetQingmingFestival(int year)
        {
            var calendars = new List<ICalendarDay>();
            DateTime date;
            int leapYear = year % 4;

            if (year >= 1901 && year <= 1911)
            {   // 閏年和下一年是 4/5，其他年份是 4/6
                date = (leapYear < 2) ? new DateTime(year, 4, 5) : new DateTime(year, 4, 6);
            }
            else if (year >= 1912 && year <= 1943)
            {   // 閏年和下兩年是 4/5，其他年份是 4/6
                date = (leapYear < 3) ? new DateTime(year, 4, 5) : new DateTime(year, 4, 6);
            }
            else if (year >= 1944 && year <= 1975)
            {   // 皆為 4/5
                date = new DateTime(year, 4, 5);
            }
            else if (year >= 1976 && year <= 2007)
            {   // 閏年是 4/4，其他年份是 4/5
                date = (leapYear < 1) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2008 && year <= 2039)
            {   // 閏年和下一年是 4/4，其他年份是 4/5
                date = (leapYear < 2) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2040 && year <= 2071)
            {   // 閏年和下兩年是 4/4，其他年份是 4/5
                date = (leapYear < 3) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2072 && year <= 2099)
            {   // 皆為 4/4
                date = new DateTime(year, 4, 4);
            }
            else if (year == 2100)
            {   // 為 4/5
                date = new DateTime(year, 4, 5);
            }
            else
            {
                return calendars;
            }
            calendars.Add(new Festival("QingmingFestival", date, true, "清明節");
            return calendars;
        }

        #endregion


        #region 補假日計算

        /// <summary>
        /// 補假處理，逢週六、往前補假；逢週日、往後補假
        /// </summary>
        protected List<ICalendarDay> FixHoliday(List<ICalendarDay> calendars)
        {
            List<ICalendarDay> holidays = calendars.Where(c => c.IsHoliday).ToList();

            foreach (var holiday in holidays)
            {
                if (holiday.Date.DayOfWeek == DayOfWeek.Saturday)
                {
                    DateTime fixDate = FindFixDate(calendars, holiday.Date, -1);
                    calendars.Add(new Festival($"{holiday.Name} (Fixd)", fixDate, true, $"{holiday.Name} (補假)"));
                }

                if (holiday.Date.DayOfWeek == DayOfWeek.Sunday)
                {
                    DateTime fixDate = FindFixDate(calendars, holiday.Date, 1);
                    calendars.Add(new Festival($"{holiday.Name} (Fixd)", fixDate, true, $"{holiday.Name} (補假)"));
                }
            }

            return calendars;
        }

        /// <summary>
        /// 尋找補假日
        /// </summary>
        /// <param name="calendars"></param>
        /// <param name="baseDate"></param>
        /// <param name="dayIterator"></param>
        /// <returns></returns>
        protected DateTime FindFixDate(List<ICalendarDay> calendars, DateTime baseDate, int dayIterator)
        {
            DateTime nextDate = baseDate.AddDays(dayIterator);

            bool isNotFound = calendars.Any(d => d.IsHoliday &&
                                                 d.Date.ToString("yyyy-MM-dd") == nextDate.ToString("yyyy-MM-dd"));

            if (isNotFound)
            {
                return FindFixDate(calendars, nextDate, dayIterator);
            }
            return nextDate;
        }

        #endregion
    }
}
