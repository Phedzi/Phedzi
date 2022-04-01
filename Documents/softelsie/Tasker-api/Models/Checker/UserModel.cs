using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models.Checker
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public UserTypeModel Type { get; set; }
        public string ImageUrl { get; set; }
        public List<CheckingModel> CotractedList { get; set; }
    }
}
