using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tutor.Web.Startup))]
namespace Tutor.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
