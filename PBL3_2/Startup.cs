using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PBL3_2.Startup))]
namespace PBL3_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
