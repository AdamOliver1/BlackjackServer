using BlackjackServer.Extensions;
using BlackjackServer.Hnadlers.API;
using BlackjackServer.Hubs;
using BlackjackServer.Services;
using BlackjackServer.Services.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
         options.AddPolicy("AllowAll",
         builder =>
         {
             builder.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
         }));

//add handlers
builder.Services.AddSingleton<IGamesManager, GamesManager>();
builder.Services.AddScoped<IGameHandler, GameHandler>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseErrorHandlingMiddleware();

app.MapControllers();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<GameHub>("/GameHub");
});

app.Run();
