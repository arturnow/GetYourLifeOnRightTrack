using System.Threading.Tasks;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface IIdentityService
    {
        int Register(Identity identity);

        //Task<int> RegisterAsync(Identity identity);

        void Update(Identity entity);

        Identity Get(int id);
        Identity Get(string login);

        Identity[] GetAll();

        void Delete(int id);

        bool IsValid(string userName, string password);
    }
}
