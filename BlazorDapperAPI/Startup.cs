using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using BlazorDapperAPI.Data;
using BlazorDapperAPI.Models;
namespace BlazorDapperAPI
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
            services.AddControllers();
            //Thêm cái này để domain có thể gọi api được
            services.AddCors(options => options.AddPolicy("Open",
                builder => builder.AllowAnyOrigin().AllowAnyHeader()));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlazorDapperAPI", Version = "v1" });
            });
            var SqlConnectionConfiguration = new SqlConnectionConfiguration(Configuration.GetConnectionString("DBConnectionString"));
            //AddSingleton: Tạo thể hiện 1 lần duy nhất từ lúc khởi chạy ứng dụng
            services.AddSingleton(SqlConnectionConfiguration);
            //AddScoped: Tạo lại thể hiện sau mỗi request, trong cùng 1 request được tái sử dụng
            services.AddScoped<IVideoRepository, VideoRepository>();
            //AddTransient: Tạo lại thể hiện sau mỗi request, trong cùng 1 request vẫn tạo lại
            //services.AddTransient
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlazorDapperAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("Open");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
