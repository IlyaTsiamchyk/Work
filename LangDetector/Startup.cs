using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LangDetector.Startup))]
namespace LangDetector
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
