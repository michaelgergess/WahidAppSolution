using Application.Contract;
using Application.Service.User;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Model;
using System.Configuration;

namespace UserPresentation.Controllers
{
  
    public class AccountController : Controller
    {

        private readonly IuserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(IuserService userService, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found."); 
                return View(loginDTO);
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.password))
            {
                ModelState.AddModelError("", "Invalid password."); 
                return View(loginDTO);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(loginDTO);

                return View();
            }
            else
            {
                ModelState.AddModelError("", "Password or Email Not Correct"); // Add custom error message for general validation errors
                return View(loginDTO);
            }
        }

        public async Task<IActionResult> Register()
        {

            return View();

        }
        [HttpPost]

        public async Task<IActionResult> Register(UserRegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Registration(registerDTO, "admin");
                return RedirectToAction("Login", new UserLoginDTO { Email = registerDTO.Email,password = registerDTO.password});
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        public async Task<IActionResult> logOut()
        {
          await _userService.LogoutUser();
            return View("Login");
        }
       
        public async Task<IActionResult> CheckUserName(string UserName) {
            var user = await _userManager.FindByNameAsync(UserName);
          if (user == null) {
                return Json(true);
            }
           
            return Json(false);

        }
        public async Task<IActionResult> CheckEmail1(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return Json(true);
            }

            return Json(false);

        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllUsers()
        {
           var res =await _userService.GetAllUsers();
            return View(res.Entity);
        }
       
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ToggleBlockStatus(string UserName)
        {
            BlockUserDTO blockUserDTO = new BlockUserDTO();
            blockUserDTO.Name = UserName;
            var user = await _userManager.FindByNameAsync(UserName);
            if (user.IsBlocked is false) {
                blockUserDTO.IsBlocked = true;
            }
            else {
                blockUserDTO.IsBlocked = false;
            }

            await _userService.BlockOrUnBlockUser(blockUserDTO);
            return RedirectToAction("GetAllUsers");
        }

    }
}
