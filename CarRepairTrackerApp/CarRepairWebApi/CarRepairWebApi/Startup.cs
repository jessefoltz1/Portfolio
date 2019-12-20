using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRepairService.DAOs;
using CarRepairWebApi.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace CarRepairWebApi
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
            //Configures Swagger to look at the XmlComments above our methods
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Car Repair API", Version = "v1" });
            });

            // Add CORS policy allowing any origin
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Enables automatic authentication token.
            // The token is expected to be included as a bearer authentication token.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // The rules for token validation
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,                 // issuer not required
                        ValidateAudience = false,               // audience not required
                        ValidateLifetime = true,                // token must not have expired
                        ValidateIssuerSigningKey = true,        // token signature must match so as not to be tampered with
                        NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,   // allows us to use sub for username
                        RoleClaimType = "rol",                  // allows us to put the role in rol
                        IssuerSigningKey = new SymmetricSecurityKey(    // each token is signed with a private key so as to ensure its validity
                        Encoding.UTF8.GetBytes(Configuration["JwtSecret"]))
                    };
                });

            string connectionString = Configuration.GetConnectionString("CarRepairConnection");
            services.AddScoped<ICarRepairDAO, CarRepairDAO>(m => new CarRepairDAO(connectionString));
            services.AddScoped<ITokenGenerator, JwtGenerator>(m => new JwtGenerator(Configuration["JwtSecret"]));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enables the middleware to check the incoming request headers.
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRepair API v1");
            });

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
