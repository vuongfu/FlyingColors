using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TutorOnline.Web.Startup))]
namespace TutorOnline.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
