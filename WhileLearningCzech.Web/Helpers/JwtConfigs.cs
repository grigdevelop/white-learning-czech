using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WhileLearningCzech.Web.Helpers
{
    public static class JwtConfigs
    {
        public const string SecurityKey = "pldksecurekey123";

        public static SecurityKey GetSecurityKey()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfigs.SecurityKey));
            return key;
        }
    }
}
