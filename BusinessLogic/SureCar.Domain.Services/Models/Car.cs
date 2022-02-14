namespace SureCar.Services.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public List<Vehicle> Vehicles { get; set ; }
    }
}
