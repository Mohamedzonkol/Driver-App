using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Driver.DataServicers.Data;
using Driver.DataServices.Repositories.Interfaces;
using Driver.Entites.DbsSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using WatchDog;

namespace Driver.DataServices.Repositories
{
    public class DriverRepository:GenericRepository<Drivers>,IDriverRepository
    {
        private readonly IDatabase _cacheDb;

        public DriverRepository(ILogger logger, AppDbContext context, ConnectionMultiplexer redis) : base(logger, context,redis)
        {
            _cacheDb = redis.GetDatabase();
        }

        public override async Task<IEnumerable<Drivers>> GetAll()
        {
            try
            {
                var cachedData = await _cacheDb.StringGetAsync("Drivers:All");
                if (!cachedData.IsNullOrEmpty)
                {
                    return JsonSerializer.Deserialize<IEnumerable<Drivers>>(cachedData);
                }

                var drivers = await _dbSet.Where(w => w.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync();
                var expiryTime = TimeSpan.FromMinutes(30); 

                await _cacheDb.StringSetAsync("Drivers:All", JsonSerializer.Serialize(drivers), expiryTime);

                return drivers;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}GetAll Function Error", typeof(DriverRepository));
                WatchLogger.LogError("Get All Driver Funtion Is Down");
                throw;
            }
        }
        public override async Task<bool> Delete(Guid driverId)
        {
            try
            {
                var result=await _dbSet.FirstOrDefaultAsync(e => e.Id == driverId);
                if (result == null)
                    return false;
                result.Status= 0;
                result.UpdateDateTime=DateTime.Now;

                // Update the cache
                await UpdateCache();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}Delete Function Error", typeof(DriverRepository));
                WatchLogger.LogError("Delete Funtion Is Down");

                throw;
            }
        }
        public override async Task<bool> Update(Drivers driver)
        {
            try
            {
                var result=await _dbSet.FirstOrDefaultAsync(e => e.Id == driver.Id);
                if (result == null)
                    return false;
                result.UpdateDateTime=DateTime.Now;
                result.DriverNumber= driver.DriverNumber;
                result.DataOFBirth= driver.DataOFBirth;
                result.FirstName= driver.FirstName;
                result.LastName= driver.LastName;
                // Update the cache
                await UpdateCache();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}Update Function Error", typeof(DriverRepository));
                WatchLogger.LogError("Update Funtion Is Down");
                throw;
            }
        }
        private async Task UpdateCache()
        {
            try
            {
                // Retrieve all drivers from the database
                var drivers = await _dbSet.Where(w => w.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync();
                var expiryTime = TimeSpan.FromMinutes(30); // Example: Set expiry time to 30 minutes

                // Update the cache with the new data
                await _cacheDb.StringSetAsync("Drivers:All", JsonSerializer.Serialize(drivers), expiryTime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating cache in DriverRepository");
                WatchLogger.LogError("Update Cache Funtion Is Down");

            }
        }

    }
}
