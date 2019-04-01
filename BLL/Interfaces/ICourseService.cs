using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseDTO> GetCoursesForUser(string userId);
        IEnumerable<CourseDTO> GetAllCourses();
        void AddNewCourse(CourseDTO courseDTO);
        CourseDTO GetCourse(int idCourse);
        void AddToCourse(string userId, int courseId);
        void DeleteFromCourse(string userId, int courseId);
    }
}
