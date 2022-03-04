using System;
using System.Collections.Generic;
using Models.Common;

namespace Models.Budget
{
    public class BudgetModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserModel> Users { get; set; }
        public bool IsMonthly { get; set; } // this means it reserts every mounths
        public float SaveTarget { get; set; }
        public float SaveCurrent { get; set; }
        public float Loss { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}
