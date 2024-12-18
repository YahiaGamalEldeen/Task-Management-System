using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TaskManagement;
using TaskManagement.core.Interfaces;
using TaskManagement.core.InterfaceServices;
using TaskManagement.EF;
using TaskManagement.services;
using static System.Net.Mime.MediaTypeNames;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// إعداد خدمات DbContext و Identity (كما في الخطوات السابقة)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// إعداد JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]); // مفتاح التشفير (غيّر النص لمفتاح قوي)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"], // قم بتحديد مصدر الإصدار
        ValidAudience = builder.Configuration["JWT:Audience"], // قم بتحديد الجمهور
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
//builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ITaskServices, TaskServices>();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddTransient<IProjectServices, ProjectServices>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {  
        Title = "Task Management",
        Description = "ASP.Net Core API",
        Contact = new OpenApiContact
        {
            Name = "Yahia GamaL",
            Email = "yhyyaldyn80@gmail.com",
        } });

    // Configure Bearer Token Authorization
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer abc123\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


/*builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management",
        Description = "ASP.Net Core API",
        Contact = new OpenApiContact
        {
            
            Name = "Yahia GamaL",
            Email = "yhyyaldyn80@gmail.com",
        }
    });

});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

