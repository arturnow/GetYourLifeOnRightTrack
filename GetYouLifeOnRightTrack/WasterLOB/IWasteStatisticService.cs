using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WasterLOB
{
    using WasterDAL.Model;

    public interface IWasteStatisticService
    {
        long CreateStatistics(long totalSeconds, int entryCount, int userId, long periodId);

        bool StatisticsExists(int userId, long periodId);
        WasteStatistic GetStats(int userId, long periodId);
        void UpdateStats(WasteStatistic statsToUpdate);

        IEnumerable<WasteStatistic> GetCurrentDailyMonthStatsWithPeriodInfo(int userId);
        WasteStatistic GetCurrentWeekSummaryWithPeriodInfo(int userId);
        WasteStatistic GetCurrentMonthSummaryWithPeriodInfo(int userId);
    }
}
