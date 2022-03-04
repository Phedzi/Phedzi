using System;
using System.Collections.Generic;
using Models.Common;

namespace Models.Budget
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasTarget { get; set; } // some Cartegories maight not be having target
        public float TotalAmount { get; set; }
        public float TotalTarget { get; set; }
        public float TotalLoss { get; set; }
        public BudgetModel Budget { get; set; }
    }
}
