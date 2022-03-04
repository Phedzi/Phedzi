
namespace RepModels.Common
{
    public class Response<T>
    {
        public Response()
        {
            status = "Warning";
            message = "Error occured ";
        }
        public string status { get; set; }
        public string message { get; set; }
        public T result { get; set; }
    }
}
