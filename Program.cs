using Microsoft.EntityFrameworkCore;
using Test_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Builder cho nhiều người có thể truy cập và Get API
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddDbContext<SinhVienContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Task3"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Data seeding
using (var scope = app.Services.CreateScope()) // Tạo một scope mới để quản lý vòng đời của các dịch vụ
{
    var services = scope.ServiceProvider;    // Lấy các dịch vụ đã được đăng ký
    var context = services.GetRequiredService<SinhVienContext>(); // Lấy DbContext
    SeedDB.Init(context);                     // Gọi hàm SeedDB.Init để thực hiện seeding
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
