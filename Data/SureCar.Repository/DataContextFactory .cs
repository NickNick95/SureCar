using System.Data.Entity.Infrastructure;

namespace SureCar.Repositories
{
    public class DataContextFactory : IDbContextFactory<DataContext>
    {
        public DataContext Create()
        {
            return new DataContext("Server=LAPTOP-H3HB4GNU\\SQLEXPRESS;Database=SureCar;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
