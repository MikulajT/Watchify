using BLL.Services;
using DAL.Models;
using DAL.Repository;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(builder.Configuration["ConnectionString"]);
});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<ITvShowRepository, TvShowRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ITmdbApiService, TmdbApiService>();
builder.Services.AddTransient<INotifierService, NotifierService>();
builder.Services.AddTransient<ITvShowService, TvShowService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions
{
    ProgressBar = true,
    TimeOut = 5000,
    PositionClass = ToastPositions.BottomRight
});
var app = builder.Build();

// Configure the HTTP request pipeline.
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
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard();
//RecurringJob.AddOrUpdate<NotifierService>(x => x.NotifyAllUsers(builder.Configuration["TmdbApiKey"]), "0 4 * * 5");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseNToastNotify();
app.MapRazorPages();

app.Run();
