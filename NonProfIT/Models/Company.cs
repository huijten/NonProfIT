using Microsoft.AspNetCore.Identity;
using NonProfIT.Areas.Identity.Data;

namespace NonProfIT.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public ApplicationUser CompanyOwner { get; set; }
        public List<ApplicationUser> Employees { get; set; } 
    }
}
