namespace CarService.DbAccess.Entities
{
    public class CustomerAuto
    {
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int? AutoId { get; set; }
        public Auto Auto { get; set; }
    }
}
