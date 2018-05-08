using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRS.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IRS.Web
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
            //添加ef的依赖  
            var connection = "server=.;uid=sa;pwd=123;database=IRSDb";
            services.AddDbContext<IRSContext>(options => options.UseSqlServer(connection));
            services.AddScoped<DbContext, IRSContext>();
            services.AddMvc();

            #region 依赖注入
            //services.AddScoped<IFaceDal, FaceDal>();
            //services.AddScoped<IPeopleDal, PeopleDal>();
            //services.AddScoped<IGroupDal, GroupDal>();
            //services.AddScoped<IPeopleGroupDal, PeopleGroupDal>();
            //services.AddScoped<IFaceService, FaceService>();
            //services.AddScoped<IPeopleService, PeopleService>();
            //services.AddScoped<IGroupService, GroupService>();
            //services.AddScoped<IPeopleGroupService, PeopleGroupService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
