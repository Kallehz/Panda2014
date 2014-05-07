using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PandaApp.Startup))]
namespace PandaApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
