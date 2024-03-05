using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Entites.DTOs.Responeses
{
    public class DriverResponses
    {
        public Guid DriverId { get; set; }
        public string FullName { get; set; }
        public int DriverNumber { get; set; }
        public DateTime DataOFBirth { get; set; }

    }
}
