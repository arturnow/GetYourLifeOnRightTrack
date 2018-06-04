using System;
using System.Linq;
using System.Web.Mvc;
using WasterLOB;
using WasterWebAPI.Handlers;

namespace WasterWebAPI.Controllers
{
    public class PatternController : Controller
    {
        private readonly IPatternService _patternService;
        
        public PatternController(IPatternService patternService)
        {
            _patternService = patternService;
        }

        //
        // GET: /Pattern/

        public ActionResult Index()
        {
            var patterns = _patternService.GetPatterns().Select(PatternMapper.MapPatternToPatternViewModel).ToArray();
            return View(patterns);
        }

        //
        // GET: /Pattern/Details/5

        public ActionResult Details(Guid id)
        {
            var pattern = PatternMapper.MapPatternToPatternViewModel(_patternService.Get(id));
            return View(pattern);
        }

        //
        // GET: /Pattern/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pattern/Create

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
        // GET: /Pattern/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Pattern/Edit/5

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
        // GET: /Pattern/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Pattern/Delete/5

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
