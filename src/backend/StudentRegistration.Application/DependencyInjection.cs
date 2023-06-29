using Microsoft.Extensions.DependencyInjection;
using StudentRegistration.Application.Interfaces;
using StudentRegistration.Application.Services;

namespace StudentRegistration.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection Services)
		{
			Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			Services.AddScoped<IUserService, UserService>();
			Services.AddScoped<IStudentService, StudentService>();
			Services.AddScoped<IProjectService, ProjectService>();
			return Services;
		}
	}
}