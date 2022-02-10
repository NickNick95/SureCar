using SureCar.API.Infrastructure;
using SureCar.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

Registry.BuildServices(builder.Services, configuration);

IServiceProvider provider = builder.Services.BuildServiceProvider();
var databaseStartupService = provider.GetService<IDatabaseService>();
await databaseStartupService.PrepareDatabaseIfNotExists();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

//app.UseCors(x => x
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();