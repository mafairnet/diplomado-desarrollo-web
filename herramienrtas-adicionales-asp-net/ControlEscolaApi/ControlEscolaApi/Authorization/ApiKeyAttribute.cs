using Microsoft.AspNetCore.Mvc;

namespace ControlEscolaApi.Authorization
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute()
        : base(typeof(ApiKeyAuthorizationFilter))
        {
        }
    }
}
