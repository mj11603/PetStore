using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace PetStoreApp
{
    public class ApiKeyAuthorizationAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthorizationAttribute() : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }

    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["api_key"].ToString();

            if (string.IsNullOrWhiteSpace(apiKey) || apiKey != _configuration["ApiKey"])
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
