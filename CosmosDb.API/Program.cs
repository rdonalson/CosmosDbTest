using CosmosDb.Infrastructure.interfaces;
using CosmosDb.Infrastructure.repositories;
using Microsoft.Azure.Cosmos;

namespace CosmosDb.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			IConfiguration configuration = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
					.Build();

			builder.Services.AddSingleton(configuration);

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddSingleton<CosmosClient>(sp =>
			{
				var connectionString = builder.Configuration.GetConnectionString("CosmosDbConnectionString");
				return new CosmosClient(connectionString);
			});

			builder.Services.AddSingleton<INoteRepository>(sp =>
			{
				var cosmosClient = sp.GetRequiredService<CosmosClient>();
				var databaseName = builder.Configuration.GetValue<string>("CosmosDbDatabaseName");
				var containerName = builder.Configuration.GetValue<string>("CosmosDbContainerName");
				return new CosmosNoteRepository(cosmosClient, databaseName, containerName);
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
		}
	}
}



/* -----------------------------------------------
 * Archive
 
 		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
		}

 -----------------------------------------------*/