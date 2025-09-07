using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace E_commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container to aplly SOLID PRINCPLE.
            // to use dbcontext we should add this service.
            //ADD Constractour to use on configure.
            builder.Services.AddDbContext<ApplicationDbContext>(option => 
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opition=>
            {
                opition.Password.RequiredLength = 8;
                opition.Password.RequireNonAlphanumeric = false;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IRepositories<Catgory>, Repository<Catgory>>();
            builder.Services.AddScoped<IRepositories<Brand>, Repository<Brand>>();
            builder.Services.AddScoped<IProductRepositories, ProductRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        //edit routing add area by defult customer
               // from pattern: "{controller=Home}/{action=Index}/{id?}")
               // to pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")

        }
    }
}
