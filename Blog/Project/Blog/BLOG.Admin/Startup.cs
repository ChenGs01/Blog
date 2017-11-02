using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BLOG.Models;
using Microsoft.EntityFrameworkCore;
using BLOG.DAL;
using BLOG.BLL;

namespace BLOG.Admins
{
    public class Startup
    {
<<<<<<< HEAD
       public Startup(IConfiguration configuration)
=======
        public Startup(IConfiguration configuration)
>>>>>>> 1 commit
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BLOGContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlConString")));


            services.AddMvc();
            services.AddScoped(typeof(IBaseDAL<>), typeof(BaseDAL<>));
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<IArticlesService, ArticlesService>();
            services.AddTransient<ICategorysService, CategorysService>();
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Admin}/{action=Login}/{id?}");
            });
        }
    }
}
