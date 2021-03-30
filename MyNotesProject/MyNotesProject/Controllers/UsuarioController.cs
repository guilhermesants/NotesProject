using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyNotesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _singInManager;
       
        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> singInManager)
        {
            _userManager = userManager;
            _singInManager = singInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegistroUsuario(RegisitroUsuario model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new Usuario()
                    {
                        
                        UserName = model.UserName
                    };

                    var resultUser = await _userManager.CreateAsync(user, model.Password);
                    if (resultUser.Succeeded)
                    {
                        await _singInManager.SignInAsync(user, false);
                        return RedirectToAction("AreaDoUsuario");
                    }
                }
                ModelState.AddModelError(String.Empty, "Já existe usuário com este nome");

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RegistroUsuario()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Success()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await _singInManager.SignInAsync(user, false);

                        return RedirectToAction("AreaDoUsuario");
                    }

                }
                ModelState.AddModelError("", "Usuário ou senha inválida");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ListarLembretes(string id)
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AreaDoUsuario()
        {
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToRoute(new { controller = "Usuario", action = "Login" });
        }
    }
}
