using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamePortal.Startup))]
namespace GamePortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
