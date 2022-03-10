using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsMVCApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // middleware class objects initalised
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            Action<IEndpointRouteBuilder> endpointBuilderAction = (EndpointBuilder) =>
            {
                var defaultRoute = new {controller = "Products" , action = "Index"};
                EndpointBuilder.MapControllerRoute("home", "{controller}/{action}/{arg?}", defaultRoute);
            };// respective middleware class methods initalised ( invoke )


            app.UseEndpoints(endpointBuilderAction);
        }
    }
}
