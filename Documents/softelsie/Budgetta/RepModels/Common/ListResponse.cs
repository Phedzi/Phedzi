using System.Collections.Generic;

namespace RepModels.Common
{
    public class ListResponse<T>
    {
        public ListResponse()
        {
            status = "Warning";
            count = 0;
            message = "Error occured";
        }
        public string status { get; set; }
        public int count { get; set; }
        public string message { get; set; }
        public List<T> results { get; set; }
    }
}
