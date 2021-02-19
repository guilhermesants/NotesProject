using MyNotesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Repositories
{
    public interface ILembreteRepository
    {
        void add(Nota nota);
        IEnumerable<Nota> notas { get; }
        Nota GetNotaById(int id);
        Nota GetNotaByName(string name);
        void Save();
    }
}
