using System.Web.Http;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Handlers;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    public class AccountsApiController : ApiController
    {
        private readonly IIdentityService _service;

        public AccountsApiController(IIdentityService service)
        {
            _service = service;
        }

        [HttpGet]
        public void Register()
        {

            //TODO: Register logic
        }

        [HttpPost]
        public void Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var identity = MapRegisterModelToIdentity(model);

                _service.Register(identity);

            }

            //TODO: Register logic
        }

        private Identity MapRegisterModelToIdentity(RegisterModel model)
        {
            return new Identity
            {
                Email = model.Email,
                Login = model.Login.Trim().ToLower(),
                Password = model.Password
            };
        }

        public bool Login(LoginModel model)
        {
            return _service.IsValid(model.UserName, model.Password);
        }
    }
}
