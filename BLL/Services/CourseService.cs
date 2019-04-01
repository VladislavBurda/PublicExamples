using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class CourseService : ICourseService
    {
        IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddNewCourse(CourseDTO courseDTO)
        {
            _unitOfWork.Course.Add(new Course
            {
                Title = courseDTO.Title
            });
            _unitOfWork.Save();
        }

        public void AddToCourse(string userId, int courseId)
        {
            _unitOfWork.UserCourse.Add(new UserCourse
            {
                CourseId = courseId,
                UserId = userId
            });
            _unitOfWork.Save();
        }

        public void DeleteFromCourse(string userId, int courseId)
        {
            _unitOfWork.UserCourse.Delete(new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            });
            _unitOfWork.Save();
        }

        public IEnumerable<CourseDTO> GetAllCourses()
        {
            return _unitOfWork.Course.Get()
                .Select(x => new CourseDTO
                {
                    Id = x.CourseId,
                    Title = x.Title
                });
        }

        public CourseDTO GetCourse(int idCourse)
        {
            return _unitOfWork.Course.Get(x => x.CourseId == idCourse)
                .Select(z => new CourseDTO
                {
                    Id = z.CourseId,
                    Title = z.Title
                })
                .SingleOrDefault();
        }

        public IEnumerable<CourseDTO> GetCoursesForUser(string userId)
        {
            return _unitOfWork.Course.Get(x => x.UserCourses.Any(z => z.ApplicationUser.Id == userId))
                .Select(z => new CourseDTO
                {
                    Id = z.CourseId,
                    Title = z.Title
                });
        }
    }
}
