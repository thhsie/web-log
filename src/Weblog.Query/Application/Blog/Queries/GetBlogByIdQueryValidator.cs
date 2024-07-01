using FluentValidation;

namespace Weblog.Query.Application.Blog.Queries;

public class GetBlogByIdQueryValidator : AbstractValidator<GetBlogByIdQuery>
{
    public GetBlogByIdQueryValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}