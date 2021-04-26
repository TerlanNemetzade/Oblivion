using Email;
using Entity.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Oblivion.Models;
using Oblivion.Models.Account;
using System.Threading.Tasks;
using System.Linq;
using Entity.Business.Abstract;
using System.Security.Claims;

namespace Oblivion.Controllers
{

    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailSender _emailSender;
        private readonly IProductService _productService;
        private readonly SignInManager<User> _signInManager;
        public UserController(UserManager<User> usrMgr, IPasswordHasher<User> passwordHash, 
            IPasswordValidator<User> passwordVal, IProductService productService,
            IUserValidator<User> userValid, IEmailSender emailSender,
            SignInManager<User> signInManager) 
        {
            _userManager = usrMgr;
            _passwordHasher = passwordHash;
            _passwordValidator = passwordVal;
            _userValidator = userValid;
            _emailSender = emailSender;
            _productService = productService;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(_userManager.Users);
            
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user.UserName == "oblivion33")
            {
                TempData["message"] = Messages.TriedDeleteAdmin;
                RedirectToAction(nameof(Index));
            }
            //else if (_userManager.Users==null)
            //{
            //    await _signInManager.SignOutAsync();
            //}
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View("Index", _userManager.Users);
        }
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(string id, string email, string password)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult validEmail = null;
                if (!string.IsNullOrEmpty(email))
                {
                    validEmail = await _userValidator.ValidateAsync(_userManager, user);
                    if (validEmail.Succeeded)
                        user.Email = email;
                    else
                        Errors(validEmail);
                }
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    else
                        Errors(validPass);
                }
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded)
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    ;
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail",
                 "Account", new { token, email = user.Email }, Request.Scheme);

                    var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                  
                    await _emailSender.SendEmailAsync(message);
                    if (User.Identity.Name=="oblivion33")
                    {
                        if (result.Succeeded)
                            return RedirectToAction("Index");
                        else
                            Errors(result);
                    }
                    else
                    {
                        if (result.Succeeded)
                            return RedirectToAction("EditProfile");
                        else
                            Errors(result);
                    }
                   
                }

            }
            else
                ModelState.AddModelError("", "User Not Found");

            return View(user);

        }
        [AllowAnonymous]
        public async Task<IActionResult> EditProfile()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
           
             
            return View(user);
        }


        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);



    }
}
