using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations;
using WebApi.Data;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBookCommand;
using static WebApi.BookOperations.UpdateBookCommand;

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

		public IActionResult GetById(int id)
		{
			BookDetailViewModel result;
			try
			{
				GetBookDetailQuery query = new GetBookDetailQuery(_context);
				query.BookId = id;
				result = query.Handle();
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}

			return Ok(result);

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
		public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
		{
			try
			{
				UpdateBookCommand command = new UpdateBookCommand(_context);
				command.BookId = id;
				command.Model = updatedBook;
				command.Handle();
			}
			catch (Exception ex)
			{

				return BadRequest(ex.Message);
			}
			return Ok();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBook(int id)
		{
			try
			{
				DeleteBookCommand command = new DeleteBookCommand(_context);
				command.BookId = id;
				command.Handle();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return Ok();

		}
	}
}
