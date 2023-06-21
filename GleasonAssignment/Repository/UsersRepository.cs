using GleasonAssignment.DataAccessLayer;
using GleasonAssignment.IRepository;
using GleasonAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace GleasonAssignment.Repository
{
    public class UsersRepository : IUserRepository
    {
        private readonly DataStorage _dbContext;
        public UsersRepository(DataStorage dbcontext)
        {
            _dbContext = dbcontext;   
        }
        #region
        public bool UserValidation(UserFormModel user)
        {
            bool IsValidUser = false;
            var result = _dbContext.Users.Where(w => w.UserName == user.UserName && w.Password == user.Password).FirstOrDefault();
            if (result != null)
                IsValidUser = true;
            return IsValidUser;
        }
        public bool DeleteUser(int id)
        {
            var user = _dbContext.Users.Where(w => w.ID == id).FirstOrDefault();
            if (user != null)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Users.Remove(user);
                        _dbContext.SaveChanges();

                        var userAvailableRoles = _dbContext.UserRoles.Where(r => r.UserID == id).ToList();
                        foreach (var userRole in userAvailableRoles)
                        {
                            _dbContext.UserRoles.Attach(userRole);
                            _dbContext.UserRoles.Remove(userRole);
                        }
                        _dbContext.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }

            if (user != null)
            {

                return true;
            }
            return false;
        }

        public UserFormModel GetUser(int id)
        {
            UserFormModel result;
            var users = (from u in _dbContext.Users
                         join ur in _dbContext.UserRoles
                         on u.ID equals ur.UserID
                         join r in _dbContext.Roles
                         on ur.RoleID equals r.ID
                         into temp
                         from b in temp.DefaultIfEmpty()
                         select new UserFormModel
                         {
                             ID = u.ID,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             UserName = u.UserName,
                             IsTrailUser = u.IsTrailUser,
                             Roles = b.RoleName
                         }).Where(w => w.ID == id);

            result = new UserFormModel();
            foreach (var user in users.ToList())
            {
                result.ID = user.ID;
                result.UserName = user.UserName;
                result.Email = user.Email;
                result.FirstName = user.FirstName;
                result.LastName = user.LastName;
                result.IsTrailUser = user.IsTrailUser;
                result.Roles = string.Join(",", users.Select(s => s.Roles));
            }
            return result;
        }
        public bool UpdateUser(int id, UserFormModel obj, List<int> roleList)
        {
            var result = _dbContext.Users.Where(w => w.ID == id).FirstOrDefault();
            if (result != null)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        result.UserName = obj.UserName;
                        result.Email = obj.Email;
                        result.FirstName = obj.FirstName;
                        result.LastName = obj.LastName;
                        result.IsTrailUser = obj.IsTrailUser;
                        _dbContext.SaveChanges();
                        var availableUserRole = _dbContext.UserRoles.Where(w => w.UserID == id).ToList();
                        foreach (var role in availableUserRole)
                        {
                            _dbContext.UserRoles.Attach(role);
                            _dbContext.UserRoles.Remove(role);
                        }
                        _dbContext.SaveChanges();
                        foreach (var item in roleList)
                        {
                            var roleObj = new UserRolesFormModel
                            {
                                UserID = obj.ID,
                                RoleID = item
                            };
                            _dbContext.UserRoles.Add(roleObj);
                        }
                        _dbContext.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
            return false;
        }
        public bool SaveUser(UserFormModel obj, List<int> roleList)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Users.Add(obj);
                    _dbContext.SaveChanges();
                    foreach (var item in roleList)
                    {
                        var roleObj = new UserRolesFormModel
                        {
                            UserID = obj.ID,
                            RoleID = item
                        };
                        _dbContext.UserRoles.Add(roleObj);
                    }
                    _dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion
        public List<RoleFormModel> GetRoles()
        {
            return _dbContext.Roles.ToList();
        }
        public List<UserFormModel> ShowAllUsers()
        {
            List<UserFormModel> result;
            var users = (from u in _dbContext.Users
                         join ur in _dbContext.UserRoles
                         on u.ID equals ur.UserID
                         join r in _dbContext.Roles
                         on ur.RoleID equals r.ID
                         into temp
                         from b in temp.DefaultIfEmpty()
                         select new UserFormModel
                         {
                             ID = u.ID,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             Email = u.Email,
                             UserName = u.UserName,
                             IsTrailUser = u.IsTrailUser,
                             Roles = b.RoleName
                         }).ToList().OrderBy(o => o.ID);
            var userList = users.ToLookup(u => u.ID);
            result = new List<UserFormModel>();
            foreach (var item in userList)
            {
                foreach (var user in item.ToList())
                {
                    user.UserName = user.UserName;
                    user.Email = user.Email;
                    user.IsTrailUser = user.IsTrailUser;
                    user.Roles = string.Join(",", item.Select(s => s.Roles));
                    result.Add(user);
                    break;
                }
            }
            return result;

        }
    }
}
