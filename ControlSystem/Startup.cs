using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControlSystem.Startup))]
namespace ControlSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
