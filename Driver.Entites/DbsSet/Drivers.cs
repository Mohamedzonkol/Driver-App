using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Entites.DbsSet
{
    public class Drivers:BaseEntity
    {
        public Drivers()
        {
            Achevments = new HashSet<Achevment>();
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int DriverNumber { get; set; }
        public DateTime DataOFBirth { get; set; }
        public virtual ICollection<Achevment> Achevments{ get; set; }
    }
}
