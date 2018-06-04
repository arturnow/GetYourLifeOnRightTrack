namespace WasterDAL.Repositories
{
    using System.Linq;

    using WasterDAL.Model;

    public class WasteStatisticRepository : BaseRepositry<WasteStatistic,long>, IWasteStatisticRepository
    {
        public WasteStatisticRepository(WasteContext context)
            : base(context)
        {
        }

        protected override long GenerateId()
        {
            //TODO: It seems like DB generate ID
            var last = this.Query(statistic => true).OrderByDescending(statistic => statistic.Id).FirstOrDefault();
            return last != null ? last.Id : 1;
        }
    }
}