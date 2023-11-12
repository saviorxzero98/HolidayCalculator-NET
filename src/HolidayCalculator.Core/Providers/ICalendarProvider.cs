using HolidayCalculator.Core.Entities;
using System;
using System.Collections.Generic;

namespace HolidayCalculator.Core.Providers
{
    public interface ICalendarProvider
    {
        /// <summary>
        /// Is holiday
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        bool IsHoliday(DateTime date);

        /// <summary>
        /// Get all festivals
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        List<ICalendarDay> GetFestivals(int year);

        /// <summary>
        /// Get all holidays
        /// </summary>
        /// <param name="year"></param>
        /// <param name="includeWeekend"></param>
        /// <returns></returns>
        List<ICalendarDay> GetHolidays(int year, bool includeWeekend);
    }
}
