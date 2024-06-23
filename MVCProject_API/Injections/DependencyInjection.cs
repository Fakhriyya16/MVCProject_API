using FluentValidation.AspNetCore;
using MVCProject_API.Helpers;
using MVCProject_API.Services;
using MVCProject_API.Services.Interfaces;

namespace MVCProject_API.Injections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IInstructorService, InstructorService>();
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableDataAnnotationsValidation = true;
            });

            services.AddSwaggerGen(c =>
            {
                 c.EnableAnnotations();
            });

            return services;
        }
    }
}
