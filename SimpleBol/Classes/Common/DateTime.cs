

namespace SimpleBol.Classes.Common
{
    public class DateTimeCommon
    {
        public static DateTime MakeMonthStartDate(int monthsBack)
        {
            var month = DateTime.Now.AddMonths(monthsBack);
            var fd = MakeFirstDayOfMonth(month);
            return new DateTime(month.Year, month.Month, fd.Day, 0, 0, 0);
        }

        public static DateTime MakeMonthStopDate(int monthsBack)
        {
            var month = DateTime.Now.AddMonths(monthsBack);
            var fd = MakeLastDayOfMonth(month);
            return new DateTime(month.Year, month.Month, fd.Day, 23, 59, 59);
        }

        public static DateTime MakeFirstDayOfMonth(DateTime sourceDate)
        {
            return new DateTime(sourceDate.Year, sourceDate.Month, 1);
        }

        public static DateTime MakeLastDayOfMonth(DateTime sourceDate)
        {
            var daysInMonth = DateTime.DaysInMonth(sourceDate.Year, sourceDate.Month);
            return new DateTime(sourceDate.Year, sourceDate.Month, daysInMonth);
        }

        public static int MonthDifference(DateTime first, DateTime second)
        {
            return Math.Abs((first.Month - second.Month) + 12 * (first.Year - second.Year));
        }

        public static DateTime MakeOneYearStartDate(int yearsBack)
        {
            var year = DateTime.Now.AddYears(yearsBack).Year;
            return new DateTime(year, 1, 1, 0, 0, 0);
        }

        public static DateTime MakeOneYearStopDate(int yearsBack)
        {
            var year = DateTime.Now.AddYears(yearsBack).Year;
            return new DateTime(year, 12, 31, 23, 59, 59);
        }

        public static DateTime MakeLastYearCompareStartDate()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            return new DateTime(year, 1, 1, 0, 0, 0);
        }

        public static DateTime MakeLastYearCompareStopDate()
        {
            var year = DateTime.Now.AddYears(-1).Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            var days = DateTime.DaysInMonth(year, month);

            // Check for leap years
            if (day > days) { day = days; }

            return new DateTime(year, month, days, 23, 59, 59);
        }
    }
}
