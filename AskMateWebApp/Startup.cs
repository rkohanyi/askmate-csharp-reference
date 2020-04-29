using AskMateWebApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Data;
using System.IO;

namespace AskMateWebApp
{
    public class Startup
    {
        private readonly string uploadsDirectory;
        private readonly string connectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            uploadsDirectory = InitUploadsDirectory(webHostEnvironment);
            connectionString = InitConnectionString();
        }

        private string InitUploadsDirectory(IWebHostEnvironment webHostEnvironment)
        {
            string uploadsDirectory = Path.Combine(Environment.GetEnvironmentVariable("UPLOADS_DIRECTORY") ?? webHostEnvironment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsDirectory);
            return uploadsDirectory;
        }

        private string InitConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Host=localhost;Username=postgres;Password=admin;Database=test";
            return connectionString;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddTransient<IDbConnection>(_ =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });
            services.AddScoped<IQuestionsService, SqlQuestionsService>();
            services.AddScoped<IAnswersService, SqlAnswersService>();
            services.AddSingleton(typeof(IStorageService), new FileStorageService(uploadsDirectory));
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
