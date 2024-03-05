using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver.Entites.DbsSet;

namespace Driver.DataServices.Repositories.Interfaces
{
    public interface IAchevmentsRepository:IGenericRepository<Achevment>
    {
        Task<Achevment?> GetDriverAchevments(Guid driverId);
    }
}
