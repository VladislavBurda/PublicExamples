using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StudentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Components
{
    public class UserCountViewComponent
    {
        IUserService _userService;

        public UserCountViewComponent(IUserService userService)
        {
            _userService = userService;
            
        }

        public string Invoke()
        {
            UserCountViewModel model = new UserCountViewModel();

            model.UserCount = _userService.GetAllUserInRole("user").Count();
            model.TeacherCount = _userService.GetAllUserInRole("teacher").Count();

            return $" Учеников: {model.UserCount} Преподователей: {model.TeacherCount}  ";
        }
    }
}
