using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(string UserName);
        UserDTO GetUserId(string UserId);
        IEnumerable<UserDTO> GetAllUser();
        IEnumerable<UserDTO> GetAllUserInRole(string role);
        IEnumerable<UserDTO> GetAllUsersWithRoleForCourse(string role, int CourseId);
        IEnumerable<UserDTO> GetAllUsersWithRoleNotInCourse(string role, int CourseId);
    }
}
