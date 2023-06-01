using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Grosvenor.Portal.Business;
using Grosvenor.Portal.Data;
using Grosvenor.Portal.Data.Context;
using Grosvenor.Portal.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Linq;
using Grosvenor.Portal.Model.Config;
using Microsoft.OpenApi.Models;

namespace Grosvenor.Portal.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection(nameof(EmailConfig)));
            builder.Services.AddSingleton<IEmailManager, EmailManager>();
            builder.Services.AddScoped<IBookingManager, BookingManager>();

            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = string.Concat("https://login.microsoftonline.com/", configuration["AzureAd:TenantId"], "/v2.0");
                    options.ClientId = configuration["AzureAd:ClientId"];
                    options.ResponseMode = OpenIdConnectResponseMode.FormPost;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.TokenValidationParameters.ValidAudience = configuration["AzureAd:ClientId"];
                    options.TokenValidationParameters.NameClaimType = MagicStrings.ClaimName.Name;
                    options.TokenValidationParameters.RoleClaimType = MagicStrings.ClaimName.Role;
                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = context =>
                        {
                            context.ProtocolMessage.DomainHint = configuration["AzureAd:DomainHint"];
                            return System.Threading.Tasks.Task.CompletedTask;
                        },
                        OnAuthorizationCodeReceived = context =>
                        {
                            context.Options.Backchannel.DefaultRequestHeaders.TryAddWithoutValidation("Origin", context.Options.ClientId);
                            return System.Threading.Tasks.Task.CompletedTask;
                        },
                        OnTicketReceived = async context =>
                        {
                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            var userGroups = context.Principal.FindAll("groups").Select(x => x).ToList();
                            foreach (var item in userGroups)
                            {
                                identity.RemoveClaim(item);
                            }

                            var hasAdminAccess = userGroups.Any(x => x.Value.Equals(configuration["AzureAd:AdminGroupId"]));
                            if (hasAdminAccess)
                            {
                                identity.AddClaim(new(MagicStrings.ClaimName.Role, MagicStrings.UserRole.Admin));
                            }

                            await context.HttpContext.RequestServices.GetService<IUserManager>()
                            .GetOrCreateAsync(new User
                            {
                                Id = context.Principal.GetUserId(),
                                Name = context.Principal.GetName(),
                                Email = context.Principal.GetEmail()
                            });
                        }
                    };
                   
                });

            builder.Services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
                config.AddPolicy(MagicStrings.AuthPolicy.AdminOnly, new AuthorizationPolicyBuilder().AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme).RequireAuthenticatedUser().RequireRole(MagicStrings.UserRole.Admin).Build());
            });
            builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
            builder.Services.AddDbContext<PortalContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IUserManager, UserManager>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Compnay Flats Api",
                    Description = "Compnay Flats Api"
                });
            });

            var app = builder.Build();
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx => ctx.Context.SetCacheControl()
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("v1/swagger.json","v1");
                    config.DocumentTitle = "Api Documentation";
                    config.RoutePrefix = "swagger";
                });
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers().RequireAuthorization();
            app.MapFallbackToFile("index.html", new StaticFileOptions
            {
                OnPrepareResponse = ctx => ctx.Context.SetCacheControl() 
            }).RequireAuthorization();
            app.Run();
        }
    }
}