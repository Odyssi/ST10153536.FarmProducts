using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace PROG7311.FarmProducts.ST10153536.ViewModels
{
    public class RoleViewModel : IdentityRole
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        public string Name { get; set; }
    }
}
