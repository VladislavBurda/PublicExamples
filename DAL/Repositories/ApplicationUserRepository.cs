using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DAL.Repositories
{
    public class ApplicationUserRepository : EntityBaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<ApplicationUser> GetAllUserInRole(string role)
        {
            var selectedRole = _context.Roles.SingleOrDefault(x => x.Name == role);
            if (selectedRole != null)
            {
                var userInRoles = _context.UserRoles.Where(x => x.RoleId == selectedRole.Id).ToList();
                return _context.ApplicationUsers.Where(x => userInRoles.Any(r => r.UserId == x.Id)).ToList();
            }
            else
                throw new Exception("Role NOT FOUND");
        }
    }
}
