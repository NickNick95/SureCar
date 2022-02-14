namespace SureCar.API.Models.Response
{
    public class ResponseMessage
    {
        public ResponseMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
