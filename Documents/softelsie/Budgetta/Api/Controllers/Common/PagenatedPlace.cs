using Models.Extra;
using System.Collections.Generic;

namespace Api.Controllers.Common
{
    public class PagenatedPlace
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public object Items { get; set; }
    }
}