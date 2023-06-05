using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PROG7311.FarmProducts.ST10153536.Models.Domain
{
    public class ApplicationRole : IdentityRole
    {
        
        public const string Admin = "Admin";
        public const string Employee = "Employee";
        public const string Farmer = "Farmer";
    }
}
