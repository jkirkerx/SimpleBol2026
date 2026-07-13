namespace SimpleBol.Models.MongoDb
{
    public sealed class HoursOfOperation
    {
        public string? TimeZoneId { get; set; }

        public List<DailyHours> Days { get; set; } = [];
    }

    public sealed class DailyHours
    {
        public DayOfWeek Day { get; set; }

        public bool IsClosed { get; set; }

        /// <summary>
        /// Opening time in 24-hour HH:mm format, such as 08:30.
        /// </summary>
        public string? Open { get; set; }

        /// <summary>
        /// Closing time in 24-hour HH:mm format, such as 17:00.
        /// </summary>
        public string? Close { get; set; }
    }
}
