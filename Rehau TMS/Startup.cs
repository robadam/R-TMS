using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rehau_TMS.Startup))]
namespace Rehau_TMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
