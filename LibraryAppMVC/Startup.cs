using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DatabaseConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace LibraryAppMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* JWT Resources
             * https://auth0.com/blog/securing-asp-dot-net-core-2-applications-with-jwts/
             * https://blogs.msdn.microsoft.com/webdev/2017/04/06/jwt-validation-and-authorization-in-asp-net-core/
             * https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/identity-2x
             * This part is how the token authentication works
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Audience = "http://localhost:5001/";
                    opt.Authority = "http://localhost:5000/";
                    opt.Configuration = new OpenIdConnectConfiguration();
                    // Stackexchange https://stackoverflow.com/questions/37693516/unable-to-obtain-configuration-from-well-known-openid-configuration/37973711
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero  // Lets tokens expire when they say they will
                    };
                });

            services.AddSingleton(Configuration);

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("TheDatabase")));
            services.AddResponseCaching();
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "dd/MM/yyyy";
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "FBLA Mobile App", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var redirOpt = new RewriteOptions();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // HTTPS redirector
                redirOpt.AddRedirectToHttps();
            }

            app.UseRewriter(redirOpt);
            app.UseAuthentication();
            //app.UseResponseCaching();

            // Serve book images
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images")),
                    RequestPath = "/images",
                    EnableDirectoryBrowsing = true
            });

            // Use Swagger, add json endpoint
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FBLA Mobile App v1");
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
