using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using StudentRegistration.Helpers.Interfaces;
using StudentRegistration.Application;
using StudentRegistration.Infrastructure;
using StudentRegistration.API.Helpers;
using StudentRegistration.API.Middlewares;

namespace StudentRegistration.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						//ValidateLifetime = true,
						//ValidIssuer = builder.Configuration["Jwt:Issuer"],
						//ValidAudience = builder.Configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
							.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
					};
				});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Description = "Standarrd Authorization header using the Bearer scheme (\"bearer {token}\")",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});

				options.OperationFilter<SecurityRequirementsOperationFilter>();
			});

			//builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentConnectionDB")));
			builder.Services.AddHttpContextAccessor();

			// Add Dependency Injection
			string connectionString = builder.Configuration.GetConnectionString("StudentConnectionDB");
			builder.Services.AddDbContext(connectionString);
			builder.Services.AddInfrastructure();
			builder.Services.AddApplication();
			builder.Services.AddScoped<IAuthHelper, AuthHelper>();
			builder.Services.AddScoped<IImageHelper, ImageHelper>();

			builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			builder.Services.AddCors(o =>
			{
				o.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			var blacklistedTokens = new List<string>(); // Replace with your actual blacklisted tokens collection
			builder.Services.AddSingleton(blacklistedTokens);

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseHttpsRedirection();

			app.UseCors("AllowAll");

			app.UseAuthentication();

			app.UseAuthorization();
			app.UseMiddleware<TokenValidationMiddleware>(app.Services.GetRequiredService<List<string>>());

			app.MapControllers();

			app.Run();
		}
	}
}