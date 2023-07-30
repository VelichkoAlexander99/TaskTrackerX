using Microsoft.OpenApi.Models;
using TaskTrackerX.AuthApi.Data;
using TaskTrackerX.AuthApi.HostBuilders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "this is V1"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    ApplicationDbContext.EnsureRolesCreated(scope.ServiceProvider);
    ApplicationDbContext.EnsureUserCreated(scope.ServiceProvider);
}

app.Run();