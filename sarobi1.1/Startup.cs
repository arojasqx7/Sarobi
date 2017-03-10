using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sarobi1._1.Startup))]
namespace sarobi1._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
