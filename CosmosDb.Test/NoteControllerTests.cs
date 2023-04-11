using CosmosDb.API.Controllers;
using CosmosDb.Data.model;
using CosmosDb.Infrastructure.interfaces;
using Moq;

namespace CosmosDb.Test
{
	[TestFixture]
	public class NoteControllerTests
	{
		private Mock<INoteRepository> _noteRepositoryMock;
		private NotesController _notesController;

		[SetUp]
		public void Setup()
		{
			_noteRepositoryMock = new Mock<INoteRepository>();
			_notesController = new NotesController(_noteRepositoryMock.Object);
		}

		[Test]
		public async Task GetNotes_ReturnsNotes()
		{
			// Arrange
			var notes = new List<Note>
		{
			new Note { Id = Guid.NewGuid().ToString(), Text = "Note 1", Tags = new List<string> { "Tag 1", "Tag 2" } },
			new Note { Id = Guid.NewGuid().ToString(), Text = "Note 2", Tags = new List<string> { "Tag 2", "Tag 3" } },
			new Note { Id = Guid.NewGuid().ToString(), Text = "Note 3", Tags = new List<string> { "Tag 3", "Tag 4" } }
		};
			_noteRepositoryMock.Setup(repo => repo.GetNotes()).ReturnsAsync(notes);

			// Act
			var result = await _notesController.GetNotes();

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<IEnumerable<Note>>(result);
			Assert.That(result.Count(), Is.EqualTo(3));
		}

		[Test]
		public async Task GetNoteById_ReturnsNote()
		{
			var id = Guid.NewGuid().ToString();
			// Arrange
			var note = new Note { Id = id, Text = "Note 1", Tags = new List<string> { "Tag 1", "Tag 2" } };
			_noteRepositoryMock.Setup(repo => repo.GetNoteById("1")).ReturnsAsync(note);

			// Act
			var result = await _notesController.GetNoteById("1");

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Note>(result);
			Assert.That(result.Id, Is.EqualTo(id));
		}

		[Test]
		public async Task CreateNote_ReturnsCreatedNote()
		{
			var id = Guid.NewGuid().ToString();
			// Arrange
			var noteToCreate = new Note { Text = "Note 1", Tags = new List<string> { "Tag 1", "Tag 2" } };
			var createdNote = new Note { Id = id, Text = "Note 1", Tags = new List<string> { "Tag 1", "Tag 2" } };
			_noteRepositoryMock.Setup(repo => repo.CreateNote(noteToCreate)).ReturnsAsync(createdNote);

			// Act
			var result = await _notesController.CreateNote(noteToCreate);

			// Assert
			Assert.IsNotNull(result);
			Assert.IsInstanceOf<Note>(result);
			Assert.That(result.Id, Is.EqualTo(id));
		}

		[Test]
		public async Task UpdateNote_UpdatesNote()
		{
			var id = Guid.NewGuid().ToString();
			// Arrange
			var noteToUpdate = new Note { Id = id, Text = "Note 1 updated", Tags = new List<string> { "Tag 1 updated", "Tag 2 updated" } };

			// Act
			await _notesController.UpdateNote("1", noteToUpdate);

			// Assert
			_noteRepositoryMock.Verify(repo => repo.UpdateNote("1", noteToUpdate), Times.Once());
		}

		[Test]
		public async Task DeleteNoteById_DeletesNote()
		{
			var id = Guid.NewGuid().ToString();
			// Act
			await _notesController.DeleteNoteById(id);

			// Assert
			_noteRepositoryMock.Verify(repo => repo.DeleteNoteById(id), Times.Once());
		}
	}
}
