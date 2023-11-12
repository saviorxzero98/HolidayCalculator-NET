using HolidayCalculator.Core.Entities;
using HolidayCalculator.Core.Providers;
using System;
using System.Collections.Generic;

namespace HolidayCalculator.Providers.TW.Providers
{
    public abstract class TaiwanCalendarProviderBase : ICalendarProvider
    {
        /// <summary>
        /// 是否為假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract bool IsHoliday(DateTime date);

        /// <summary>
        /// 取得指定年分的節日
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public abstract List<ICalendarDay> GetFestivals(int year);

        /// <summary>
        /// 取得指定年份的假日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="includeWeekend"></param>
        /// <returns></returns>
        public abstract List<ICalendarDay> GetHolidays(int year, bool includeWeekend);
    }
}
