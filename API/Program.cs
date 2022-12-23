using API.Contexts;
using API.Repositories.Data;
using Microsoft.EntityFrameworkCore;

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
