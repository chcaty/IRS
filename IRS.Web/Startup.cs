using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRS.BLL.Implements;
using IRS.BLL.Interface;
using IRS.Dal.Implements;
using IRS.Dal.Interface;
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
            var connection = Configuration.GetConnectionString("SqlServer");
            //var connection = "server=.;uid=sa;pwd=123;database=IRSDb";
            services.AddDbContext<IRSContext>(options => options.UseSqlServer(connection, b => b.UseRowNumberForPaging()));
            services.AddScoped<DbContext, IRSContext>();
            services.AddMvc();
            //services.AddSession();

            #region 依赖注入
            services.AddScoped<IUserDal, UserDal>();
            services.AddScoped<IRoleDal, RoleDal>();
            services.AddScoped<IPermissionDal, PermissionDal>();
            services.AddScoped<IRolePermissionDal, RolePermissionDal>();
            services.AddScoped<ICategoryInfoDal, CategoryInfoDal>();
            services.AddScoped<IRecordDal, RecordDal>();
            services.AddScoped<IProcessingRecordDal, ProcessingRecordDal>();
            //services.AddScoped<IPeopleGroupDal, PeopleGroupDal>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRolePermissionService, RolePermissionService>();
            services.AddScoped<ICategoryInfoService, CategoryInfoService>();
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IProcessingRecordService, ProcessingRecordService>();
            //services.AddScoped<IPeopleGroupService, PeopleGroupService>();
            #endregion

            #region 跨域
            services.AddCors(options => options.AddPolicy("Domain", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().AllowCredentials()));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Domain");

            //app.UseCors(builder => builder.WithOrigins("http://localhost"));

            app.UseMvc();
            //app.UseSession();
        }
    }
}
