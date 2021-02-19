using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Models
{
    public class UsuarioClaimsPrincipalFactory : UserClaimsPrincipalFactory<Usuario>
    {
        public UsuarioClaimsPrincipalFactory(UserManager<Usuario> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {

        }
    }
}
