namespace NTierArchitecture.Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }          //Guid ne demek
        public string Name { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
