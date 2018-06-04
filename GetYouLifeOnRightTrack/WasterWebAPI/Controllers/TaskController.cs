using System;
using System.Linq;
using System.Web.Mvc;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    using WasterWebAPI.Handlers;

    public class TaskController : Controller
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService service)
        {
            _service = service;
        }

        public ActionResult Index(Guid goalId)
        {
            var items = _service.GetLast(goalId, 3).Select(MapTaskToTaskViewItem);

            ViewBag.GoalId = goalId;

            return View(items);
        }

        public ActionResult Create(Guid goalId)
        {
            var model = new CreateTaskViewModel { /*GoalId = goalId*/ };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Guid goalId, CreateTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                Task entity = MapCreateTaskViewModelToEntity(model);

                try
                {
                    _service.Create(goalId, entity);
                }
                catch
                {
                    ModelState.AddModelError("", "Error saving task");
                    return View(model);
                }
                return RedirectToAction("Index", new { goalId });
            }

            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var task = _service.Get(id);

            if (task == null)
            {
                throw new ApplicationException(string.Format("Requested task doesn't exist"));
            }

            DetailTaskViewModel model = MapTaskToDetailTaskViewModel(task);
            return View(model);
        }

        [HttpPost]
        public ActionResult Finish(Guid id, DateTime? endTime)
        {
            _service.Finish(id);

            var task = _service.Get(id);

            return RedirectToAction("Index", new { goalId = task.GoalId });
        }

        public ActionResult Edit(Guid id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
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

        public ActionResult Delete(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
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

        private TaskViewItem MapTaskToTaskViewItem(Task task)
        {
            return new TaskViewItem
                {
                    Id = task.Id,
                    Name = task.Name,
                    Priority = task.Priority,
                    Progress = task.Progress,
                    StartDateTime = task.StartDate,
                    EndDateTime = task.EndDate,
                };
        }

        private DetailTaskViewModel MapTaskToDetailTaskViewModel(Task task)
        {
            return new DetailTaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Priority = task.Priority,
                Progress = task.Progress,
                StartDateTime = task.StartDate,
                EndDateTime = task.EndDate,
            };
        }

        private Task MapCreateTaskViewModelToEntity(CreateTaskViewModel model)
        {
            return new Task
            {
                Name = model.Name,
                Priority = model.Priority,
                Progress = model.Progress,
                StartDate = model.StartDateTime,
                EndDate = model.EndDateTime,
            };
        }
    }
}
