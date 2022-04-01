using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Extra
{
    // I added the filter attribute to try and make the pagination feature merged with links 
    // while being generic
    public class PagedList<T>
    {
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {

            this.TotalItems = (source == null)? 0: source.Count();
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.List = (source == null)? new List<T>() : source
                            .Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToList();
        }

        public int TotalItems { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public List<T> List { get; }
        public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);
        public bool HasPreviousPage => this.PageNumber > 1;
        public bool HasNextPage => this.PageNumber < this.TotalPages;
        public int NextPageNumber => this.HasNextPage ? this.PageNumber + 1 : this.TotalPages;
        public int PreviousPageNumber => this.HasPreviousPage ? this.PageNumber - 1 : 1;

        public PagingHeader GetHeader()
        {
            return new PagingHeader(
                 this.TotalItems, this.PageNumber,
                 this.PageSize, this.TotalPages);
        }
        public List<LinkInfo> GetLinks(IHttpContextAccessor _httpContextAccessor,string filterName,object filter)
        {
            string _baseUrl = _httpContextAccessor.HttpContext.Request.Host.Value + _httpContextAccessor.HttpContext.Request.Path.Value;

            if (_httpContextAccessor.HttpContext.Request.IsHttps)
                _baseUrl = "https://" + _baseUrl;
            else
                _baseUrl = "http://" + _baseUrl;

            var links = new List<LinkInfo>();

             if (this.HasPreviousPage)
                links.Add(CreateLink(_baseUrl, filterName, filter, this.PreviousPageNumber, "previousPage", "GET"));

            links.Add(CreateLink(_baseUrl, filterName, filter, this.PageNumber, "self", "GET"));

            if (this.HasNextPage)
                links.Add(CreateLink(_baseUrl, filterName, filter, this.NextPageNumber, "nextPage", "GET"));

            return links;
        }

        private LinkInfo CreateLink(string baseUrl,string filterName, object filter, int pageNumber, string rel, string method)
        {
            var hrf = "";

            if (filterName == "none")
            {
                hrf = baseUrl + '?' + "PageNumber" + '=' + pageNumber;
            }
            else
            {
                 hrf = baseUrl + '?' + filterName + '=' + filter + '&' + "PageNumber" + '=' + pageNumber;
            }

            if (String.IsNullOrEmpty(filterName))
                    hrf = baseUrl + '?' + "PageNumber" + '=' + pageNumber;

            return new LinkInfo
            {
                Href = hrf,
                Rel = rel,
                Method = method
            };
        }
    }
}
