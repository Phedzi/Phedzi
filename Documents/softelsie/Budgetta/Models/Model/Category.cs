using System;
using System.Collections.Generic;

namespace Models.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasTarget { get; set; } // some Cartegories maight not be having target
        public float TotalAmount { get; set; }
        public float TotalTarget { get; set; }
        public float TotalLoss { get; set; }
        public Budget Budget { get; set; }
    }
}
