using TaskFlow.Extensions;
using DotNetEnv;
using TaskFlow.Extensions.Middleware;

var builder = WebApplication.CreateBuilder(args);
Env.Load();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfraSwagger();
builder.Services.AddDependeceInjection();
builder.Services.AddJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
