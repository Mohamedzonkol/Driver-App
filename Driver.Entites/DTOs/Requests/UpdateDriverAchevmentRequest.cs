using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Entites.DTOs.Requests
{
    public class UpdateDriverAchevmentRequest
    {
        public Guid DriverId { get; set; }
        public int RaceWins { get; set; }
        public int PolePostion { get; set; }
        public int FastestLab { get; set; }
        public int Champainship { get; set; }
    }
}
