using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eventcore.Models;
using Microsoft.OpenApi.Models;
using Eventcore.Repositories;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Eventcore
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
            string constr = Configuration.GetConnectionString("DefaultCon");
            string key = Configuration["JwtSettings:key"];
            string issuer = Configuration["JwtSettings:issuer"];
            string audience = Configuration["JwtSettings:audience"];
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            SecurityKey securityKey = new SymmetricSecurityKey(keyBytes);

            services.AddCors();
            
            services.AddControllers().AddXmlSerializerFormatters();
            services.AddDbContext<EventContext>(context => context.UseSqlServer(constr));
            services.AddScoped<IRepository<Event>, GenericRepository<Event>>();
            services.AddScoped(typeof(DataSeeder));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Event API",
                    Description = "Allows working with Event information in database",
                    TermsOfService = new Uri("http://www.cognizant.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Gadupudi Krishnapriya",
                        Email = "krishnapriyagadupudi@gmail.com",
                        Url = new Uri("https://github.com/Priyachowdar")
                    }
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    In = ParameterLocation.Header,
                    Description = "Jwt token for authorized user"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {new OpenApiSecurityScheme(){Reference=new OpenApiReference{ Id="Bearer", Type=ReferenceType.SecurityScheme}}, new string[]{ } }
                });
            });
            services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<EventContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = securityKey
                };
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {


                options.AllowAnyOrigin();   //accept request from all client
                options.AllowAnyMethod();  //support all http operations like get,post,put,delete
                options.AllowAnyHeader();  //support all http headers like content-type,accept,etc

                //options.WithOrigins("http://www.cognizant.com");
                //options.WithMethods("GET", "PUT");
                //options.WithHeaders("Authorization", "Content-Type");
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "Event API"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
