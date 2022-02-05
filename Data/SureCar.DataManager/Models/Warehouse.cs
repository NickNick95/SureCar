using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.DataManagers.Models
{
    /// <summary>
    /// The Warehouse model
    /// </summary>
    public class Warehouse
    {
        /// <summary>
        /// The Id
        /// </summary>
        [NotMapped]
        public string Id { get; set; }
        /// <summary>
        /// The Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Location
        /// </summary>
        public Location Location { get; set; }
        /// <summary>
        /// The Car
        /// </summary>
        [JsonProperty("cars")]
        public Car Car { get; set; }

    }
}
