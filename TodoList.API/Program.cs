using Microsoft.EntityFrameworkCore;
using TodoList.API.DataAccess.Context;
using TodoList.API.Repositories.Implementations;
using TodoList.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddUserSecrets<Program>();

string? connectionString = builder.Configuration["AZURE_COSMOS_CONNECTIONSTRING"];
string? databaseName = builder.Configuration["DatabaseName"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseCosmos(connectionString: connectionString!, databaseName: databaseName!)
);


builder.Services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://todolist.runehille.com/")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
