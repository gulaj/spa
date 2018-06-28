using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Bus.Service;
using Bus.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using spa.Controllers;
using spa.Models;

namespace spa
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
	        services.AddAutoMapper();


			var builder = new ContainerBuilder();

			builder.RegisterType<WorkerBusService>().AsImplementedInterfaces();

			builder.Populate(services);

			var container = builder.Build();
			return container.Resolve<IServiceProvider>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

		
			if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
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
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
	}
	
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<WorkerDetailsPosted, FormDataWorkerDetails>();
			CreateMap<FormDataWorkerDetails, WorkerDetailsPosted>();

			CreateMap<WorkerDetailsPosted, DataWorkerDetails>();
			CreateMap<DataWorkerDetails, WorkerDetailsPosted>();
		}
	}
}
