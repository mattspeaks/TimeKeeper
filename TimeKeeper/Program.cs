using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeKeeper.Interfaces;
using TimeKeeper.Models;
using TimeKeeper.Repositories;
using TimeKeeper.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddSignInManager<SignInManager<User>>();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//Add Repos
builder.Services.AddScoped<ILearningNoteRepository, LearningNoteRepository>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<ITimekeeperDayRepository, TimekeeperDayRepository>();
builder.Services.AddScoped<ITimekeeperEventRepository, TimekeeperEventRepository>();

//Add Services
builder.Services.AddScoped<ILearningNoteService, LearningNoteService>();
builder.Services.AddScoped<IToDoNoteService, ToDoNoteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimekeeperDayService, TimekeeperDayService>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();



