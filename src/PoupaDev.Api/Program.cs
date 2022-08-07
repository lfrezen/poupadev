using Microsoft.EntityFrameworkCore;
using PoupaDev.Api.Jobs;
using PoupaDev.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PoupaDevCs");
builder.Services.AddDbContext<PoupaDevContext>(o =>
    o.UseSqlServer(connectionString));

// builder.Services.AddDbContext<PoupaDevContext>(o =>
//     o.UseInMemoryDatabase("PoupaDev"));

builder.Services.AddHostedService<RendimentoAutomaticoJob>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
