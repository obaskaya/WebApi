using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using WebApi.Data;


namespace WebApi.DBOperations
{
	public class DataGenerator
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BookStoreDbContext(
			serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			
			{
				// Look for any book.
				if (context.Books.Any())
				{
					return;   // Data was already seeded
				}
				context.Books.AddRange(
					new Book
					{
						//Id = 1,
						Title = "Lean Startup",
						GenreId = 1,
						PageCount = 200,
						PublishDate = new DateTime(2012, 09, 02)
					},
					new Book
					{
						//Id = 2,
						Title = "FbSpor",
						GenreId = 2,
						PageCount = 1907,
						PublishDate = new DateTime(1907, 07, 03)
					}
				);
				context.SaveChanges();
			}

		}
	}
}
