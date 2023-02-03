using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Data;
using WebApi.DBOperations;

namespace WebApi.BookOperations
{
	public class CreateBookCommand

	{
		public CreateBookModel Model { get; set; }
		private readonly BookStoreDbContext _dbContext;
		public CreateBookCommand(BookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Handle()
		{
			var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
			if (book is not null)
			{
				throw new InvalidOperationException("Book is already in database");
			}
			book = new Book();
			book.Title = Model.Title;
			book.PublishDate = Model.PublishDate;
			book.PageCount = Model.PageCount;
			book.GenreId = Model.GenreID;

			_dbContext.Books.Add(book);
			_dbContext.SaveChanges();
		}

		public class CreateBookModel
		{
			public string Title { get; set; }
			public int GenreID { get; set; }
			public int PageCount { get; set; }
			public DateTime PublishDate { get; set; }
		}
	}
}
