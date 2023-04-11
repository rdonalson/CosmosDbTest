namespace CosmosDb.Data.model
{
	public class Note
	{
		public Note() => DateCreated = DateTime.Today;
		public Note(DateTime dateCreated)
		{
			DateCreated = dateCreated;
		}

		public Guid Id { get; set; }
		public DateTime DateCreated { get; set; }
		public string? Text { get; set; }
		public List<string>? Tags { get; set; }
	}
}
