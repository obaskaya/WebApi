using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.BookOperations;
using WebApi.Common;
using WebApi.Data;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBookCommand;
using static WebApi.BookOperations.UpdateBookCommand;
using WebApi.BookOperations;
using FluentValidation;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]s")]
	public class BookController : ControllerBase
	{
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public BookController(BookStoreDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}


		[HttpGet]
		public IActionResult GetBooks()
		{
			GetBooksQuery query = new GetBooksQuery(_context, _mapper);
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
				GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
				query.BookId = id;
				GetBooksQueryValidator validator= new GetBooksQueryValidator();
				validator.ValidateAndThrow(query);
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
			CreateBookCommand command = new CreateBookCommand(_context, _mapper);
			try
			{
				command.Model = newBook;
				CreateBookCommandValidator validator = new CreateBookCommandValidator();
				
				validator.ValidateAndThrow(command);

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
				UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
				validator.ValidateAndThrow(command);
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
				DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
				validator.ValidateAndThrow(command);
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
