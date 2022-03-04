using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public bool HasCities { get; set; }
        public List<City> CityList { get; set; }
    }
}
