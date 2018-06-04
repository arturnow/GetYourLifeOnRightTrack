using WasterDAL.Model;
using WasterWebAPI.Models;

namespace WasterWebAPI.Mappers
{
    public static class PatternMapper
    {
        public static Pattern MapPatternViewModelToPattern(PatternModel viewModel)
        {
            var pattern = new Pattern
            {
                IsEnabled = viewModel.IsEnabled,
                IsWorking = viewModel.IsEnabled,
                Value = viewModel.Pattern
            };
            return pattern;
        }

        public static PatternModel MapPatternToPatternViewModel(Pattern entity)
        {
            var pattern = new PatternModel
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                UpdateDate = entity.UpdateDate,
                IsEnabled = entity.IsEnabled,
                IsWorking = entity.IsEnabled,
                Pattern = entity.Value
            };
            return pattern;
        }
    }
}