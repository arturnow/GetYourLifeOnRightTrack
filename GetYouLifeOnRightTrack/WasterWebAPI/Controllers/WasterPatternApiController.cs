using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WasterLOB;
using WasterWebAPI.Filters;
using WasterWebAPI.Handlers;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    [HeaderAuthorizationFilter]
    public class WasterPatternApiController : ApiController
    {
        private readonly IPatternService _patternService;

        
        public WasterPatternApiController(IPatternService patternService)
        {
            _patternService = patternService;
        }

        // GET api/wasterpattern
        public IEnumerable<PatternModel> Get()
        {
            return _patternService.GetPatterns().Select(PatternMapper.MapPatternToPatternViewModel).ToList();
        }

        // GET api/wasterpattern/5
        public PatternModel Get(Guid id)
        {
            var existingRecord = _patternService.Get(id);
            return PatternMapper.MapPatternToPatternViewModel(existingRecord);
        }

        public Guid Add([FromBody]PatternModel viewModel)
        {
            var pattern = PatternMapper.MapPatternViewModelToPattern(viewModel);
            viewModel.Id = _patternService.Create(pattern);

            return viewModel.Id;
        }

        public void Disable([FromBody]PatternModel record)
        {
            _patternService.Disable(record.Pattern);
        }

       

    }
}
