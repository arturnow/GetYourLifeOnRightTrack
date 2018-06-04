using System;
using System.Linq;
using System.Web.Mvc;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    using WasterWebAPI.Handlers;
    public class GoalController : Controller
    {
        private readonly IGoalService _service;

        public GoalController(IGoalService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            var list = _service.GetAll().Select(MapGoalToGoalListItem);

            return View(list);
        }

        public ActionResult Details(Guid id)
        {
            return View(this.MapGoalToGoalViewModel(_service.Get(id)));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateGoalViewModel());
        }


        [HttpPost]
        public ActionResult Create(CreateGoalViewModel model)
        {
            try
            {
                _service.Create(MapCreateGoalViewModelToEntity(model));

                TempData["message"] = "A coś tam się stało";
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Jakiś bład z obiektem");
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            return View(MapGoalToEditGoalViewModel(_service.Get(id)));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, EditGoalViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _service.Update(MapEditGoalViewModelToGoal(model));
                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Unexpected error");
                return View(model);
            }
        }

        public ActionResult Delete(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                //var entity = _service.Get();

                //_service.Delete();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private GoalListItem MapGoalToGoalListItem(Goal goal)
        {
            return new GoalListItem
                {
                    Category = goal.Category,
                    Id = goal.Id,
                    Name = goal.Name,
                    Descirption = goal.Description,
                    DueDateTime = goal.DueDateTime,
                    Progress = 0,
                };
        }

        private EditGoalViewModel MapGoalToEditGoalViewModel(Goal entity)
        {
            return new EditGoalViewModel
            {
                Name = entity.Name,
                Id = entity.Id,
                Category = entity.Category,
                Progress = entity.Progress,
                Duration = entity.Duration,
                Descirption = entity.Description,
                StartTime = entity.StartDate,
                DueDateTime = entity.DueDateTime
            };
        }

        private GoalViewModel MapGoalToGoalViewModel(Goal entity)
        {
            return new GoalViewModel
            {
                Name = entity.Name,
                Id = entity.Id,
                Category = entity.Category,
                Progress = entity.Progress,
                Duration = entity.Duration,
                Descirption = entity.Description,
                StartTime = entity.StartDate,
                DueDateTime = entity.DueDateTime
            };
        }

        private Goal MapCreateGoalViewModelToEntity(CreateGoalViewModel model)
        {
            return new Goal
            {
                Name = model.Name,
                Category = model.Category,
                Progress = model.Progress,
                Description = model.Descirption,
                DueDateTime = model.DueDateTime.Value,
                Duration = model.Duration,
                StartDate = model.StartTime.Value,
            };
        }

        private Goal MapEditGoalViewModelToGoal(EditGoalViewModel model)
        {
            return new Goal
            {
                Id =  model.Id,
                Name = model.Name,
                Category = model.Category,
                Progress = model.Progress,
                Description = model.Descirption,
                DueDateTime = model.DueDateTime,
                Duration = model.Duration,
                StartDate = model.StartTime,
            };
        }
    }
}
