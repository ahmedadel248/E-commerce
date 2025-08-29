namespace E_commerce.ViewModels
{

    public class ProductFilterVM
    {
        public string? ProductName { get; set; }
        public double? Minprice { get; set; }
        public double? Maxprice { get; set; }
        public bool? IsHot { get; set; }
        public int? CatgoryId { get; set; }
    }
}
