using Kyrsova.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebappDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 17)))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

// **Добавление конфигурации аутентификации**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Путь к странице входа
    });

// Добавление сервисов в контейнер
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфигурация конвейера обработки HTTP-запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Значение HSTS по умолчанию составляет 30 дней. Возможно, вы захотите изменить его для производственных сценариев.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Использование аутентификации и авторизации**
app.UseAuthentication(); // Добавлено для использования аутентификации
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();