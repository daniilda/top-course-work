using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopCourseWorkBl.DataLayer;
using TopCourseWorkBl.DataLayer.Extensions;

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
                .AddSwaggerGen()
                .AddMediatR(type)
                .AddAutoMapper(type)
                .AddHttpContextAccessor()
                .AddSingleton<HttpCancellationTokenAccessor>()
                .AddDatabaseInfrastructure(_configuration);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseSwagger()
                .UseSwaggerUI(opt
                    => opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TOPCourseworkBL"));

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.Migrate();
        }
    }
}