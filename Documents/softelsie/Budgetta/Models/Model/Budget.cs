using System;
using System.Collections.Generic;

namespace Models.Model
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public bool IsMonthly { get; set; } // this means it reserts every mounths
        public float SaveTarget { get; set; }
        public float SaveCurrent { get; set; }
        public float Loss { get; set; }
        public List<Category> Categories { get; set; }
    }
}
