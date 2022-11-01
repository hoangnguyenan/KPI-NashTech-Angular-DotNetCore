using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
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
using MovieTheater.Infrastructure.Data.ApplicationDbContext;
using MovieTheater.Services.Filter;
using MovieTheater.Services.Helper;
using MovieTheater.Services.Interface;
using MovieTheater.Services.Service;
using MovieTheater.Services.Validation;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.WebApi
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
            services.AddDbContext<MovieDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlOption => sqlOption.UseNetTopologySuite()); //integration geometric
            });

            #region Enable Cors 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var frontEndUrl = Configuration.GetValue<string>("frontend_url");
                    builder.WithOrigins(frontEndUrl)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders(new string[] { "totalAmountOfRecords", "totalAmountOfRecordsUpComing" , "totalAmountOfRecordsInCinema" });
                });
            });
            #endregion
            #region Dependency Injection
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMovieTheaterService, MovieTheaterService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRatingService, RatingService>();
            #endregion
            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //automatic pass geometric configuration           
            services.AddSingleton(provider => new MapperConfiguration(config =>
            {
                var geometry = provider.GetRequiredService<GeometryFactory>();
                config.AddProfile(new AutoMapperProfile(geometry));
            }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices
                .Instance.CreateGeometryFactory(srid: 4326));
            #endregion

            services.AddHttpContextAccessor();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddFluentValidation(fv =>
              {
                    fv.RegisterValidatorsFromAssemblyContaining<ActorValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<CategoryValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<MovieTheaterValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<MovieValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<AccountValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<RatingValidator>();
              }).AddJsonOptions(options =>
                {
                 // cai package Install-Package NetTopologySuite.IO.GeoJSON4STJ -Version 2.1.1 ko bi error Converters does not exist
                 //binding GeoJSon data to parameters in .netCore
                 options.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieTheater.WebApi", Version = "v1" });
            });

            #region Security JWT
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.User.AllowedUserNameCharacters = null;
                //options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MovieDbContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes(Configuration["keyjwt"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Admin", policy => policy.RequireClaim("role", "admin"));
                options.AddPolicy("Admin", policy =>
                        policy.RequireAssertion(context =>
                            context.User.HasClaim(c =>
                                (c.Value == "admin" ||
                                 c.Value == "manager"))));
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieTheater.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
