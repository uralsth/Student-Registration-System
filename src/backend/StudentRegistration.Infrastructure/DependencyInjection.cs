using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StudentRegistration.Application.Repositories;
using StudentRegistration.Infrastructure.Repositories;

namespace StudentRegistration.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{

			services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
