﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; }
    }
}
