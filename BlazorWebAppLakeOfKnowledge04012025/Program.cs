using BlazorWebAppLakeOfKnowledge04012025.Components;
using BlazorWebAppLakeOfKnowledge04012025.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace BlazorWebAppLakeOfKnowledge04012025
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Add HTTP Client
            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7039"); // Use your API URL
            });

            // Register services
            builder.Services.AddScoped<ApiService>();

            // ✅ Add Authentication & Configure Cookie Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login-choose"; // Redirect unauthorized users
                    options.AccessDeniedPath = "/access-denied"; // ✅ Redirect if user lacks permission
                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.Redirect("/access-denied"); // ✅ Ensure redirection
                        return Task.CompletedTask;
                    };
                });

            // ✅ Add Authorization Policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("GuideOnly", policy => policy.RequireRole("Guide"));
                options.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"));
            });


            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // רישום ה-UserService כ-Singleton
            builder.Services.AddSingleton<UserService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAntiforgery();

            // ✅ Ensure Authentication & Authorization Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Components
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // ✅ Redirect Unauthorized Users (Login Page)
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 401) // Unauthorized (Not Logged In)
                {
                    context.HttpContext.Response.Redirect("/login-choose");
                }
                else if (context.HttpContext.Response.StatusCode == 403) // Forbidden (Lack Permissions)
                {
                    context.HttpContext.Response.Redirect("/access-denied");
                }
            });

            app.Run();
        }
    }
}












//using BlazorWebAppLakeOfKnowledge04012025.Components;
//using BlazorWebAppLakeOfKnowledge04012025.Services;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components.Authorization;
//using System;

//namespace BlazorWebAppLakeOfKnowledge04012025
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddRazorComponents()
//                .AddInteractiveServerComponents();

//            // Add HTTP Client
//            builder.Services.AddHttpClient("API", client =>
//            {
//                client.BaseAddress = new Uri("https://localhost:7039"); // ? Use your API URL
//            });

//            // Register services
//            builder.Services.AddScoped<ApiService>();

//            // Add authentication and authorization services
//            builder.Services.AddAuthorization(options =>
//            {
//                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
//                options.AddPolicy("GuideOnly", policy => policy.RequireRole("Guide"));
//                options.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"));
//            });

//            // Add Authentication (you can use your preferred method here)
//            //builder.Services.AddAuthorizationCore(); // For Blazor WebAssembly, or adjust for your authentication scheme
//            //builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>(); // Custom state provider if needed
//            //builder.Services.AddScoped<UserService>(); // If you use the UserService you mentioned for handling user details

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();

//            app.UseStaticFiles();
//            app.UseAntiforgery();

//            app.MapRazorComponents<App>()
//                .AddInteractiveServerRenderMode();


//            app.Run();
//        }
//    }
//}













//using BlazorWebAppLakeOfKnowledge04012025.Components;
//using BlazorWebAppLakeOfKnowledge04012025.Services;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using System;

//namespace BlazorWebAppLakeOfKnowledge04012025
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddRazorComponents()
//                .AddInteractiveServerComponents();



//            builder.Services.AddHttpClient("API", client =>
//            {
//                client.BaseAddress = new Uri("https://localhost:7039"); // ? Use your API URL
//            });

//            builder.Services.AddScoped<ApiService>();



//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();

//            app.UseStaticFiles();
//            app.UseAntiforgery();

//            app.MapRazorComponents<App>()
//                .AddInteractiveServerRenderMode();

//            app.Run();
//        }
//    }
//}

