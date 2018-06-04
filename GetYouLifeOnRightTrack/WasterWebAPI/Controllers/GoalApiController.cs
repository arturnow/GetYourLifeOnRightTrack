using System;
using System.Web.Http;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Handlers;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    //I have added API controller, but I might change it or add normal controller for WWW
    public class GoalApiController : ApiController
    {
        private readonly IGoalService _service;

        public GoalApiController(IGoalService service)
        {
            _service = service;
        }


        //Create Goal
        public Guid CreateGola(GoalViewModel model)
        {
            if (ModelState.IsValid)
            {
               Goal entity = MapViewModelToModel(model);

                return _service.Create(entity);
            }
            throw new ApplicationException("Błąd");
        }

        //Update Goal

        public Guid UpdateGola(GoalViewModel model)
        {
            if (ModelState.IsValid)
            {
                Goal entity = MapViewModelToModel(model);

                _service.Update(entity);
            }
            return model.Id;
        }

        //Finish Goal
        private Goal MapViewModelToModel(GoalViewModel model)
        {
            return new Goal
            {
                Id = model.Id,
                Category = model.Category,
                Description = model.Descirption,
                DueDateTime = model.DueDateTime,
                StartDate = model.StartTime,
                Progress = model.Progress,
                Name = model.Name,
                Duration = model.Duration
            };
        }
    }
}