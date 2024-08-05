using AutoMapper;
using DataAccess.Model.DAO;
using DataAccess.Model.DBContext;
using Microsoft.EntityFrameworkCore;
using WEB_CLIENT.Providers;
using WEB_CLIENT.Services.Admin;
using WEB_CLIENT.Services.ChangePassword;
using WEB_CLIENT.Services.Courses;
using WEB_CLIENT.Services.ForgotPassword;
using WEB_CLIENT.Services.Home;
using WEB_CLIENT.Services.Login;
using WEB_CLIENT.Services.ManagerCourse;
using WEB_CLIENT.Services.ManagerLesson;
using WEB_CLIENT.Services.ManagerPDF;
using WEB_CLIENT.Services.ManagerQuiz;
using WEB_CLIENT.Services.ManagerVideo;
using WEB_CLIENT.Services.MyCourse;
using WEB_CLIENT.Services.Profile;
using WEB_CLIENT.Services.Register;
using WEB_CLIENT.Services.TakeQuiz;

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
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            // -------------------------register dbcontext----------------------------
            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<MyDbContext>(/*options =>
                options.UseSqlServer(connection)*/
            );
            // -------------------------register service ----------------------------
            builder.Services.AddSingleton<DAOUser>();
            builder.Services.AddSingleton<DAOCourse>();
            builder.Services.AddSingleton<DAOLesson>();
            builder.Services.AddSingleton<DAOEnrollCourse>();
            builder.Services.AddSingleton<DAOCategory>();
            builder.Services.AddSingleton<DAOLessonPDF>();
            builder.Services.AddSingleton<DAOLessonVideo>();
            builder.Services.AddSingleton<DAOQuiz>();
            builder.Services.AddSingleton<DAOResult>();
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
