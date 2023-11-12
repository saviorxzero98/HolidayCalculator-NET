using System;

namespace HolidayCalculator.Core.Entities
{
    public interface ICalendarDay
    {
        string Name { get; set; }

        string LocalName { get; set; }

        DateTime Date { get; set; }

        bool IsHoliday { get; set; }
    }
}
