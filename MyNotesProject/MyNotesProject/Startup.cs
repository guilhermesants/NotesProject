using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyNotesProject.Context;
using MyNotesProject.Models;
using MyNotesProject.Repositories;
using MyNotesProject.UsuarioStore;

namespace MyNotesProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //var connectionstring = @"Data Source=DESKTOP-PCPASGP\\SQLEXPRESS;Initial Catalog=LEMBRETESDB;Integrated Security=True";
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //services.AddDbContext<MyUsuarioDbContext>(
            //    opt => opt.UseSqlServer(connectionstring, sql => sql.MigrationsAssembly(migrationAssembly))
            //    );

            services.AddDbContext<MyUsuarioDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentityCore<Usuario>(options => { });

            services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<MyUsuarioDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<Usuario>, UsuarioClaimsPrincipalFactory>();

            services.AddTransient<ILembreteRepository, LembreteRepository>();
            
            services.AddScoped<IUserStore<Usuario>, 
                UserOnlyStore<Usuario, MyUsuarioDbContext>>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Usuario/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Lembrete}/{action=CriarLembrete}/{id?}");
            });
        }
    }
}
