using Microsoft.Owin;
using Owin;
using Peek.Web;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace Peek.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
