using HolidayCalculator.Core.Providers;
using Xunit;

namespace HolidayCalculator.Core.Test.Providers
{
    public class SampleCalendarProviderTest
    {
        private readonly SampleCalendarProvider _provider;

        public SampleCalendarProviderTest()
        {
            _provider = new SampleCalendarProvider();
        }

        [Fact]
        public void TestIsHolday()
        {
            // Arrange
            var date1 = new DateTime(2023, 1, 1);
            var date2 = new DateTime(2023, 1, 2);

            // Act
            var result1 = _provider.IsHoliday(date1);
            var result2 = _provider.IsHoliday(date2);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void TestGetHolidays()
        {
            // Arrange
            var year = 2023;

            // Act
            var holidays = _provider.GetHolidays(year, includeWeekend: true);

            // Assert
            Assert.NotEmpty(holidays);
            Assert.Equal(52 * 2, holidays.Count);
        }
    }
}
