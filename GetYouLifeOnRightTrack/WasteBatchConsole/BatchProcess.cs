                                                                                                                                                                                    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
                                                                                                                                                                                    using Common;
                                                                                                                                                                                    using WasterDAL;
using WasterDAL.Model;
using WasterDAL.Repositories;
using WasterLOB;

namespace WasteBatchConsole
{
    using WasterWebAPI;

    internal class BatchProcess
    {
        internal static WasteContext _context = null;
        private DateTime _startDateTime;
        private const string BatchName = "PeriodStatsBatch";

        private readonly IPeriodService _service;
        private readonly IIdentityService _identityService;
        private readonly ITrackService _trackService;
        private readonly IWasteStatisticService _wasteStatisticService;
        private readonly IBatchLogService _batchLogService;

        //TODO : Pozbyć się tego i stworzyć UnitOfWork. Repositoria i tak mają inny Context
        private Identity[] _userArray;
        private int _currentUserIndex = -1;

        private readonly List<Period> _periods = new List<Period>();

        private long _batchId;
        private DateTime _endDateTime;

        public BatchProcess(IPeriodService service,
            IIdentityService identityService,
            ITrackService trackService,
            IWasteStatisticService wasteStatisticService,
            IBatchLogService batchLogService)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (identityService == null)
            {
                throw new ArgumentNullException("identityService");
            }
            if (trackService == null) throw new ArgumentNullException("trackService");
            if (wasteStatisticService == null)
            {
                throw new ArgumentNullException("wasteStatisticService");
            }
            if (batchLogService == null)
            {
                throw new ArgumentNullException("batchLogService");
            }
            _service = service;
            _identityService = identityService;
            _trackService = trackService;
            _wasteStatisticService = wasteStatisticService;
            _batchLogService = batchLogService;
        }

        public bool InProgress()
        {
            return _currentUserIndex < _userArray.Length - 1;
        }

        public void Start(DateTime startDateTime)
        {
            _startDateTime = _batchLogService.GetLastRun(BatchName) ?? startDateTime;
            _endDateTime = DateTime.Now;

            this._batchId = _batchLogService.StartBatch(BatchName);
            //TODO: Get Data
            _userArray = _identityService.GetAll();
            //Process Data

            SetupPeriods();
        }

        //TODO: Create Service for Period plus move services to Business layer

        public void NextUser()
        {
            DateTime forDate = _startDateTime;
            _currentUserIndex++;
            var user = _userArray[_currentUserIndex];
            
            //Trzeba ściągnąć wszystkie rekordy i działać!
            var allRecords = _trackService.GetByDate(_startDateTime, DateTime.Now, user.Id);
            
            PopulatePeriodList(forDate);

            foreach (var period in _periods)
            {
                CreateStatisticsForPeriod(period, allRecords, user);
            }
        }

        private void CreateStatisticsForPeriod(Period period, TrackRecord[] allRecords, Identity user)
        {
            Trace.WriteLine(string.Format("Creating stats for period from {0} to {1} for user {2}", period.StartDate, period.EndDate, user.Login));

//I tu ważna sprawa, jak nasze daty od do!
            var fromDate = period.StartDate < _startDateTime ? _startDateTime : period.StartDate;
            var toDate = period.EndDate > _endDateTime ? _endDateTime : period.EndDate;


            Trace.WriteLine(string.Format("Real range is {0}-{1}", fromDate.ToShortDateString(), toDate.ToShortDateString()));

            var totalTime = StatsCalculator.CalculateStats(allRecords, fromDate, toDate);

            var statsToUpdate = _wasteStatisticService.GetStats(user.Id, period.Id);
            if (statsToUpdate == null)
            {

                Trace.WriteLine(string.Format("Creating new stats"));
                _wasteStatisticService.CreateStatistics((int) totalTime,
                    allRecords.Count(
                        record => record.StartDate >= period.StartDate && record.StartDate < period.EndDate),
                    user.Id, period.Id);
            }
            else
            {
                Trace.WriteLine(string.Format("Updating stats"));
                statsToUpdate.TimeSumInSecond = (int) totalTime;
                statsToUpdate.EntryCount = allRecords.Count(
                    record => record.StartDate >= period.StartDate && record.StartDate < period.EndDate);
                _wasteStatisticService.UpdateStats(statsToUpdate);
            }
        }

        private void PopulatePeriodList(DateTime forDate)
        {
            forDate = forDate.Date;
            while (forDate < DateTime.Now)
            {
                var todayPeriod = this._service.GetDayPeriod(forDate);
                if(todayPeriod == null)
                    continue;
                
                _periods.Add(todayPeriod);

                var weekPeriod = this._service.GetWeekPeriod(forDate);
                if (!_periods.Any(period => period.Type == PeriodType.Week && period.StartDate == weekPeriod.StartDate))
                {
                    _periods.Add(weekPeriod);
                }

                    var monthPeriod = this._service.GetMonthPeriod(forDate);
                    if (!_periods.Any(period => period.Type == PeriodType.Month && period.StartDate == monthPeriod.StartDate))
                {
                    _periods.Add(monthPeriod);
                }

                forDate = forDate.AddDays(1);
            }
        }

        public void Finish()
        {
            _batchLogService.StopBatch(_batchId);

            _context.SaveChanges();
            _context.Dispose();
        }

        private void SetupPeriods()
        {
            DateTime forDate = _startDateTime.Date;

            while (forDate <= DateTime.Today)
            {
                if (_service.GetDayPeriod(forDate) == null)
                {
                    _service.CreateDayPeriod(forDate);
                }

                DateTime weekStart = DateTimeHelper.GetWeekStart(forDate);
                if (_service.GetWeekPeriod(weekStart) == null)
                {
                    _service.CreateWeekPeriod(forDate);
                }

                if (_service.GetMonthPeriod(forDate) == null)
                {
                    _service.CreateMonthPeriod(forDate);
                }

                forDate = forDate.AddDays(1);

            }
            _context.SaveChanges();
        }
    }

    //TODO: Think is it can be static

    public class WasteStats
    {
        public PeriodType Type { get; set; }
        public double TimeSum { get; set; }
    }


}