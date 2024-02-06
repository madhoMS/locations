namespace LocationAvailabilityAPI.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Location_Name { get; set; }
        public TimeSpan Opening_Time { get; set; }
        public TimeSpan Ending_Time { get; set; }
    }
}
