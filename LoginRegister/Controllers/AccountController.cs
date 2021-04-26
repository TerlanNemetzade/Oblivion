using System;
using System.Threading.Tasks;
using AutoMapper;
using Email;
using Oblivion.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Oblivion.Models.Account;

namespace Oblivion.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
             
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel viewModel/*, string returnUrl = null*/)
        {
            #region OldRegister
            //ViewData["ReturnUrl"] = returnUrl;
            //if (ModelState.IsValid)
            //{
            //    //var user = new IdentityUser
            //    //{
            //    //    Email = viewModel.Email

            //    //};
            //    var user = _mapper.Map<IdentityUser>(viewModel);
            //    var result = await _userManager.CreateAsync(user, viewModel.Password);
            //    if (result.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return RedirectToAction(nameof(SuccessRegistration));
            //    }

            //    AddError(result);
            //}
            //return View(viewModel);
            #endregion
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = _mapper.Map<User>(viewModel);

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(viewModel);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail),
                "Account", new { token, email = user.Email }, Request.Scheme);

            var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
            await _emailSender.SendEmailAsync(message);

            await _userManager.AddToRoleAsync(user, "Client");

            return RedirectToAction(nameof(SuccessRegistration));
        }

        [HttpGet]

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
              
                var user = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, viewModel.RememberMe, false);
                
                if (user.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Username or password is incorrect");
            }
            return View(viewModel);
        }
        
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel passwordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(passwordViewModel);
            
            }
            var user = await _userManager.FindByEmailAsync(passwordViewModel.Email);

            if (user == null)
            {
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBack = Url.Action(nameof(ResetPassword), "Account",
                new { token, email = user.Email }, Request.Scheme);

            var message = new Message(new string[] { user.Email },
                "Reset password", callBack, null);

            await _emailSender.SendEmailAsync(message);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string token,string email)
        {
            var model = new ResetPasswordModel { Email=email,Token=token };
            return View(model);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetModel);
            }
            var user = await _userManager.FindByEmailAsync(resetModel.Email);
            if (user == null)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetModel.Token, resetModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach(var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }


        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        private void AddError(IdentityResult identityResult)
        {
            foreach (var item in identityResult.Errors)
            {
                Console.WriteLine(string.Empty, item.Description);
            }
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
    }
}
