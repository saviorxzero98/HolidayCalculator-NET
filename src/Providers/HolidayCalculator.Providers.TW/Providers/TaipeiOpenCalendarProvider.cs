using HolidayCalculator.Core.Entities;
using System;
using System.Collections.Generic;

namespace HolidayCalculator.Providers.TW.Providers
{
    /// <summary>
    /// 使用到臺北市資料大平台的資料
    /// </summary>
    /// <remarks>
    /// https://data.taipei/dataset/detail?id=c30ca421-d935-4faa-b523-9c175c8de738
    /// </remarks>
    public class TaipeiOpenCalendarProvider : TaiwanCalendarProviderBase
    {
        /// <summary>
        /// 是否為假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public override bool IsHoliday(DateTime date)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得指定年分的節日
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public override List<ICalendarDay> GetFestivals(int year)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得指定年份的假日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="includeWeekend"></param>
        /// <returns></returns>
        public override List<ICalendarDay> GetHolidays(int year, bool includeWeekend)
        {
            throw new NotImplementedException();
        }
    }
}
