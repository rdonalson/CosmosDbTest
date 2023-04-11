using CosmosDb.Data.model;
using CosmosDb.Infrastructure.interfaces;
using Microsoft.Azure.Cosmos;

namespace CosmosDb.Infrastructure.repositories
{
	public class CosmosNoteRepository : INoteRepository
	{
		private readonly Container _container;

		public CosmosNoteRepository(CosmosClient client, string databaseName, string containerName)
		{
			_container = client.GetContainer(databaseName, containerName);
		}

		public async Task<IEnumerable<Note>> GetNotes()
		{
			var sqlQueryText = "SELECT * FROM c";
			var queryDefinition = new QueryDefinition(sqlQueryText);
			var queryResultSetIterator = _container.GetItemQueryIterator<Note>(queryDefinition);

			var notes = new List<Note>();
			while (queryResultSetIterator.HasMoreResults)
			{
				var currentResultSet = await queryResultSetIterator.ReadNextAsync();
				notes.AddRange(currentResultSet);
			}

			return notes;
		}
		public async Task<Note> GetNoteById(string id)
		{
			var note = await _container.ReadItemAsync<Note>(id, new PartitionKey(id));
			return note;
		}

		public async Task<Note> CreateNote(Note note)
		{
			note.Id = Guid.NewGuid();
			note.DateCreated = DateTime.UtcNow;

			var response = await _container.CreateItemAsync(note);
			return response.Resource;
		}

		public async Task UpdateNote(string id, Note note)
		{
			var existingNote = await _container.ReadItemAsync<Note>(id, new PartitionKey(id));
			existingNote.Resource.Text = note.Text;
			existingNote.Resource.Tags = note.Tags;

			await _container.ReplaceItemAsync(existingNote.Resource, id, new PartitionKey(id));
		}

		public async Task DeleteNoteById(string id)
		{
			await _container.DeleteItemAsync<Note>(id, new PartitionKey(id));
		}
	}
}
