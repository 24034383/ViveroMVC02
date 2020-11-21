using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViveroMVC02.Startup))]
namespace ViveroMVC02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
