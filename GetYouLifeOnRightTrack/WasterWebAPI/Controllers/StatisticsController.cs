using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WasterLOB;

namespace WasterWebAPI.Controllers
{
    using WasterDAL.Model;

    public class StatisticsController : Controller
    {
        private readonly IWasteStatisticService _wasteStatisticService;

        private readonly IIdentityService _identityService;

        private readonly IUserContextProvider _userContextProvider;

        public StatisticsController(IWasteStatisticService wasteStatisticService, IIdentityService identityService, IUserContextProvider userContextProvider)
        {
            if (wasteStatisticService == null)
            {
                throw new ArgumentNullException("wasteStatisticService");
            }
            if (identityService == null)
            {
                throw new ArgumentNullException("identityService");
            }
            if (userContextProvider == null)
            {
                throw new ArgumentNullException("userContextProvider");
            }
            this._wasteStatisticService = wasteStatisticService;
            this._identityService = identityService;
            this._userContextProvider = userContextProvider;
        }

        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            var userName = _userContextProvider.GetContextUserName();
            var user = _identityService.Get(userName);
            IEnumerable<WasteStatistic> stats = _wasteStatisticService.GetCurrentDailyMonthStatsWithPeriodInfo(user.Id);

            ViewBag.WeekSummary = _wasteStatisticService.GetCurrentWeekSummaryWithPeriodInfo(user.Id);
            ViewBag.MonthSummary = _wasteStatisticService.GetCurrentMonthSummaryWithPeriodInfo(user.Id);

            return View(stats);
        }

        //
        // GET: /Statistics/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Statistics/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Statistics/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Statistics/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Statistics/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Statistics/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Statistics/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
