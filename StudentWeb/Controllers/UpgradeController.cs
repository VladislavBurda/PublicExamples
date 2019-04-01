using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StudentWeb.Controllers
{
    [Authorize]
    public class UpgradeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UpgradeController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string pass)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            switch (pass)
            {
                case "123":
                    if (!await _userManager.IsInRoleAsync(user, "teacher"))
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, "teacher");
                    }
                    break;
                case "456":
                    if (!await _userManager.IsInRoleAsync(user, "dean"))
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, roles);
                        await _userManager.AddToRoleAsync(user, "dean");
                    }
                    break;
            }

            return RedirectToAction("Index","Home");
        }
    }
}