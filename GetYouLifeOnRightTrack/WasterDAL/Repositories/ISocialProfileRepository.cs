using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public interface ISocialProfileRepository : IBaseRepositry<SocialProfile, long>
    {
    }

    public class SocialProfileRepository : BaseRepositry<SocialProfile, long>, ISocialProfileRepository
    {
        public SocialProfileRepository(WasteContext context) : base(context)
        {
        }

        protected override long GenerateId()
        {
            //DB generates IDs
            return -1;
        }

    }
}