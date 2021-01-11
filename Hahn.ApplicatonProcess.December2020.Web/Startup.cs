using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Hahn.ApplicatonProcess.December2020.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Hahn.ApplicatonProcess.December2020.Domain;
using FluentValidation.AspNetCore;

namespace Hahn.ApplicatonProcess.December2020.Web
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
            // Inject your application services
            InjectAppServices(services);
            services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });
            // services.AddDbContext<ApplicationDbContext>(x => x.UseSqlite("Data Source=LocalDatabase.db"));
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ApplicationDbContext"));
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicatonProcess.December2020.Web", Version = "v1" });
            });
            services.AddControllersWithViews();
    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicatonProcess.December2020.Web v1"));
                // app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // db.Database.Migrate();
            db.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseCookiePolicy();

            app.UseRouting();
            // app.UseRequestLocalization();
            app.UseCors("AllowOrigin");

            // app.UseAuthentication();
            app.UseAuthorization();
            // app.UseSession();
            // app.UseResponseCompression();
            // app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapControllerRoute(
                //     name: "default",
                //     pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InjectAppServices(IServiceCollection services)
        {  
            // Get connection string from appsettings.json  
            // services.AddDbContext<ApplicationDbContext>(options => {
            //     // options.UseSqlServer(Connection, b => b.MigrationsAssembly("Hahn.ApplicatonProcess.December2020.Web"));
            //     options.UseSqlServer(Configuration.GetConnectionString("ApplicationDbContext"));
            // });
            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicantValidator>());
            services.AddTransient<ApplicantValidator>();
            services.AddHttpClient();

            // Add Classes for Scoped DI  
            // Add Application DbContext object  
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<ApplicantRepository>();
            services.AddScoped<ApplicantViewModel>();
            services.AddScoped<ApplicantService>();
            
        }
    }
}
