using Models.Common;
using System;
using System.Collections.Generic;

namespace Models.Checker
{
    public class UserTypeModel : BaseModel
    {
        public string name { get; set; }
        public string Discription { get; set; }
        public string DefaultUrl { get; set; }
        public int Weight { get; set; }
        public List<UserModel> Users { get; set; }
        public List<MenuModel> Menues { get; set; }
    }
}
