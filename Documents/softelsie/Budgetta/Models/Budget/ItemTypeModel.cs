using System.Collections.Generic;
using Models.Common;

namespace Models.Budget
{
    public class ItemTypeModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
