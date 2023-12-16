using Clientes.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services
    .AddControllers()
    .AddValidationSetup();

// Application Setup
builder.Services.AddApplicationSetup();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
