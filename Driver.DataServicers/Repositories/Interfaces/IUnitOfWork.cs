using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.DataServices.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IDriverRepository DriversRepo { get; }
        IAchevmentsRepository AchevmentsRepo { get; }
        Task<bool> CompleteAsync();
    }
}
