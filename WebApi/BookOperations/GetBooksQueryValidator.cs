using FluentValidation;

namespace WebApi.BookOperations
{
	public class GetBooksQueryValidator : AbstractValidator<GetBookDetailQuery>
	{
		public GetBooksQueryValidator()
		{
			RuleFor(query=> query.BookId).GreaterThan(0);
		}
	}
}
