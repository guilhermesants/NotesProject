using Microsoft.EntityFrameworkCore;
using MyNotesProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotesProject.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Nota> notas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>(org =>
            {
                org.ToTable("usuarios");
                org.HasKey(x => x.Id);
                org.HasMany<Nota>().WithOne().HasForeignKey(x => x.IdUsuario).IsRequired(false);
            });
        }
    }
}
