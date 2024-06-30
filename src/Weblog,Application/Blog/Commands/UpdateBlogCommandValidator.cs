using FluentValidation;

namespace Weblog.Application.Blog.Commands;

public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
{
    public UpdateBlogCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Title)
            .NotEmpty();

        RuleFor(command => command.Content)
            .NotEmpty();

        RuleFor(command => command.LastUpdated)
            .NotEmpty();
    }
}