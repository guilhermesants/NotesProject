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

        public LembreteController(ILembreteRepository lembreteRepository)
        {
            _lembreteRepository = lembreteRepository;
        }

        public IActionResult CriarLembrete(Nota nota)
        {
            if (ModelState.IsValid)
            {
                var lembrete = _lembreteRepository.GetNotaByName(nota.Nome_Lembrete);
                if (lembrete == null)
                {
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
