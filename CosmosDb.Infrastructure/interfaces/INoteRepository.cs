using CosmosDb.Data.model;

namespace CosmosDb.Infrastructure.interfaces
{
	public interface INoteRepository
	{
		Task<IEnumerable<Note>> GetNotes();
		Task<Note> GetNoteById(string id);
		Task<Note> CreateNote(Note note);
		Task UpdateNote(string id, Note note);
		Task DeleteNoteById(string id);
	}
}

