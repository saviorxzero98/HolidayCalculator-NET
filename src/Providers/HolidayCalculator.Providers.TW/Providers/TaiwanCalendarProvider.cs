using HolidayCalculator.Core.Entities;
using HolidayCalculator.Core.Providers;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }


        #region 計算假日


        /// <summary>
        /// 計算清明節時間
        /// </summary>
        /// <param name="year"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool TryGetQingmingFestival(int year, out DateTime date)
        {
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
                date = default(DateTime);
                return false;
            }
            return true;
        }

        #endregion
    }
}
