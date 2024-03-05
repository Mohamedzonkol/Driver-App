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

namespace Driver.DataServices.Repositories
{
    public class AchevmentsRepository:GenericRepository<Achevment>, IAchevmentsRepository
    {
        private readonly IDatabase _cacheDb;

        public AchevmentsRepository(ILogger logger, AppDbContext context, ConnectionMultiplexer redis) : base(logger, context,redis)
        {
            _cacheDb = redis.GetDatabase();

        }
        public async Task<Achevment?> GetDriverAchevments(Guid driverId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(e => e.Id == driverId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}GetDriverAchevments Function Error", typeof(AchevmentsRepository));
                throw;
            }
        }
        public override async Task<IEnumerable<Achevment>> GetAll()
        {
            try
            {
                var cachedData = await _cacheDb.StringGetAsync("Achevments:All");
                if (!cachedData.IsNullOrEmpty)
                {
                    return JsonSerializer.Deserialize<IEnumerable<Achevment>>(cachedData);
                }

                IEnumerable<Achevment> achevments = await _dbSet.Where(w => w.Status == 1).AsNoTracking()
                    .AsSplitQuery().
                    OrderBy(o => o.CreatedAt).ToListAsync();
                var expiryTime = TimeSpan.FromMinutes(30);
                await _cacheDb.StringSetAsync("Achevments:All", JsonSerializer.Serialize(achevments),expiryTime);
                return achevments;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}GetAll Function Error", typeof(AchevmentsRepository));
                throw;
            }
        }
        public override async Task<bool> Delete(Guid driverId)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == driverId);
                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdateDateTime = DateTime.Now;

                await UpdateCache();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}Delete Function Error", typeof(AchevmentsRepository));
                throw;
            }
        }
        public override async Task<bool> Update(Achevment achevment)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(e => e.Id == achevment.Id);
                if (result == null)
                    return false;

                result.UpdateDateTime = DateTime.Now;
                result.Champainship = achevment.Champainship;
                result.PolePostion = achevment.PolePostion;
                result.RaceWins = achevment.RaceWins;
                result.FastestLab = achevment.FastestLab;

                await UpdateCache();

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}Update Function Error", typeof(AchevmentsRepository));
                throw;
            }
        }

        private async Task UpdateCache()
        {
            try
            {
                var achevments = await _dbSet.Where(w => w.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(o => o.CreatedAt)
                    .ToListAsync();
                var expiryTime = TimeSpan.FromMinutes(30);

                await _cacheDb.StringSetAsync("Achevments:All", JsonSerializer.Serialize(achevments),expiryTime);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating cache in AchevmentsRepository");
                throw;
            }
        }

    }
}
