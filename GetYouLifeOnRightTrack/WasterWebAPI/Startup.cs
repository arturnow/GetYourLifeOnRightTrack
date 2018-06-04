using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WasterWebAPI.Startup))]
namespace WasterWebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
