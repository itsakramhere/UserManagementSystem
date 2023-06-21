using GleasonAssignment.Models;

namespace GleasonAssignment.IRepository
{
    public interface IUserRepository
    {
        List<UserFormModel> ShowAllUsers();
        bool SaveUser(UserFormModel obj,List<int> roleObj);
        List<RoleFormModel> GetRoles();
        bool UpdateUser(int id, UserFormModel obj, List<int> roleList);
        UserFormModel GetUser(int id);
        bool DeleteUser(int id);
        bool UserValidation(UserFormModel user);
    }
}
