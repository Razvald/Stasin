using AquaDB.Database;

namespace AquaDB.Models
{
    public class DbContextProvider
    {
        private static DbContextProvider _instance;
        private readonly AppDbContext _dbContext;

        private DbContextProvider()
        {
            _dbContext = new AppDbContext();
        }

        public static DbContextProvider Instance
        {
            get
            {
                _instance ??= new DbContextProvider();
                return _instance;
            }
        }

        public AppDbContext GetDbContext()
        {
            return _dbContext;
        }
    }
}
