using System;
using Weblog.Core.SharedKernel;

namespace Weblog.Application.Blog.Responses;

public record CreatedBlogResponse(Guid Id) : IResponse;