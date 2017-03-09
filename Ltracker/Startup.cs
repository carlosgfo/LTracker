using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ltracker.Startup))]
namespace Ltracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
