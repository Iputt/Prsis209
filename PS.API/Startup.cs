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
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.OpenApi.Models;
using System.IO;

namespace PS.API
{
    /// <summary>
    /// ����
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ������-����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "PS API",
                    Description = "Acting on PS",
                    Version = "v1",
                    //TermsOfService="None"
                    //Contact
                    //Licecse
                });
                //ΪSwagger JSON and UI����xml�ĵ�ע��·��
                string xmlPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "ps_swagger.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// �����м��/�˵�
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //�����м����������Swagger��ΪJSON�ս��
            app.UseSwagger();
            //�����м�������Swagger-UI��ָ��Swagger��ΪJSON�ս��
            app.UseSwaggerUI(c =>
            {
                //����json�ĵ�
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PS API V1");
                //������ø�Ŀ¼Ϊswagger������ֵ�ÿ�
                c.RoutePrefix = "";
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
