using System;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TopCourseWorkBl.AuthenticationLayer;
using TopCourseWorkBl.AuthenticationLayer.Extensions;
using TopCourseWorkBl.BackgroundTasksService;
using TopCourseWorkBl.DataLayer.Extensions;
using TopCourseWorkBl.Extensions;
using static TopCourseWorkBl.EnvironmentConstants;

namespace TopCourseWorkBl
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var type = typeof(Startup);

            services
                .AddSwagger()
                .AddMediatR(type)
                .AddAutoMapper(type)
                .AddHttpContextAccessor()
                .AddSingleton<HttpCancellationTokenAccessor>()
                .AddDatabaseInfrastructure(_configuration);

            services
                .AddHostedService<MainHostedService>()
                .AddSingleton<TasksProvider>()
                .AddSingleton<TaskStatusProcessor>()
                .AddSingleton<BackgroundTaskProcessor>() ;

            services.AddControllers();

            services.AddOptions<AuthOptions>()
                .Configure(opt => _configuration
                    .GetSection(nameof(AuthOptions))
                    .Bind(opt));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _configuration[AuthKey] is null
                            ? "defaultKeyqweqweqweqweqweqweqweqweqweqwe".GetSymmetricSecurityKey()
                            : _configuration[AuthKey].GetSymmetricSecurityKey(),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseSwagger()
                .UseSwaggerUI(opt
                    =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TOPCourseworkBL");
                    opt.RoutePrefix = string.Empty;
                });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.Migrate();
        }
    }
}