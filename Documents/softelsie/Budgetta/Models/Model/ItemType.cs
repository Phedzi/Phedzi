﻿using System.Collections.Generic;

namespace Models.Model
{
    public class ItemType
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
    }
}
