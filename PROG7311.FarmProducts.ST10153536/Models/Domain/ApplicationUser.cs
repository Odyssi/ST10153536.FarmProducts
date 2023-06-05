using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROG7311.FarmProducts.ST10153536.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        [ForeignKey("ApplicationRole")]
        public ICollection<string> Roles { get; set; } = new List<string>();

        public virtual ICollection<ProductFarmer> ProductFarmers { get; set; } = new HashSet<ProductFarmer>();
    }

}
