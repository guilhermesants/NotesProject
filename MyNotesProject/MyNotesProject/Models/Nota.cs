using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }
        public string Nome_Lembrete { get; set; }
        public string descricao_Lembrete { get; set; }
        public DateTime Data_Lembrete { get; set; }

        public Usuario usuario { get; set; }
    }
}
