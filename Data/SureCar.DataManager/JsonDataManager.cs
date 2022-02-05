using Newtonsoft.Json;
using SureCar.Common.Interface.DataManager;

namespace SureCar.DataManagers
{
    /// <summary>
    /// JSON data manager is a public class that is used to parse data from JSON files to model.
    /// </summary>
    public class JsonDataManager : IJsonDataManager
    {
        /// <summary>
        /// Parses Json to model
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <param name="json">The json file</param>
        /// <returns>Model</returns>
        public T ParseJsonFromFile<T>(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();
                T item = JsonConvert.DeserializeObject<T>(json);

                return item;
            }
        }
    }
}
