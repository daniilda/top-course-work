using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .AddHttpContextAccessor();

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
            
            //app.UseHttpsRedirection(); //TODO: delete when get cert.

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}