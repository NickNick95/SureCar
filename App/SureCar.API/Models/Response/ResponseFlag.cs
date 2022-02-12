namespace SureCar.API.Models.Response
{
    public class ResponseFlag
    {
        public ResponseFlag(bool flag)
        {
            Flag = flag;
        }

        public bool Flag { get; set; }
    }
}
