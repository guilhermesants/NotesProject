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
        public UsuarioController(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
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
    }
}
