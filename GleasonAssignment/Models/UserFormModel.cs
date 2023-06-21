using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GleasonAssignment.Models
{
    public class UserFormModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter UserName")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsTrailUser { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Roles { get; set; }
    }
}
