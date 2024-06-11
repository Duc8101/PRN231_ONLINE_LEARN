using DataAccess.Model.DAO;
using DataAccess.Model.DBContext;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using WEB_CLIENT.Providers;
using WEB_CLIENT.Services.IService;
using WEB_CLIENT.Services.Service;

namespace WEB_CLIENT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            // -------------------------register dbcontext----------------------------
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(connection)
            );
            // -------------------------register service ----------------------------
            builder.Services.AddTransient(typeof(ICommonDAO<>), typeof(CommonDAO<>));
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IChangePasswordService, ChangePasswordService>();
            builder.Services.AddScoped<ICoursesService, CoursesService>();
            builder.Services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IManagerCourseService, ManagerCourseService>();
            builder.Services.AddScoped<IManagerLessonService, ManagerLessonService>();
            builder.Services.AddScoped<IManagerPDFService, ManagerPDFService>();
            builder.Services.AddScoped<IManagerVideoService, ManagerVideoService>();
            builder.Services.AddScoped<IManagerQuizService, ManagerQuizService>();
            builder.Services.AddScoped<IMyCourseService, MyCourseService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddScoped<ITakeQuizService, TakeQuizService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var app = builder.Build();
            StaticServiceProvider.Provider = app.Services;
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
