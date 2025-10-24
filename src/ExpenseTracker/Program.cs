using ExpenseTracker.Application.DTO;
using ExpenseTracker.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettingDto>();
builder.Services.AddControllersWithViews();
builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddRepositoryServices();
builder.Services.AddAuthentication(builder.Configuration,appSettings);
builder.Services.AddApplicationServices();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();
