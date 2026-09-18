using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Helper;
using Doan2.dal.interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình CORS để Frontend (Live Server, HTML) gọi được API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 2. Thêm Controllers
builder.Services.AddControllers();

// 3. Cấu hình Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 4. Đăng ký Dependency Injection theo kiến trúc 3 lớp
builder.Services.AddTransient<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IMovieBusiness, MovieBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();