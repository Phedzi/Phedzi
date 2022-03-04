using System;
using System.Collections.Generic;
using Models.Common;

namespace Models.Budget
{
    public class ItemModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public bool PaymentMaid { get; set; }
        public float PaidAmount { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public ItemTypeModel ItemType { get; set; }
    }
}
