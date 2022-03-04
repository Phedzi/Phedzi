using Models.Common;
using System.Collections.Generic;

namespace Models.Budget
{
    public class UserTypeModel : BaseModel
    {
        public string Name { get; set; }
        public string Descriprion { get; set; }
        public string DefaultUrl { get; set; }
        public int Weight { get; set; }
        public List<UserModel> Users { get; set; }
    }
}
