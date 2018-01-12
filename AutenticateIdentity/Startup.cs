using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutenticateIdentity.Startup))]
namespace AutenticateIdentity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
