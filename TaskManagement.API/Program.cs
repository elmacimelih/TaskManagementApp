using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManagement.Application.Extensions;      // <-- Application AddApplication
using TaskManagement.Infrastructure.Extensions;
using TaskManagement.Infrastructure.Persistence;   // <-- Infrastructure AddInfrastructure

var builder = WebApplication.CreateBuilder(args);

// 1) DbContext (Infrastructure tarafından kullanılacak)
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Katmanları extension ile kaydet
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// 3) MVC & Swagger
builder.Services
    .AddControllers()
    .AddJsonOptions(opts =>
    {
        // Döngü oluşturan referansları ignore et
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        // Null değerleri gövdede atla (isteğe bağlı)
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// 4) Otomatik Migration
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}

// 5) HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
