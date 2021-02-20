using Microsoft.AspNetCore.Authentication;
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
        private readonly IUserClaimsPrincipalFactory<Usuario> _userClaimsPrincipalFactory;
        public UsuarioController(UserManager<Usuario> userManager, IUserClaimsPrincipalFactory<Usuario> userClaimsPrincipalFactory)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(RegisitroUsuario model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new Usuario()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                }
                return View("Success");
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

        public async Task<IActionResult> Login(LoginUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

                        await HttpContext.SignInAsync("Identity.Application", principal);

                        //return RedirectToAction("ListarLembretes", new { id = user.Id});
                        return RedirectToAction("AreaDoUsuario", new { id = user.Id });
                    }

                    ModelState.AddModelError("", "Usuário ou senha inválida");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> ListarLembretes(string id)
        {
            return View();
        }

        public async Task<IActionResult> AreaDoUsuario(string? id)
        {
            
            return View();
        }
    }
}
