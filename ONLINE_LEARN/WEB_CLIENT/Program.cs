using WEB_CLIENT.Model;
using WEB_CLIENT.Services.IService;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // insert data
            await InsertData.Insert();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(options =>
            {
                // set time for session
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<ICoursesService, CoursesService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{key?}");

            app.Run();

        }
    }
}
