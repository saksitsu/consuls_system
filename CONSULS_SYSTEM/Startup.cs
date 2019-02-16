using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CONSULS_SYSTEM.DATA;
using CONSULS_SYSTEM.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CONSULS_SYSTEM
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
            services.AddDbContext<CONSULS_Context>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<Models.Config>(Configuration.GetSection("Config"));
            services.AddSingleton<IConfiguration>(Configuration);
            var serviceProvider = services.BuildServiceProvider();
            DbInit.INIT(serviceProvider);

            //ไม่ต้องแจ้งกรณี ถ้าลูกค้าไม่จด HTTPS
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddSingleton<LocService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");


            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            //new CultureInfo("de-CH"),
                            //new CultureInfo("fr-CH"),
                            new CultureInfo("th-TH")
                        };

                    options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    // You can change which providers are configured to determine the culture for requests, or even add a custom
                    // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                    // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                    // By default, the following built-in providers are configured:
                    // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                    // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                    // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                });

            services.AddSession(options =>
            {
                Guid g = Guid.NewGuid();
                //ส่วนนี้เป็น Security 
                //options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                options.CookieName = "SESSION_" + g.ToString();
                options.Cookie.HttpOnly = true;
                options.CookieHttpOnly = true;
                options.IdleTimeout = TimeSpan.FromDays(7);
                options.IOTimeout = TimeSpan.FromDays(7);
            });
            //Clear cookie ทุก ๆ  1 ชม.
            services.ConfigureApplicationCookie(options =>
            {
                //options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie.Expiration = TimeSpan.FromDays(365);
                options.SlidingExpiration = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
               
                //เอาไว้ทดสอบก่อน
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //Using Session
            app.UseSession();

            //Security Header
            app.Use((context, next) =>
            {
                context.Response.Headers.ContainsKey("Server");
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers["Server"] = string.Empty;
                return next();
            });

            //เอาออกชั่วคราว มันBlock ไฟล์รูปกับ Script เว็บนอกหมด
            //app.UseCsp(opts => opts
            //    .BlockAllMixedContent()
            //    .StyleSources(s => s.Self())
            //    .StyleSources(s => s.UnsafeInline())
            //    .FontSources(s => s.Self())
            //    .FormActions(s => s.Self())
            //    .FrameAncestors(s => s.Self())
            //    .ImageSources(s => s.Self())
            ////.ScriptSources(s => s.Self()) //--> ใส่ตัวนี้แล้ว reCaptcha จะไม่ขึ้น
            //);
            app.UseHsts(h => h.MaxAge(days: 365).IncludeSubdomains().Preload());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.Deny());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
