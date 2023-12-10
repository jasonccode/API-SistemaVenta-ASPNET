
using APISistemaVenta.SistemaVenta.DAL.DbContext;
using APISistemaVenta.SistemaVenta.IOC;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion postgres
builder.Services.InyectarDependencias(builder.Configuration);
//builder.Services.AddNpgsql<DbventaContext>(builder.Configuration.GetConnectionString("Postgres"));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


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
