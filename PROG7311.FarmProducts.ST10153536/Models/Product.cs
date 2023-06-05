namespace PROG7311.FarmProducts.ST10153536.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<ProductFarmer>? ProductFarmers { get; set; }
    }
}
