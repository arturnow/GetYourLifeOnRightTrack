using Common;

namespace WasterLOB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WasterDAL.Model;
    using WasterDAL.Repositories;

    public class WasteStatisticService : IWasteStatisticService
    {
        private readonly IWasteStatisticRepository _repository;

        private readonly IIdentityRepository _identityRepository;

        private readonly IPeriodRepository _periodRepository;

        public WasteStatisticService(IWasteStatisticRepository repository, IIdentityRepository identityRepository, IPeriodRepository periodRepository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (identityRepository == null)
            {
                throw new ArgumentNullException("identityRepository");
            }
            if (periodRepository == null)
            {
                throw new ArgumentNullException("periodRepository");
            }
            this._repository = repository;
            this._identityRepository = identityRepository;
            this._periodRepository = periodRepository;
        }

        public long CreateStatistics(long totalSeconds,int entryCount, int userId, long periodId)
        {
            if (this._identityRepository.Get(userId) == null)
            {
                throw new ApplicationException(string.Format("User doesn't exists with id = {0}", userId));
            }
            if (this._periodRepository.Get(periodId) == null)
            {
                throw new ApplicationException(string.Format("User doesn't exists with id = {0}", periodId));
            }
            if (this._repository.Query(statistic => statistic.PeriodId == periodId && statistic.IdentityId == userId).Any())
            {
                throw new ApplicationException(string.Format("Statistic for user with Id={0} and Period Id={1} already exists",userId, periodId));
            }

            var stats = new WasteStatistic { PeriodId = periodId, EntryCount = entryCount, IdentityId = userId, TimeSumInSecond = totalSeconds };

            return this._repository.Create(stats);
        }

        public bool StatisticsExists(int userId, long periodId)
        {
            return
                this._repository.Query(statistic => statistic.IdentityId == userId && statistic.PeriodId == periodId).Any();
        }

        public WasteStatistic GetStats(int userId, long periodId)
        {
            return
                this._repository.Query(statistic => statistic.IdentityId == userId && statistic.PeriodId == periodId).FirstOrDefault();
        }

        public void UpdateStats(WasteStatistic statsToUpdate)
        {
            this._repository.Update(statsToUpdate);
        }

        public IEnumerable<WasteStatistic> GetCurrentDailyMonthStatsWithPeriodInfo(int userId)
        {
            var startDay = DateTimeHelper.GetMonthStart(DateTime.Now);
            var endDay = startDay.AddMonths(1);


            var currentStats =this._repository.Query(
                statistic =>
                    statistic.IdentityId == userId && statistic.Period.Type == PeriodType.Day
                    && statistic.Period.StartDate >= startDay && statistic.Period.StartDate < endDay, "Period").OrderByDescending(statistic => statistic.Period.StartDate);
            return currentStats.ToList();
        }

        public WasteStatistic GetCurrentWeekSummaryWithPeriodInfo(int userId)
        {
            var startDay = DateTimeHelper.GetWeekStart(DateTime.Now);


            var currentStats = this._repository.Query(
                statistic =>
                    statistic.IdentityId == userId && statistic.Period.Type == PeriodType.Week
                    && statistic.Period.StartDate == startDay, "Period");
            return currentStats.FirstOrDefault();
        }

        public WasteStatistic GetCurrentMonthSummaryWithPeriodInfo(int userId)
        {
            var startDay = DateTimeHelper.GetMonthStart(DateTime.Now);


            var currentStats = this._repository.Query(
                statistic =>
                    statistic.IdentityId == userId && statistic.Period.Type == PeriodType.Month
                    && statistic.Period.StartDate == startDay, "Period");
            return currentStats.FirstOrDefault();
        }
    }
}