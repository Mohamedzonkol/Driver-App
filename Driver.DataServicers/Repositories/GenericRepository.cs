using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Driver.DataServicers.Data;
using Driver.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using WatchDog;

namespace Driver.DataServices.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ILogger _logger;
        protected AppDbContext _context;
        internal DbSet<T> _dbSet;
        protected readonly IDatabase _cacheDb;
        public GenericRepository(ILogger logger,AppDbContext context, ConnectionMultiplexer redis)
        {
            _logger = logger;
            _context = context;
            _dbSet= context.Set<T>();
        }
        public  virtual Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T?> GetById(Guid id)
        {
            var cachedData = await _cacheDb.StringGetAsync($"{typeof(T).Name}:{id}");
            if (!cachedData.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            var entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                var expiryTime = TimeSpan.FromMinutes(30); 
                await _cacheDb.StringSetAsync($"{typeof(T).Name}:{id}", JsonSerializer.Serialize(entity),expiryTime);
            }

            return entity;
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                // Add entity to the database
                await _dbSet.AddAsync(entity);
                var expiryTime = TimeSpan.FromMinutes(30); 

                // Store newly added entity in cache
                await _cacheDb.StringSetAsync($"{typeof(T).Name}:", JsonSerializer.Serialize(entity),expiryTime);

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo}Add Function Error", typeof(T));
                WatchLogger.LogError("Add  Funtion Is Down");

                throw;
            }
        }

        public virtual Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    } 

    
    
}
