using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class User : IdentityUser
    {
        public List<Place> PlaceList { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
    }
}
