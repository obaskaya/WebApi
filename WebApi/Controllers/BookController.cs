using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations;
using WebApi.Data;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBookCommand;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]s")]
	public class BookController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		public BookController(BookStoreDbContext context)
		{
			_context = context;
		}


		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(_context);
			var result = query.Handle();
			return Ok(result);

		}

		// get by id
		[HttpGet("{id}")]

		public Book GetById(int id)
		{
			var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
			return book;

		}

		// add book 
		[HttpPost]
		public IActionResult AddBook([FromBody] CreateBookModel newBook)
		{
			CreateBookCommand command = new CreateBookCommand(_context);
			try
			{
				command.Model = newBook;
				command.Handle();

			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
			return Ok();

			
		}

		// update book
		[HttpPut("{id}")]
		public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
		{
			var book = _context.Books.SingleOrDefault(x => x.Id == id);
			if (book is null)
			{
				return BadRequest();
			}
			book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
			book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
			book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
			book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

			_context.SaveChanges();
			return Ok();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{
			var book = _context.Books.SingleOrDefault(x => x.Id == id);
			if (book is null)
			{
				return BadRequest();
			}

			_context.Books.Remove(book);
			_context.SaveChanges();

			return Ok();
		}
	}
}
