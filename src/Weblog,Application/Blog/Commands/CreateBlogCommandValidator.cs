using FluentValidation;

namespace Weblog.Application.Blog.Commands;

public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty();

        RuleFor(command => command.Content)
            .NotEmpty();

        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.LastUpdated)
            .NotEmpty();
    }
}