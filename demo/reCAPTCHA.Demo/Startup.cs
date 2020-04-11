using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Owl.reCAPTCHA;

namespace reCAPTCHA.Demo
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
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddreCAPTCHAV3(x =>
            {
                x.VerifyBaseUrl = "https://recaptcha.google.cn/";
                x.SiteKey = "6LccrsMUAAAAANSAh_MCplqdS9AJVPihyzmbPqWa";
                x.SiteSecret = "6LccrsMUAAAAAL91ysT6Nbhk4MnxpHjyJ_pdVLon";
            });

            services.AddreCAPTCHAV2(x =>
            {
                x.VerifyBaseUrl = "https://recaptcha.google.cn/";
                x.SiteKey = "6LcArsMUAAAAAKCjwCTktI3GRHTj98LdMEI9f9eQ";
                x.SiteSecret = "6LcArsMUAAAAAO_FBbZghC9aUa1F1rjvcdiOESKd";
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}