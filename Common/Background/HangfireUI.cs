using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;

namespace Background;

public static class HangfireUI
{
    public static void ApplyHangfire(this WebApplication app, string name, string user, string password, string path = "/hangfire")
    {
        app.UseHangfireDashboard(path, new DashboardOptions()
        {
            DashboardTitle = name,
            Authorization = new[] { new HangfireCustomBasicAuthenticationFilter
            {
                User = user,
                Pass = user
            }},
            AppPath = "/swagger"
        });

    }
}