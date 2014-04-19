using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShortUrl.Startup))]
namespace ShortUrl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
