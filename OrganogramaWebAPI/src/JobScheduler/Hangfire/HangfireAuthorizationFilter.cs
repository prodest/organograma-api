using Hangfire.Dashboard;
using System.Linq;

namespace JobScheduler.Hangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            bool isAuthenticated = false;

            if (context != null && context.GetHttpContext() != null && context.GetHttpContext().User != null)
            {
                var user = context.GetHttpContext().User;

                if (user.Identity.IsAuthenticated)
                {
                    var perfis = user.Claims.Where(c => c.Type.Equals("role"))
                                            .Select(c => c.Value)
                                            .ToList();

                    if (perfis != null && perfis.Contains("Administrador"))
                        isAuthenticated = true;
                }
            }

            return isAuthenticated;
        }
    }
}
