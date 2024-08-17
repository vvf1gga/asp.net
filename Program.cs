using Kyrsova.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebappDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 17)))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

// **���������� ������������ ��������������**
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // ���� � �������� �����
    });

// ���������� �������� � ���������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ������������ ��������� ��������� HTTP-��������
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // �������� HSTS �� ��������� ���������� 30 ����. ��������, �� �������� �������� ��� ��� ���������������� ���������.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **������������� �������������� � �����������**
app.UseAuthentication(); // ��������� ��� ������������� ��������������
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();