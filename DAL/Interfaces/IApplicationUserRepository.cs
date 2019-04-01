using DAL.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace DAL.Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetAllUserInRole(string role);
    }
}
