using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutenticationWithOutIdentity.Startup))]
namespace AutenticationWithOutIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
