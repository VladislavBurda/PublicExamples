using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UserDTO> GetAllUser()
        {
            return _unitOfWork.ApplicationUser.Get()
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surrname = x.Surname
                });
        }

        public IEnumerable<UserDTO> GetAllUserInRole(string role)
        {
            return _unitOfWork.ApplicationUser.GetAllUserInRole(role)
                .Select(x => new UserDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surrname = x.Surname
                });
        }

        public UserDTO GetUser(string UserName)
        {
            var user = _unitOfWork.ApplicationUser.Get(x => x.UserName == UserName).FirstOrDefault();
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surrname = user.Surname
            };
        }

        public IEnumerable<UserDTO> GetAllUsersWithRoleForCourse(string role, int CourseId)
        {
            var userInRole = _unitOfWork.ApplicationUser.GetAllUserInRole(role);
            var userWithCourse = _unitOfWork.UserCourse
                .Get(x => x.Course.CourseId == CourseId 
                && userInRole.Any(y => y.Id == x.UserId));
            return userWithCourse.Select(x => new UserDTO
            {
                Id = x.UserId,
                Name = x.ApplicationUser.Name,
                Surrname = x.ApplicationUser.Surname
            });
        }

        public IEnumerable<UserDTO> GetAllUsersWithRoleNotInCourse(string role, int CourseId)
        {
            var userInRole = _unitOfWork.ApplicationUser.GetAllUserInRole(role);
            List<UserDTO> list = new List<UserDTO>();
            foreach (var item in userInRole)
            {
                if (_unitOfWork.UserCourse.Get(x => x.Course.CourseId == CourseId && x.ApplicationUser.Id == item.Id).Count() == 0)
                {
                    list.Add(new UserDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Surrname = item.Surname
                    });
                }
            }

            return list;
        }

        public UserDTO GetUserId(string UserId)
        {
            var user = _unitOfWork.ApplicationUser.Get(x => x.Id == UserId).FirstOrDefault();
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surrname = user.Surname
            };
        }
    }
}
