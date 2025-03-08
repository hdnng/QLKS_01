using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using QuanLyKhachSan.Services;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ Entity Framework Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký các dịch vụ GenericFunction cho User và Customer
builder.Services.AddScoped<GenericFunction<User>>(); // GenericFunction cho User
builder.Services.AddScoped<GenericFunction<Customer>>(); // GenericFunction cho Customer
builder.Services.AddScoped<GenericFunction<Employee>>(); // GenericFunction cho Employee

// Đăng ký UserManagementService và CustomerManagementService vào DI container
builder.Services.AddScoped<UserManagementService>(); // Xử lý nghiệp vụ đăng ký User
builder.Services.AddScoped<CustomerManagementService>(); // Xử lý nghiệp vụ đăng ký Customer

// Thêm các dịch vụ MVC vào container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình đường dẫn HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Định nghĩa route mặc định cho AccountController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignUp}/{id?}");

// Chạy ứng dụng
app.Run();
