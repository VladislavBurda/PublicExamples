using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.ViewModel.StudentViewModels;
using StudentWeb.ViewModel.TeacherViewModels;

namespace StudentWeb.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        IUserService _userService;
        ICourseService _courseService;
        IRatingsService _ratingsService;

        public TeacherController(IUserService userService, ICourseService courseService, IRatingsService ratingsService)
        {
            _userService = userService;
            _courseService = courseService;
            _ratingsService = ratingsService;
        }

        public IActionResult Index()
        {
            List<TeacherViewModel> model = new List<TeacherViewModel>();
            var user = _userService.GetUser(User.Identity.Name);
            var courses = _courseService.GetCoursesForUser(user.Id);
            foreach (var item in courses)
            {
                TeacherViewModel teacherView = new TeacherViewModel();
                teacherView.IdCourse = item.Id;
                teacherView.Title = item.Title;
                teacherView.Students = _userService.GetAllUsersWithRoleForCourse("user", item.Id)
                .Select(y => new Student
                {
                    Id = y.Id,
                    Name = y.Name,
                    SurrName = y.Surrname
                })
                .ToList();
                model.Add(teacherView);
            }
            return View(model);
        }

        public IActionResult AddStudent(int IdCourse)
        {
            AddStudentViewModel model = new AddStudentViewModel();
            var course = _courseService.GetCourse(IdCourse);
            model.Title = course.Title;
            model.Students = _userService.GetAllUsersWithRoleNotInCourse("user", course.Id)
                .Select(x => new Student
                {
                    Add = false,
                    Id = x.Id,
                    Name = x.Name,
                    SurrName = x.Surrname
                })
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(AddStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model.Students)
                {
                    if (item.Add)
                    {
                        var user = _userService.GetUserId(item.Id);
                        _courseService.AddToCourse(user.Id, model.IdCourse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(string Id, int IdCourse)
        {
            _courseService.DeleteFromCourse(Id, IdCourse);
            return RedirectToAction("Index");
        }

        public IActionResult AddRate(string Id, int IdCourse)
        {
            var user = _userService.GetUserId(Id);
            var course = _courseService.GetCourse(IdCourse);
            if (user != null && course != null)
            {
                UserRateViewModel model = new UserRateViewModel()
                {
                    Name = user.Name,
                    CourseId = course.Id,
                    SurrName = user.Surrname,
                    UserId = user.Id,
                    CourseName = course.Title,
                    AddRate = 5
                };
                model.Rate = _ratingsService.GetRaitingsForUser(user.Id, course.Id)
                    .Select(x => x.Grade);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddRate(UserRateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _ratingsService.AddRateToUser(new RaitingDTO
                {
                    CourseId = model.CourseId,
                    UserId = model.UserId,
                    Grade = model.AddRate
                });
                return RedirectToAction("Index");
            }
            else
            {
                int IdCourse = model.CourseId;
                return RedirectToAction("AddRate", new { model.UserId, IdCourse });
            }

        }
    }
}