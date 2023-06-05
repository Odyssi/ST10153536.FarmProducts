using PROG7311.FarmProducts.ST10153536.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311.FarmProducts.ST10153536.Models
{
    public class ProductFarmer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public DateTime ReceivedDate { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<ProductFarmer> ApplicationUsers { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
    }
}
