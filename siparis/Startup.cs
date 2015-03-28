using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(siparis.Startup))]
namespace siparis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
