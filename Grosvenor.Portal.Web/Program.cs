using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Grosvenor.Portal.Business;
using Grosvenor.Portal.Data;
using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using System.Security.Claims;

namespace Grosvenor.Portal.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(configuration);
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            builder.Services.Configure<MicrosoftIdentityOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.UsePkce = true;
                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = async context => await context.HttpContext.
                        RequestServices.GetService<IUserAccountManager>()
                        .GetOrCreateAsync(new UserAccount
                        {
                            UserAccountId = System.Guid.Parse(context.Principal.FindFirstValue("oid")),
                            Name = context.Principal.FindFirstValue("name"),
                            Email = context.Principal.FindFirstValue("email")
                        })
                };
            });
            builder.Services.AddAuthorization(config => config.DefaultPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build());
            builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            builder.Services.AddDbContext<PortalContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            builder.Services.AddScoped<IUserAccountManager, UserAccountManager>();

            var app = builder.Build();
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapGet("/", () => "Grosvenor Portal");
            app.Run();
        }
    }
}