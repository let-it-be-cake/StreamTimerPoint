namespace STP.DataLayer.Models
{
    public struct TimeStamp
    {
        public string Name { get; set; }

        public TimeSpan Timestamp { get; set; }

        public TimeSpan TimeAdjustment { get; set; }

        public TimeSpan LiveTimeStamp => Timestamp + TimeAdjustment;
    }
}
