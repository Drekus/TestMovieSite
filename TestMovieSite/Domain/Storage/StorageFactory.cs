using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestMovieSite.Data;

namespace TestMovieSite.Domain.Storage
{
    public class StorageFactory
    {

        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;
        private readonly ILogger<LocalStorage> _logger;

        public StorageFactory(DbContextOptions<ApplicationDbContext> dbContextOptions, ILogger<LocalStorage> logger) 
        {
            _dbContextOptions = dbContextOptions;
            _logger = logger;
        }       
        

        public async Task<IStorage> GetDefaultStorage()
        {
            await using var db = new ApplicationDbContext(_dbContextOptions);
            var storage = await db.Storages.Where(s=>s.IsDefault).FirstAsync();
            return GetStorage(storage);
        }
        
        private IStorage GetStorage(Models.Storage storage)
        {
            switch (storage.Type)
            {
                case StorageType.LocalStorage:
                    return new LocalStorage(storage, _logger);
                default:
                    throw new ArgumentException($"Invalid storage type: {storage.Type}");
            }
        }
    }
}