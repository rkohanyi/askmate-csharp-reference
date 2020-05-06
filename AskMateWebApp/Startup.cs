using AskMateWebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Npgsql.Logging;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace AskMateWebApp
{
    public sealed class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private static void RemoveReturnUrlFromRedirectUri(RedirectContext<CookieAuthenticationOptions> context)
        {
            var ub = new UriBuilder(context.RedirectUri);
            var query = QueryHelpers.ParseQuery(ub.Query);
            ub.Query = null;
            query.Remove("ReturnUrl");
            context.RedirectUri = ub.Uri.ToString();
            foreach (var key in query.Keys)
            {
                context.RedirectUri = QueryHelpers.AddQueryString(context.RedirectUri, key, query[key]);
            }
        }

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            RemoveReturnUrlFromRedirectUri(context);
            return base.RedirectToAccessDenied(context);
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            RemoveReturnUrlFromRedirectUri(context);
            return base.RedirectToLogin(context);
        }
    }

    public class Startup
    {
        private readonly string uploadsDirectory;
        private readonly string connectionString;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.IsDevelopment())
            {
                NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Debug, true, true);
                NpgsqlLogManager.IsParameterLoggingEnabled = true;
            }
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.EventsType = typeof(CustomCookieAuthenticationEvents));
            services.AddScoped<CustomCookieAuthenticationEvents>();
            services.AddScoped<IDbConnection>(_ =>
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            });
            services.AddScoped<IDatabaseService, PostgreSqlDatabaseService>();
            services.AddScoped<IUsersService, SqlUsersService>();
            services.AddScoped<IQuestionsService, SqlQuestionsService>();
            services.AddScoped<IQuestionsTagsService, SqlQuestionsTagsService>();
            services.AddScoped<ITagsService, SqlTagsService>();
            services.AddScoped<IAnswersService, SqlAnswersService>();
            services.AddScoped<ICommentsService, SqlCommentsService>();
            services.AddScoped<ISearchService, SqlSearchService>(x => new SqlSearchService(x.GetRequiredService<IDbConnection>(), "<mark>", "</mark>"));
            services.AddSingleton(typeof(IStorageService), new FileStorageService(uploadsDirectory));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDatabaseService databaseService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                if (!databaseService.Initialized)
                {
                    databaseService.Reset();
                }
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Questions}/{action=Index}/{id?}");
            });
        }
    }
}
