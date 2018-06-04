using System;
using System.Diagnostics;
using WasterDAL.Model;

namespace WasteBatchConsole
{
    using System.Collections.Generic;
    using System.Linq;

    public static class StatsCalculator
    {
        public static double CalculateStats(IEnumerable<TrackRecord> monthRecords, DateTime periodStart, DateTime periodEnd)
        {

            var totalSecondsForMonth = 0d;
            foreach (var currentTrackRecord in monthRecords)
            {
                if (currentTrackRecord.IsCancelled)
                    continue;


                if (currentTrackRecord.StartDate.Date < periodStart && currentTrackRecord.EndDate.HasValue && currentTrackRecord.EndDate.Value > periodStart)
                {
                    totalSecondsForMonth += (currentTrackRecord.EndDate.Value - periodStart).TotalSeconds;
                }
                else if (currentTrackRecord.EndDate.HasValue && currentTrackRecord.EndDate.Value > periodEnd &&
                         periodEnd > currentTrackRecord.StartDate)
                {
                    totalSecondsForMonth += (periodEnd - currentTrackRecord.StartDate).TotalSeconds;
                }
                else if (currentTrackRecord.StartDate >= periodStart && currentTrackRecord.EndDate.HasValue && currentTrackRecord.EndDate.Value < periodEnd)
                {
                    totalSecondsForMonth += (currentTrackRecord.EndDate.Value - currentTrackRecord.StartDate).TotalSeconds;
                }
                else
                {
                    //Log error
                    Trace.WriteLine(string.Format("Track record id ={0} is missing end date", currentTrackRecord.Id));
                }
            }

            return  totalSecondsForMonth;
        }
    }
}