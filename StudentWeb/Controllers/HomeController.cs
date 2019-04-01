using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using StudentWeb.Models;
using StudentWeb.ViewModel.HomeViewModels;
using X.PagedList;

namespace StudentWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        IUserService _userService;
        IRatingsService _ratingsService;
        ICourseService _courseService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IRatingsService ratingsService,
            ICourseService courseService)
        {
            _userManager = userManager;
            _userService = userService;
            _ratingsService = ratingsService;
            _courseService = courseService;
        }

        public IActionResult Index(int page = 1)
        {
            HomeViewModel model = new HomeViewModel();
            model.Students = GetAllStudent().ToPagedList(page, 20);
            model.Teachers = GetTeacher();
            if (User.IsInRole("dean"))
            {
                model.TeachersAllStudents = GetAllStudentWithTeacher();
                model.StudentsAvarageRateAbove = GetStudentAvaregeAbove();
                model.TeachersLowStudents = GetTeachersLowStudents();
            }

            return View(model);
        }

        private IEnumerable<Student> GetAllStudent()
        {
            List<Student> list = new List<Student>();
            var students = _userService.GetAllUserInRole("user");
            foreach (var item in students)
            {
                Student student = new Student()
                {
                    Name = item.Name,
                    SurrName = item.Surrname
                };
                var raitings = _ratingsService.GetAllRaitingsForUser(item.Id);
                if (raitings.Count() > 0)
                    student.AverageRate = raitings.Average(x => x.Grade);
                list.Add(student);
            }

            return list;
        }

        private IEnumerable<Teacher> GetTeacher()
        {
            List<Teacher> list = new List<Teacher>();
            var teachers = _userService.GetAllUserInRole("teacher");
            foreach (var item in teachers)
            {
                Teacher teacher = new Teacher()
                {
                    Name = item.Name,
                    CourseName = new List<string>()
                };

                var courses = _courseService.GetCoursesForUser(item.Id);
                int studentCount = 0;
                foreach (var course in courses)
                {
                    teacher.CourseName.Add(course.Title);
                    studentCount += _userService.GetAllUsersWithRoleForCourse("user", course.Id).Count();
                }
                teacher.CountStudent = studentCount;
                list.Add(teacher);
            }

            return list;
        }

        /// <summary>
        /// список тех преподавателей, предметы которых посещают все студенты
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetAllStudentWithTeacher()
        {
            List<string> list = new List<string>();
            var courses = _courseService.GetAllCourses();
            foreach (var item in courses)
            {
                var userInCourse = _userService.GetAllUsersWithRoleNotInCourse("user", item.Id).Count();
                if (userInCourse == 0)
                {
                    var teacherInCourse = _userService.GetAllUsersWithRoleForCourse("teacher", item.Id);
                    foreach (var teacher in teacherInCourse)
                    {
                        list.Add(teacher.Name);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// список тех студентов, чей средний балл выше среднего по всему списку студентов 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Student> GetStudentAvaregeAbove()
        {
            List<Student> list = new List<Student>();
            var students = _userService.GetAllUserInRole("user");

            double avvarageRate = 0;
            int countRate = 0;
            foreach (var item in students)
            {
                var raitings = _ratingsService.GetAllRaitingsForUser(item.Id);
                if (raitings.Count() > 0)
                {
                    double rate = raitings.Average(x => x.Grade);
                    if (rate > 0)
                    {
                        countRate++;
                        avvarageRate += rate;
                    }
                }
            }

            avvarageRate = avvarageRate / countRate;

            foreach (var item in students)
            {
                var raitings = _ratingsService.GetAllRaitingsForUser(item.Id);
                if (raitings.Count() > 0)
                {
                    double rate = raitings.Average(x => x.Grade);
                    if (rate >= avvarageRate)
                        list.Add(new Student
                        {
                            Name = item.Name,
                            SurrName = item.Surrname,
                            AverageRate = rate
                        });
                }
            }

            return list;
        }

        /// <summary>
        /// тех преподавателей, предметы которых посещают меньше всех студентов 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetTeachersLowStudents()
        {
            List<string> list = new List<string>();
            var teacher = GetTeacher().OrderBy(x => x.CountStudent).Take(3);
            foreach (var item in teacher)
            {
                list.Add(item.Name);
            }

            return list;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
