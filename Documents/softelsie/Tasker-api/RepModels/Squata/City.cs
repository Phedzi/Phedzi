using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public bool HasSites { get; set; }
        public Province Province { get; set; }
        public List<Site> SiteList { get; set; }
    }
}
