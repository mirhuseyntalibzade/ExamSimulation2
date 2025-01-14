using BL.DTOs.AuthDTO;
using BL.Exceptions;
using BL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            try
            {
                await _authService.RegisterAsync(user);
                return View("Login");
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            try
            {
                await _authService.LoginAsync(user);
                return RedirectToAction("Index","Home");
            }
            catch (OperationNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
