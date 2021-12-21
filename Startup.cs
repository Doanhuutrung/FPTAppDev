using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTAppDev.Startup))]
namespace FPTAppDev
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
