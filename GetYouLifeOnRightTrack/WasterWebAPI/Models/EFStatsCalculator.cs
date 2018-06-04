using System.Data.Entity;
using WasterDAL.Model;

namespace WasterWebAPI.Models
{
    public class EFStatsCalculator
    {
        private readonly DbContext _context;

        public EFStatsCalculator(DbContext context)
        {
            _context = context;
        }

        public void CalculateMonth(TrackRecord trackRecord)
        {

        }

        public void CalclateWeek()
        {
            
        }

        public void CalculateDay()
        {
            
        }
    }
}