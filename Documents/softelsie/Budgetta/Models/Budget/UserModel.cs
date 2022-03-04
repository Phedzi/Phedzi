using Microsoft.AspNetCore.Identity;

namespace Models.Budget
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public UserTypeModel Type { get; set; }
        public string ImageUrl { get; set; }
    }
}
