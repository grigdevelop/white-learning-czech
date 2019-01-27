using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace PersonalWebsite.Web.Helpers
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiAuthorizeAttribute()
        {
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
