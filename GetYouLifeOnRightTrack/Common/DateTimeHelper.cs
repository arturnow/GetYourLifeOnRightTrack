using System;

namespace Common
{
    //TODO: Move to common project
    public static class DateTimeHelper
    {
        public static DateTime GetMonthStart(DateTime today)
        {
            return today.AddDays(1 - today.Day).Date;
        }

        public static DateTime GetWeekStart(DateTime today)
        {
            today = today.Date;

            var diff = today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)today.DayOfWeek;

            //Monday is 1, Sunday is 0
            var weekStart = today.AddDays((int)DayOfWeek.Monday - diff);

            return weekStart;
        }
    }
}