using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Checker
{
    public class User
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserTypeModel Type { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public List<AgreementModel> Agreements { get; set; }

    }
}
