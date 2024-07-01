using System.Collections.Generic;
using Ardalis.Result;
using MediatR;
using Weblog.Query.QueriesModel;

namespace Weblog.Query.Application.Blog.Queries;

public class GetAllBlogQuery : IRequest<Result<IEnumerable<BlogQueryModel>>>;