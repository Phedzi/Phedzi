using Models.Common;
using System.Collections.Generic;

namespace Logic.Common
{
    public class ResponseService<T> : IResponseService<T>
    {

        public Response<T> Result(T result)
        {
            Response<T> response = new Response<T>();

            if (result != null)
            {
                response.status = "OK";
                response.message = "Results found";
                response.result = result;
                return response;
            }
            return response;
        }

        public Response<T> ResultMessage(T result, string status)
        {
            Response<T> response = new Response<T>();

            if (result != null)
            {
                response.status = status;
                response.result = result;
                response.message = "Transaction Successful";
                return response;
            }
            return response;
        }

        public ListResponse<T> Results(List<T> results)
        {
            ListResponse<T> response = new ListResponse<T>();

            if (results != null)
            {
                response.status = "OK";
                response.message = "Results found";
                response.count = results.Count;
                response.results = results;
                return response;
            }
            return response;
        }

        public ListResponse<T> TreeResults(List<T> trees)
        {
            ListResponse<T> response = new ListResponse<T>();

            if (trees != null)
            {
                response.status = "OK";
                response.message = "Results found";
                response.count = trees.Count;
                response.results = trees;
                return response;
            }
            return response;
        }
    }
}
