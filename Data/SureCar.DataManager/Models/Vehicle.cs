using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SureCar.DataManagers.Models
{
    /// <summary>
    /// The Vehicle model
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// The Id
        /// </summary>
        [NotMapped]
        public int Id { get; set; }
        /// <summary>
        /// The Make
        /// </summary>
        public string Make { get; set; }
        /// <summary>
        /// The Model
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// The Year model
        /// </summary>
        [JsonProperty("year_model")]
        public int YearModel { get; set; }
        /// <summary>
        /// The Price
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The Licensed
        /// </summary>
        public bool Licensed { get; set; }
        /// <summary>
        /// The Date added
        /// </summary>
        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }
    }
}
