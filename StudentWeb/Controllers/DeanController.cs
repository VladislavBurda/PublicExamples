using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.ViewModel.DeanViewModels;

namespace StudentWeb.Controllers
{
    [Authorize(Roles = "dean")]
    public class DeanController : Controller
    {
        IUserService _userService;
        IRatingsService _ratingsService;
        ICourseService _courseService;

        public DeanController(IUserService userService, IRatingsService ratingsService, ICourseService courseService)
        {
            _userService = userService;
            _ratingsService = ratingsService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            List<DeanViewModel> model = new List<DeanViewModel>();
            string role = "teacher";

            var course = _courseService.GetAllCourses();
            foreach (var item in course)
            {
                DeanViewModel deanViewModel = new DeanViewModel()
                {
                    Title = item.Title,
                    IdCourse = item.Id
                };
                deanViewModel.Teachers = _userService.GetAllUsersWithRoleForCourse(role, item.Id)
                    .Select(x => new Teacher()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SurrName = x.Surrname
                    });
                model.Add(deanViewModel);
            }


            return View(model);
        }

        public IActionResult CreateCourse()
        {
            CreateCourseViewModel model = new CreateCourseViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateCourse(CreateCourseViewModel model)
        {
            _courseService.AddNewCourse(new CourseDTO
            {
                Title = model.Title
            });

            return RedirectToAction("Index");
        }

        public IActionResult AddTeacher(int idCourse)
        {
            AddTeacherViewModel model = new AddTeacherViewModel();
            var course = _courseService.GetCourse(idCourse);
            model.CourseId = course.Id;
            model.Title = course.Title;
            model.Teachers = _userService.GetAllUsersWithRoleNotInCourse("teacher", idCourse)
                .Select(x => new Teacher
                {
                    Id = x.Id,
                    Name = x.Name,
                    SurrName = x.Surrname
                });

            return View(model);
        }

        public IActionResult AddTeacherToCourse(int CourseId, string Id)
        {
            _courseService.AddToCourse(Id, CourseId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTeacherFromCourse(int IdCourse, string Id)
        {
            _courseService.DeleteFromCourse(Id, IdCourse);
            return RedirectToAction("Index");
        }
    }
}