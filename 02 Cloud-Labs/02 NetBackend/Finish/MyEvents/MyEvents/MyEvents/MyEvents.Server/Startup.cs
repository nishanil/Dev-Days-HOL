using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(devdayshol_netService.Startup))]

namespace devdayshol_netService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}