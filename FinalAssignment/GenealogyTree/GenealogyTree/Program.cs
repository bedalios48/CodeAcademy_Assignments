using GenealogyTree.Adapters;
using GenealogyTree.Domain.Interfaces;
using GenealogyTree.Domain.Interfaces.IRepositories;
using GenealogyTree.Domain.Services;
using GenealogyTree.Infrastructure.Data;
using GenealogyTree.Infrastructure.Repositories;
using GenealogyTree.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GenealogyTreeContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("GenealogyTreeConnectionString"));
});
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(RelativeProfile));
builder.Services.AddAutoMapper(typeof(ParentChildProfile));
builder.Services.AddAutoMapper(typeof(PersonProfile));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IParentChildRepository, ParentChildRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddTransient<IRelativeService, RelativeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});

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
