using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Oblivion.Models;
using Oblivion.Models.Account;
using Oblivion.Models.Account.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Oblivion.Controllers
{
    #region Old Role Edit
    //public async Task<IActionResult> RoleAssign(string userId)
    //{
    //    var user = await _userManager.FindByIdAsync(userId);
    //    //string id = "f192b27f-a894-4bd6-9dcb-8101eac63b46";
    //    //var user = await _userManager.FindByIdAsync(id);
    //    var identityRoles = _roleManager.Roles.ToList();
    //    List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
    //    List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();
    //    identityRoles.ForEach(role => roleAssignViewModels.Add(new RoleAssignViewModel
    //    {
    //        HasAssign = userRoles.Contains(role.Name),
    //        Id = role.Id,
    //        RoleName = role.Name
    //    }));
    //    return View(roleAssignViewModels);
    //}

    //[HttpPost]
    //public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> modelList)
    //{
    //    string id = "f192b27f-a894-4bd6-9dcb-8101eac63b46";
    //    var user = await _userManager.FindByIdAsync(id);
    //    foreach (RoleAssignViewModel role in modelList)
    //    {
    //        if (role.HasAssign)
    //            await _userManager.AddToRoleAsync(user, role.RoleName);
    //        else
    //            await _userManager.RemoveFromRoleAsync(user, role.RoleName);
    //    }
    //    return RedirectToAction("Index", "Account");
    //}
    #endregion
    #region Update
    //if (id != null)
    //{
    //    IdentityRole role = await _roleManager.FindByIdAsync(id);
    //    role.Name = roleViewModel.RoleName;
    //    await _roleManager.UpdateAsync(role);
    //}
    //else
    //{
    //    var newRole = await _roleManager.CreateAsync(new IdentityRole { Name = roleViewModel.RoleName });
    //    if (newRole.Succeeded)
    //    {
    //        RedirectToAction("Index", "Role");
    //    }
    //}
    //return View();
    #endregion
    [HtmlTargetElement("td", Attributes = "i-role")]
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;


        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
      
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (ModelState.IsValid)
            {

                var newRole = await _roleManager.CreateAsync(new IdentityRole(name));
                if (newRole.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var item in newRole.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View(name);
        }

        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            RoleEdit rw = new RoleEdit()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(rw);

         
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)

                            foreach (var item in result.Errors)

                                ModelState.AddModelError(item.Code, item.Description);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            foreach (var item in result.Errors)
                                ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await UpdateRole(model.RoleId);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }
            return View("Index", _roleManager.Roles);
        }




        public IActionResult Index()
        {
            return View(_roleManager.Roles);
        }
    }
}
