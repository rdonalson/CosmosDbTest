namespace CosmosDb.Data.model
{
	public class Note
	{
		public Note() => DateCreated = DateTime.Today.ToString();
		public Note(DateTime dateCreated) => DateCreated = dateCreated.ToString();

		public string Id { get; set; }
		public string DateCreated { get; set; }
		public string? Text { get; set; }
		public string? Tags { get; set; }
	}
}
