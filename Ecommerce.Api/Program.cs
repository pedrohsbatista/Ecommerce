using Dapper.FluentMap;
using Ecommerce.Domain.Config;
using Ecommerce.Domain.IRepository;
using Ecommerce.Domain.Services;
using Ecommerce.Repository.Maps;
using Ecommerce.Repository.Repositories;
using Ecommerce.RepositoryContrib.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();

FluentMapper.Initialize(config =>
{
    config.AddMap(new DepartamentoMap());
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
