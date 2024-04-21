using AutoMapper;
using Application.Contract;
using Application.Service.User;
using Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using Application.Mapper;
using Application.Services.Article;
using Infrastructure;
using Microsoft.Data.SqlClient;
using UserPresentation.Hubs;
using Application.Services.WorldChat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie();
//SignalR
builder.Services.AddSignalR();
builder.Services.AddScoped<IuserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IReportArticleRepository, ReportArticleRepository>();
builder.Services.AddScoped<IWorlChatService, WorlChatService>();
builder.Services.AddScoped<IWorldChatRepository, WorldChatRepository>();


// Register AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); // Register the AutoMapperProfile

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("LiveChat"));
}, ServiceLifetime.Scoped);
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapHub<ChatHub>("/Chat");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
