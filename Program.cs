using System.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;

using VTT.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using App.Security.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace CS_58_TichHop_EntityFramework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();
            // lien ket databas
            var connectionString = configuration.GetConnectionString("MyblogContext");
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString); // Cấu hình kết nối đến cơ sở dữ liệu của bạn
            });
            
            // Send mail
            builder.Services.AddOptions();
            var mailsetting = configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailsetting);
            builder.Services.AddSingleton<IEmailSender, SendMailService>();

            // Lay UI Identity 
            // builder.Services.AddDefaultIdentity<AppUser>()
            //     .AddEntityFrameworkStores<MyBlogContent>()
            //     .AddDefaultTokenProviders();
            builder.Services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
                


            builder.Services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 3; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;

            });
            builder.Services.ConfigureApplicationCookie(options=>{
                options.LoginPath = "/Login/";
                options.LogoutPath = "/Logout/";
                options.AccessDeniedPath = "/khongduoctruycap.html";
            });

            // add đăng nhập google

            builder.Services.AddAuthentication()
                .AddGoogle(options=>{
                    var gconfig = configuration.GetSection("Authentication:Google");
                    options.ClientId = gconfig["ClientId"];
                    options.ClientSecret = gconfig["ClientSecret"];
                    //http://localhost:5224/dang-nhap-tu-google
                    options.CallbackPath = "/dang-nhap-tu-google";

                })
                .AddFacebook(options=>{
                    var fconfig = configuration.GetSection("Authentication:Facebook");
                        #pragma warning disable CS8601 // Possible null reference assignment.
                    options.AppId = fconfig["AppId"];
                        #pragma warning restore CS8601 // Possible null reference assignment.
                    options.AppSecret = fconfig["AppSecret"];
                    //http://localhost:5224/dang-nhap-tu-google
                    options.CallbackPath = "/dang-nhap-tu-facebook";

                });
            
            builder.Services.AddAuthorization(option => {
                option.AddPolicy("AllowEditRole", policyBuider =>{
                    policyBuider.RequireAuthenticatedUser();
                    policyBuider.RequireClaim("canedit", "user");
                    // policyBuider.Requirements.Add();


                });
                option.AddPolicy("InGenZ", policyBuider =>{
                    policyBuider.RequireAuthenticatedUser();
                    // policyBuider.RequireClaim("canedit", "user");
                    policyBuider.Requirements.Add(new GenZRequirement());


                });

                option.AddPolicy("ShowAdminMenu", pb =>{
                    pb.RequireRole("Admin");
                });

                option.AddPolicy("CanUpdateArticle", builder => {
                    builder.Requirements.Add(new ArticleUpdateRequirement()); 
                });                      

            });
            builder.Services.AddTransient<IAuthorizationHandler, AppAuthorizationHandler>();
 

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapRazorPages();

            app.Run();
        }
    }
}


/*
    CREATE, READ, UPDATE, DELETE (CRUD)
    dotnet aspnet-codegenerator razorpage -m VTT.models.Article -dc VTT.models.MyBlogContent -outDir Pages/Blog -udl --referenceScriptLibraries


    Identity: 
        - Athentication: Xác định danh tính -> login Logout
        - Quản lý user: Sign Up, User, Role...

    *Policy-based authorization
    * Claims-based authorization:
        Claims -> đặc tính, tính chất của đối tượng (User)



        /Identity/Account/Login
        /Identity/Account/Manage

    // Phát sinh code cho Identity 
    dotnet aspnet-codegenerator identity -dc VTT.models.MyBlogContent

    
    http://localhost:5224/dang-nhap-tu-google

    // Phát sinh code cho các trang .cs, .cshtml
    dotnet new page -o Areas\Admin\Pages\Role -n Index -p:n App.Admin.Role
    dotnet new page -o Areas\Admin\Pages\Role -n Create -p:n App.Admin.Role
    dotnet new page -o Areas\Admin\Pages\User -n EditUserRoleClaim -p:n App.Admin.Role

*/

