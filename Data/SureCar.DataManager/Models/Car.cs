namespace SureCar.DataManagers.Models
{
    /// <summary>
    /// The Car model
    /// </summary>
    public class Car
    {
        /// <summary>
        /// The Location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The Vehicles
        /// </summary>
        public List<Vehicle> Vehicles { get; set; }
    }
}
