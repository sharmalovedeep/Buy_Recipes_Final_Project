using System;
using Buy_Recipes_Final_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Buy_Recipes_Final_Project.Areas.Identity.IdentityHostingStartup))]
namespace Buy_Recipes_Final_Project.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Buy_Recipes_IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Buy_Recipes_IdentityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                     .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<Buy_Recipes_IdentityContext>();
            });
        }
    }
}