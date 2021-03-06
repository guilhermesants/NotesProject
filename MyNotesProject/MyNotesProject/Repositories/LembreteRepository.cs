﻿using Microsoft.EntityFrameworkCore;
using MyNotesProject.Context;
using MyNotesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Repositories
{
    public class LembreteRepository : ILembreteRepository
    {
        private readonly MyDbContext _context;

        public LembreteRepository(MyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Nota> notas => _context.notas;

        public void add(Nota nota)
        {
            _context.notas.Add(nota);
        }

        public Nota GetNotaById(int id)
        {
            var nota = _context.notas.FirstOrDefault(n => n.Id == id);
            return nota;
        }

        public Nota GetNotaByName(string name)
        {
            var nota = _context.notas.FirstOrDefault(n => n.Nome_Lembrete.Equals(name));
            return nota;
        }

        public IEnumerable<Nota> notasDoUsuario(string id)
        {
            IQueryable<Nota> query = _context.notas;

            query = query.AsNoTracking().OrderBy(n => n.Id)
                .Where(nota => nota.usuario.Id == id);

            return query.ToArray();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
 }

