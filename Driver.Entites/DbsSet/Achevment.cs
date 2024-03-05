namespace Driver.Entites.DbsSet
{
    public class Achevment:BaseEntity
    {
        public int RaceWins { get; set; }
        public int PolePostion { get; set; }
        public int FastestLab { get; set; }
        public int Champainship { get; set; }
        public Guid DriverId { get; set; }
        public virtual Drivers? Driver { get; set; }

    }
}
