using System;

namespace HolidayCalculator.Core.Entities
{
    public class Festival : ICalendarDay
    {
        public string Name { get; set; } = string.Empty;

        public string LocalName { get; set; }

        public DateTime Date { get; set; }

        public bool IsHoliday { get; set; }

        public Festival()
        {
        }
        public Festival(string name, DateTime date, bool isHoliday = false, string localName = "")
        {
            Name = name;
            Date = date;
            IsHoliday = isHoliday;
            LocalName = localName;
        }
    }
}
