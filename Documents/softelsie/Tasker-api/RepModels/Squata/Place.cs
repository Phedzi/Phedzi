using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class Place
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Views { get; set; }
        public float Price { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string OverviewImg { get; set; }

        public bool IsAvailable { get; set; }
        public DateTime AvailableFrom { get; set; }
        public RoomType RoomType { get; set; }

        public Site Site { get; set; }
        public User Owner { get; set; }
        public List<Image> ImageList { get; set; }
    }
}
