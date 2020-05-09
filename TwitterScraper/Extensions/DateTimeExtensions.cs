using System;

namespace TwitterScraper
{
    internal static class DateTimeExtensions
    {
        private static DateTime _unixEpoch = new DateTime(1970, 1, 1);
        
        public static DateTime FromUnixTime(long ticks) 
            => _unixEpoch.AddMilliseconds(ticks).ToLocalTime();
    }
}