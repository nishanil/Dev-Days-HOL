using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyEvents.Server.Startup))]

namespace MyEvents.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}