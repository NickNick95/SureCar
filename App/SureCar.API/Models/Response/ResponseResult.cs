using System.Net.Http.Headers;

namespace SureCar.API.Models.Response
{
    public class ResponseResult<T>
    {
        public string ErrorMessage { get; set; }
        public T Content { get; set; }
        public int StatusCode { get; set; }
        public HttpResponseHeaders Headers { get; set; }
    }
}
