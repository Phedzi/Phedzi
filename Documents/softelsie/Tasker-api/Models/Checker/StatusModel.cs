using Models.Common;

namespace Models.Checker
{
    public class StatusModel : BaseModel
    {
        public string name { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string css { get; set; }

        public StatusModel ()
        {
            icon = "mdi mdi-flickr";
            css = "not-active";
        }
    }
}
