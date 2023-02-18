using Microsoft.EntityFrameworkCore;
using ShortenProject.Database;

namespace ShortenProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services
                .AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("SqlServer")
				?? throw new InvalidOperationException("Connection string 'SqlServer' not found.");

			builder.Services.ConfigureIdentity(connectionString);


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
				//builder.Services.AddHsts(options =>
				//{
				//	options.Preload = true;
				//	options.IncludeSubDomains = true;
				//	options.MaxAge = TimeSpan.FromDays(60);
				//	options.ExcludedHosts.Add("itay.com");
				//	options.ExcludedHosts.Add("www.itay.com");
				//});
			}
            //using (var scope = app.Services.CreateScope())
            //{
            //    using var db = scope.ServiceProvider.GetService<UrlDbcontext>();
            //    db!.Database.Migrate();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
        }
    }
}