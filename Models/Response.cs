namespace LocationAvailabilityAPI.Models
{
    public class Response
    {
        public object data { get; set; }
        public object Message { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
