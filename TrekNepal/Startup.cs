using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrekNepal.Startup))]
namespace TrekNepal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
