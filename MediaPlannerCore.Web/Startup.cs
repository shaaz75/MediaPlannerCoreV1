using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Service.Services;

namespace MediaPlannerCore.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<MediaPlannerCoreContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddTransient<ICampaignService, CampaignService>();

            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IClientService, ClientService>();

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryService, CountryService>();

            services.AddTransient<IAdSetRepository, AdSetRepository>();
            services.AddTransient<IAdSetService, AdSetService>();

            services.AddTransient<IMediaChannelRepository, MediaChannelRepository>();
            services.AddTransient<IMediaChannelService, MediaChannelService>();

            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<ISupplierService, SupplierService>();

            

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbFactory, DbFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Campaigns}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "CampaignFilter",
                    pattern: "{controller=Campaigns}/{action=Index}/{filter}/{page?}");
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                   name: "AdminFilter",
                   pattern: "{area:Admin}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
