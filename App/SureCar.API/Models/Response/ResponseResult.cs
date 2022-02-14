using System.Net.Http.Headers;

namespace SureCar.API.Models.Response
{
    public class ResponseResult<T>
    {
        public bool IsSuccessful { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> Errors { get; set; }
        public T Content { get; set; }
        public int StatusCode { get; set; }
        public HttpResponseHeaders Headers { get; set; }
    }
}
