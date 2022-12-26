using API.Contexts;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Dependency
builder.Services.AddScoped<AccountRepositories>();
builder.Services.AddScoped<AccountRoleRepositories>();
builder.Services.AddScoped<EducationRepositories>();
builder.Services.AddScoped<EmployeeRepositories>();
builder.Services.AddScoped<ProfilingRepositories>();
builder.Services.AddScoped<RoleRepositories>();
builder.Services.AddScoped<UniversityRepositories>();

// Configure SQL Server Databases
builder.Services.AddDbContext<MyContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            //Usually, this is your application base URL
            //ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuer = false,
            //If the JWT is created using a web service, then this would be the consumer URL.
            //ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
