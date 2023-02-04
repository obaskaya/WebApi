using FluentValidation;

namespace WebApi.BookOperations
{
	public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
	{
		public DeleteBookCommandValidator()
		{
			RuleFor(command =>command.BookId).GreaterThan(0);
		}
	}
}
