using Models.Extra;
using Models.Squata;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepModels.Squata
{
    public class PagenatedPlace
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public List<PlaceModel> Items { get; set; }
    }
}
