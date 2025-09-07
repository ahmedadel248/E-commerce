using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_commerce.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager,IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            ApplicationUser applicationUser = new()
            {
                Name = registerVM.Name,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Address = registerVM.Address

            };
            
           // applicationUser = registerVM.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return View(registerVM);
            }
            //send confirmation msg
            // confirmationLink = name of the action,
            // ,name of the controller, name of area we in ,new { parameters},Request.Scheme
            //  we store user data encrypted in the token 
            // token = link that we send to user to confirm his email
            // token to adjust the setting of the confirmation link 


            var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            //generte token and send to link
            var Link = Url.Action("ConfirmEmail", "Account", new
            {
                area = "Identity",
                token = token,
                userId = applicationUser.Id
            });
           await _emailSender.SendEmailAsync(applicationUser.Email, "Confirm your email", 
                $"<h1>Welcome to E-commerce</h1>" +
                $"<p>Please confirm your email by <a href='{Link}'>Clicking here</a></p>");

            TempData["success-notification"] = "Registeration is successful";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        // action to confirm email send token to action
        public async Task<IActionResult> ConfirmEmail(string token, string userId) 
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return NotFound(); 
                //if we search in db and not found
                //return BadRequest();
                //if user found but token is not valid value send by user is not valid
            }
           var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            
                TempData["error-notification"] = "Error while confirming your email";
            
            else
            
                TempData["success-notification"] = "Email confirmed successfully, you can login now";
            
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

    }
}
