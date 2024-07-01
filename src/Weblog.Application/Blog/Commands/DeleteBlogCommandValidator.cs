using FluentValidation;

namespace Weblog.Application.Blog.Commands;

public class DeleteBlogCommandValidator : AbstractValidator<DeleteBlogCommand>
{
    public DeleteBlogCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}