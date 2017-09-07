using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Form11.Startup))]
namespace Form11
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
