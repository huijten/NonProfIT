using Microsoft.AspNetCore.Identity;
using NonProfIT.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace NonProfIT.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Compensation { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public string? StudentId { get; set; }
        public ApplicationUser? Student { get; set; }
        public bool Available { get; set; }
    }
}
