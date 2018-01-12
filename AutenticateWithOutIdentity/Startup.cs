using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutenticateWithOutIdentity.Startup))]
namespace AutenticateWithOutIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}