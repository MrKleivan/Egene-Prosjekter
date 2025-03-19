namespace YourRentalCarAPI.Entities
{
    public class CarInnStock
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public int ModelYear { get; set; }
        public string WheelDrive { get; set; }
        public int Seats { get; set; }
        public string Fuel { get; set; }

        public string Picture { get; set; }
    }
}
