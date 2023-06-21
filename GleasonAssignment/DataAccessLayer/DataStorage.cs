using GleasonAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace GleasonAssignment.DataAccessLayer
{
    public class DataStorage:DbContext
    {
        public DataStorage(DbContextOptions options ):base(options)
        {
            
        }
        public DbSet<UserFormModel> Users { get; set; }
        public DbSet<RoleFormModel> Roles { get; set; }
        public DbSet<UserRolesFormModel> UserRoles { get; set; }
    }
}
