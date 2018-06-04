using System.Linq;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class IdentityRepository : BaseRepositry<Identity, int>, IIdentityRepository
    {
        public IdentityRepository(WasteContext context)
            : base(context)
        {
            
        }

        protected override int GenerateId()
        {
            var entity = this.Query(i => true).OrderBy(i => i.Id).FirstOrDefault();

            return entity != null ? entity.Id + 1 : 1;
        }
    }
}