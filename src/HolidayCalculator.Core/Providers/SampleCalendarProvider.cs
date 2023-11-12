using HolidayCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayCalculator.Core.Providers
{
    public class SampleCalendarProvider : ICalendarProvider
    {
        /// <summary>
        /// Is holiday
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// Get all festivals
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public virtual List<ICalendarDay> GetFestivals(int year)
        {
            return new List<ICalendarDay>();
        }

        /// <summary>
        /// Get all holidays
        /// </summary>
        /// <param name="year"></param>
        /// <param name="includeWeekend"></param>
        /// <returns></returns>
        public virtual List<ICalendarDay> GetHolidays(int year, bool includeWeekend)
        {
            if (includeWeekend)
            {
                var weekends = GetDaysBetween(new DateTime(year, 1, 1), new DateTime(year, 12, 31))
                                    .Where(d => d.DayOfWeek == DayOfWeek.Saturday || 
                                                d.DayOfWeek == DayOfWeek.Sunday);
                var days = weekends.Select(date => new Festival(CalendarHelper.GetDayOfWeekString(date), date, true))
                                   .AsEnumerable<ICalendarDay>()
                                   .ToList();
                return days;
            }
            else
            {
                return new List<ICalendarDay>();
            }
        }

        /// <summary>
        /// Get Days
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private IEnumerable<DateTime> GetDaysBetween(DateTime start, DateTime end)
        {
            for (DateTime i = start; i < end; i = i.AddDays(1))
            {
                yield return i;
            }
        }
    }
}
