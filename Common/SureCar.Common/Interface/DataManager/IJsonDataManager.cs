namespace SureCar.Common.Interface.DataManager
{
    /// <summary>
    /// JSON data manager is a public interface that is used to present base methods for parsing and loading date.
    /// </summary>
    public interface IJsonDataManager
    {
        /// <summary>
        /// Parses Json to model
        /// </summary>
        /// <typeparam name="T">The model</typeparam>
        /// <param name="json">The json file</param>
        /// <returns>Model</returns>
        T ParseJsonFromFile<T>(string filePath);
    }
}
