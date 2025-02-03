using System.Text;
using krkit_Backend.Data;
using krkit_Backend.Data.GenericRepository;
using krkit_Backend.Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsýný yapýyoruz
builder.Services.AddDbContext<KrKitDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository ve UnitOfWork bağımlılıklarını ekleyelim
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// JWT Authentication yapılandırmasını ekliyoruz
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Issuer adresini buraya yazın
            ValidAudience = builder.Configuration["Jwt:Audience"], // Audience adresini buraya yazın
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Anahtarınızı burada kullanın
        };
 
    });

// Diğer servisleri ekliyoruz
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



// Swagger ayarlarına Bearer token'ı ekliyoruz
// Swagger ayarlarına Bearer token'ı ekliyoruz
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Bearer YAZMA Direk YAZ"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



var app = builder.Build();

// HTTP isteði iþleme hattýný yapýlandýrýyoruz
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger ana URL üzerinde çalýþýr
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();
