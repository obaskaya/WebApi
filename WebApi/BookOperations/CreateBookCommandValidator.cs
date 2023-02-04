using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.BookOperations
{
	public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
	{
		public CreateBookCommandValidator()
		{
			RuleFor(command => command.Model.GenreID).GreaterThan(0);
			RuleFor(command => command.Model.PageCount).GreaterThan(0);
			RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
			RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
		}
	}
}
