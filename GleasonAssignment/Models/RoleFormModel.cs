using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GleasonAssignment.Models
{
    public class RoleFormModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string? RoleName { get; set; }
        [NotMapped]
        public bool IsAssigned { get; set; }
    }
}
