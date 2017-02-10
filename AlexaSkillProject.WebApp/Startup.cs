using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AlexaSkillProject.WebApp.Startup))]
namespace AlexaSkillProject.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
