using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public bool HasPlaces { get; set; }
        public City City { get; set; }
        public List<Place> PlaceList { get; set; }
    }
}
