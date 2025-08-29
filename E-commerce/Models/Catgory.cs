using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Catgory
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="هذا الحقل مطلوب")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public ICollection <Product> Prouducts { get; set; } = new List <Product>();
    }
}
