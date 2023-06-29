using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace StudentRegistration.API.Middlewares
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class TokenValidationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IReadOnlyList<string> _blacklistedTokens;

		public TokenValidationMiddleware(RequestDelegate next, List<string> blacklistedTokens)
		{
			_next = next;
			_blacklistedTokens = blacklistedTokens.AsReadOnly();
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Get the access token from the request headers
			string accessToken = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

			// Check if the token is blacklisted
			bool isBlacklisted = IsTokenBlacklisted(accessToken);
			if (isBlacklisted)
			{
				// Redirect to an unauthorized page
				context.Response.Redirect("/unauthorized");
				return;
			}

			// Continue processing the request
			await _next(context);
		}

		private bool IsTokenBlacklisted(string? accessToken)
		{
			return _blacklistedTokens.Contains(accessToken);
		}
	}

}
