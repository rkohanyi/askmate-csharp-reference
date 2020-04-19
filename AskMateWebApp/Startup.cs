using AskMateWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace AskMateWebApp
{
    public class Startup
    {
        private readonly string uploadsDirectory;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("UPLOADS_DIRECTORY")))
            {
                uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            }
            else
            {
                uploadsDirectory = Path.Combine(Environment.GetEnvironmentVariable("UPLOADS_DIRECTORY"), "uploads");
            }
            Directory.CreateDirectory(uploadsDirectory);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton(typeof(IQuestionsService), new CsvQuestionsService("questions.csv", uploadsDirectory));
            services.AddSingleton(typeof(IAnswersService), new CsvAnswersService("answers.csv"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(uploadsDirectory),
                RequestPath = "/uploads"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Questions}/{action=List}/{id?}");
            });
        }
    }
}
