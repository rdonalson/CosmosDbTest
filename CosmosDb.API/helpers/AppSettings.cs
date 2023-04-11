namespace CosmosDb.API.helpers
{
	// Configuration class for appSettings.json
	public class AppSettings
	{
		public CosmosDbSettings? CosmosDbSettings { get; set; }
	}

	// Configuration class for CosmosDb settings
	public class CosmosDbSettings
	{
		public string? ConnectionString { get; set; }
		public string? DatabaseId { get; set; }
		public string? ContainerId { get; set; }
	}
}
