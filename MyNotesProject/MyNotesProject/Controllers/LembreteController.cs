using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyNotesProject.Models;
using MyNotesProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Controllers
{
    public class LembreteController : Controller
    {
        private readonly ILembreteRepository _lembreteRepository;
        private readonly UserManager<Usuario> _userManager;

        public LembreteController(ILembreteRepository lembreteRepository, UserManager<Usuario> userManager)
        {
            _lembreteRepository = lembreteRepository;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> CriarLembrete(Nota nota)
        {
            if (ModelState.IsValid)
            {
                var lembrete = _lembreteRepository.GetNotaByName(nota.Nome_Lembrete);
                if (lembrete == null)
                {
                    var Usuario = await _userManager.FindByNameAsync(User.Identity.Name);

                    nota.usuario = Usuario;

                    _lembreteRepository.add(nota);
                    _lembreteRepository.Save();
                }
                else
                {
                    ModelState.AddModelError("", "Já existe uma nota com esse nome!");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CriarLembrete()
        {
            return View();
        }
    }
}
