using Models.Common;
using System.Collections.Generic;

namespace Models.Checker
{
    public class MenuModel : BaseModel
    {
        public string Caption { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public bool HasSubs { get; set; }
        public List<SubMenuModel> Subs { get; set; }
        public UserTypeModel UserType { get; set; }
    }
}
