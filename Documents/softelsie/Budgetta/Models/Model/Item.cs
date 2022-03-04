using System;
using System.Collections.Generic;

namespace Models.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public bool PaymentMaid { get; set; }
        public float PaidAmount { get; set; }
        public List<Category> Categories { get; set; }
        public ItemType ItemType { get; set; }
    }
}
