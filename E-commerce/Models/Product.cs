namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string MainImg   { get; set; }=string.Empty;
        public double ? Price { get; set; }
        public int  Quantity { get; set; }
        public int Rate { get; set; }
        public double Discount { get; set; }
        public int Traffic { get; set; }
        public int CatgoryId { get; set; }
        public Catgory Catgory { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public bool Status { get; set; }
    }
}
