using Models.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logic.Common
{
    public class Tokens
    {
        public static async Task<AuthResponse> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,string number)
        {
            var response = new AuthResponse()
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Token = await jwtFactory.GenerateEncodedToken(userName, identity),
                Name = userName,
                Type = number,
                Message = "success"
            };

            return response;
        }
    }
}
