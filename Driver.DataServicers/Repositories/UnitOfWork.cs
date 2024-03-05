using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver.DataServicers.Data;
using Driver.DataServices.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Driver.DataServices.Repositories
{
    public class UnitOfWork:IUnitOfWork,IDisposable, IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly ConnectionMultiplexer _redis;
        public IDriverRepository DriversRepo { get; }
        public IAchevmentsRepository AchevmentsRepo { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("Logs");
            _redis = redis;
            DriversRepo = new DriverRepository(_logger, _context, _redis);
            AchevmentsRepo = new AchevmentsRepository(logger,_context, redis);
        }
        public async Task<bool> CompleteAsync()
        {
            var result= await _context.SaveChangesAsync();
            return result > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
