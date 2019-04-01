using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.ViewModel.StudentViewModels;

namespace StudentWeb.Controllers
{
    [Authorize(Roles = "user")]
    public class StudentController : Controller
    {
        IUserService _userService;
        IRatingsService _ratingsService;
        ICourseService _courseService;

        public StudentController(IUserService userService, IRatingsService ratingsService, ICourseService courseService)
        {
            _userService = userService;
            _ratingsService = ratingsService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var user = _userService.GetUser(User.Identity.Name);
            StudentViewModel model = new StudentViewModel()
            {
                Name = user.Name,
                SurrName = user.Surrname,
                Courses = new List<Course>()
            };

            var courses = _courseService.GetCoursesForUser(user.Id);
            foreach (var item in courses)
            {
                Course course = new Course()
                {
                    CourseName = item.Title
                };
                course.Rate = _ratingsService.GetRaitingsForUser(user.Id, item.Id)
                    .Select(x => x.Grade)
                    .ToList();
                if (course.Rate.Count() > 0)
                    course.AverageRate = course.Rate.Average();

                model.Courses.Add(course);
            }


            return View(model);
        }
    }
}