using CosmosDb.Data.model;
using CosmosDb.Infrastructure.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDb.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly INoteRepository _noteRepository;

		public NotesController(INoteRepository noteRepository)
		{
			_noteRepository = noteRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<Note>> GetNotes()
		{
			return await _noteRepository.GetNotes();
		}

		[HttpGet("{id}")]
		public async Task<Note> GetNoteById(string id)
		{
			return await _noteRepository.GetNoteById(id);
		}

		[HttpPost]
		public async Task<Note> CreateNote([FromBody] Note note)
		{
			return await _noteRepository.CreateNote(note);
		}

		[HttpPut("{id}")]
		public async Task UpdateNote(string id, [FromBody] Note note)
		{
			await _noteRepository.UpdateNote(id, note);
		}

		[HttpDelete("{id}")]
		public async Task DeleteNoteById(string id)
		{
			await _noteRepository.DeleteNoteById(id);
		}
	}
}
