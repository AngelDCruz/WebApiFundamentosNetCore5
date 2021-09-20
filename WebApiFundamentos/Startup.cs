using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using WebApiFundamentos.Filtros;
using WebApiFundamentos.Middlewares;
using WebApiFundamentos.Models;
using WebApiFundamentos.Seguridad;
using WebApiFundamentos.Servicios;

namespace WebApiFundamentos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddFiltros();

            services.AddControllers(
                options => {
                    options.Filters.Add(typeof(FiltroExcepcion));
                    options.Conventions.Add(new SwaggerVersionamiento());
                }    
            ).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")
             ));

            services.AddServices();

            services.AddMemoryCache();

            TokenValidationParameters tokenValidation = new TokenValidationParameters()
            {
                ValidateIssuer=false,
                ValidateAudience=false,
                ValidateLifetime=true,
                ValidateIssuerSigningKey=true,
                IssuerSigningKey=new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["Token:key"])
                ),
                ClockSkew=TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones => opciones.TokenValidationParameters = tokenValidation);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Version 1", 
                    Version = "v1",
                    Description = "Web api primera versión",
                    Contact = new OpenApiContact
                    {
                        Name = "Angel Ortiz",
                        Email = "bufactor21.22@gmail.com",
                        Url = new Uri("https://www.facebook.com/profile.php?id=100069430039684")
                    }
                });
                
                c.SwaggerDoc("v2", new OpenApiInfo {
                    Title = "Version 1", 
                    Version = "v2",
                    Description = "Web api segunda versión",
                    Contact = new OpenApiContact
                    {
                        Name = "Angel Ortiz",
                        Email = "bufactor21.22@gmail.com",
                        Url = new Uri("https://www.facebook.com/profile.php?id=100069430039684")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    //Type = SecuritySchemeType.ApiKey,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new string[] { }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthorization(opciones =>
            {

                foreach (KeyValuePair <string, Claim> diccionario in ClaimsSistema.Claims()){

                     opciones.AddPolicy(diccionario.Key, politica => politica.RequireClaim(diccionario.Value.Type));
                    
                 }

            });

            services.AddCors(opciones => {
                opciones.AddDefaultPolicy(config => {
                    config.WithOrigins(Configuration["Cors:sitiosPermitidos"]).AllowAnyHeader().AllowAnyMethod();
                });
            });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UserLogResponse();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "versión 1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "versión 2");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseResponseCaching();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
