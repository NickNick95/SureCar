using Newtonsoft.Json;

namespace SureCar.DataManager.Models
{
    /// <summary>
    /// The Vehicle model
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// The Id
        /// </summary>
        [JsonProperty("_id")]
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
        public string DateAdded { get; set; }
    }
}
