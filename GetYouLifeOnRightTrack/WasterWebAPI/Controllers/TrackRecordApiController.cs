using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Filters;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{

    [HeaderAuthorizationFilter]
    public class TrackRecordApiController : ApiController
    {
        private readonly ITrackService _service;
        private readonly IIdentityService _identityService;
        private readonly IUserContextProvider _userContextProvider;

        public TrackRecordApiController(ITrackService service, IIdentityService identityService, IUserContextProvider userContextProvider)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (identityService == null) throw new ArgumentNullException("identityService");
            if (userContextProvider == null) throw new ArgumentNullException("userContextProvider");
            _service = service;
            _identityService = identityService;
            _userContextProvider = userContextProvider;
        }

        // GET api/trackrecord
        public IEnumerable<TrackRecordViewModel> Get()
        {
            var user = _identityService.Get(_userContextProvider.GetContextUserName());

            return _service.GetAll(user.Id).Select(MapTrackRecordToTrackRecordViewModel);
        }

        [HttpPost]
        [HeaderAuthorizationFilter]
        public Guid StartTrack([FromBody]TrackRecordViewModel model)
        {
            var user = _identityService.Get(_userContextProvider.GetContextUserName());

            model.Id = _service.Create(MapTrackRecordViewModelToTrackRecord(model), user.Id);
            return model.Id;
        }

        [HttpPost]
        public DateTime? StopTrack(Guid id, [FromBody]StopTrackRequest endTime)
        {
            _service.StopTrack(id, endTime.EndTime);

            return DateTime.Now;
        }

        [HttpPost]
        public DateTime? DeleteTrack(Guid id)
        {
            _service.CancelTrack(id);

            return DateTime.Now;
        }

        private static TrackRecord MapTrackRecordViewModelToTrackRecord(TrackRecordViewModel viewModel)
        {
            var pattern = new TrackRecord
            {
                IsWorking = viewModel.IsWorking,
                IsCancelled = viewModel.IsCancelled,
                PatternValue = viewModel.Domain,
                StartDate = viewModel.StartTime,

            };
            return pattern;
        }

        private static TrackRecordViewModel MapTrackRecordToTrackRecordViewModel(TrackRecord entity)
        {
            var pattern = new TrackRecordViewModel
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                IsCancelled = entity.IsCancelled,
                IsWorking = entity.IsWorking,
                Domain = entity.PatternValue,
                StartTime = entity.StartDate,
                EndTime = entity.EndDate
            };
            return pattern;
        }

        public class StopTrackRequest
        {
            public DateTime EndTime { get; set; }
        }
    }
}
