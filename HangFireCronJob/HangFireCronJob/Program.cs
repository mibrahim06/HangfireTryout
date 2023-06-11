using Hangfire;
using HangFireCronJob.HangFireJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var hangFireConnectionString =
    "Server=localhost,1433;TrustServerCertificate=True;Database=HangFireDb;User Id=sa;Password=TestPassword*";

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(hangFireConnectionString);
    RecurringJob.AddOrUpdate<DbJob>(job => job.Run(), Cron.Minutely);
});

builder.Services.AddHangfireServer();

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

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();