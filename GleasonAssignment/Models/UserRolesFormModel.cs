using System.ComponentModel.DataAnnotations.Schema;

namespace GleasonAssignment.Models
{
    public class UserRolesFormModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        [NotMapped]
        public string RoleName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public List<int> SelectedRoleIdList { get; set; }

    }
}
