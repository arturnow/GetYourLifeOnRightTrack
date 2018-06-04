using System;
using System.Linq;
using Common;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class PeriodService : IPeriodService
    {
        private readonly IPeriodRepository _repository;

        public PeriodService(IPeriodRepository repository)
        {
            _repository = repository;
        }

        public Period GetDayPeriod(DateTime forDate)
        {
            var todayPeriod = _repository.Query(period => true).FirstOrDefault(
                 p => p.Type == PeriodType.Day && p.StartDate == forDate.Date);

            return todayPeriod;
        }

        public Period GetWeekPeriod(DateTime forDate)
        {
            DateTime weekStart = DateTimeHelper.GetWeekStart(forDate);

            var weekPeriod = _repository.Query(period => true).FirstOrDefault(
                p => p.Type == PeriodType.Week && p.StartDate == weekStart && p.EndDate > weekStart);
            return weekPeriod;
        }

        public Period GetMonthPeriod(DateTime forDate)
        {
            DateTime monthStart = DateTimeHelper.GetMonthStart(forDate);
            
            var monthPeriod = _repository.Query(period => true).FirstOrDefault(
                  p => p.Type == PeriodType.Month && p.StartDate == monthStart && p.EndDate > monthStart);
            return monthPeriod;
        }

        public Period GetQuarterPeriod(DateTime forDate)
        {
            throw new NotImplementedException();
        }

        public long CreateDayPeriod(DateTime forDate)
        {
            var todayPeriod = new Period
            {
                Duration = 1,
                Type = PeriodType.Day,
                StartDate = forDate.Date,
                EndDate = forDate.Date.AddDays(1)
            };

            return _repository.Create(todayPeriod);
        }

        public long CreateWeekPeriod(DateTime forDate)
        {
            DateTime weekStart = DateTimeHelper.GetWeekStart(forDate);

            var weekPeriod = new Period
            {
                Duration = 7,
                Type = PeriodType.Week,
                StartDate = weekStart,
                EndDate = weekStart.AddDays(7)
            };
            return _repository.Create(weekPeriod);
        }

        public long CreateMonthPeriod(DateTime forDate)
        {
            DateTime monthStart = DateTimeHelper.GetMonthStart(forDate);
            var monthPeriod = new Period
            {
                Duration = (monthStart.AddMonths(1) - monthStart).Days,
                Type = PeriodType.Month,
                StartDate = monthStart,
                EndDate = monthStart.AddMonths(1)
            };
            return _repository.Create(monthPeriod);
        }

        public long CreateQuarterPeriod(DateTime forDate)
        {
            throw new NotImplementedException();
        }
    }
}