namespace SureCar.Services.Interface
{
    public interface IDatabaseService
    {
        Task PrepareDatabaseIfNotExists();
    }
}
