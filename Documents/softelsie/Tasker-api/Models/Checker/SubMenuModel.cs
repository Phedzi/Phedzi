using Models.Common;
/*
    SubMent can be: Loan reports, Checking Reports (Reports types), User types
 */
namespace Models.Checker
{
    public class SubMenuModel : BaseModel
    {
        public string Caption { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public MenuModel Menu { get; set; }
    }
}
