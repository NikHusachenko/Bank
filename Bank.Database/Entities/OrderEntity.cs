namespace Bank.Database.Entities
{
    public class OrderEntity
    {
        public long AuthorNumber { get; set; }
        public long PayerNumber { get; set; }
        public string Appoinment { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Cost { get; set; }
    }
}