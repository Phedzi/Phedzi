
using Models.Common;
using System.Collections.Generic;

namespace Logic.Common
{
    public interface IResponseService<T>
    {
        public Response<T> Result(T result);
        public ListResponse<T> Results(List<T> results);

        public ListResponse<T> TreeResults(List<T> trees);

        public Response<T> ResultMessage(T result,string status);

    }
}
