using System;
using Weblog.Core.SharedKernel;

namespace Weblog.Application.User.Responses;

public record CreatedUserResponse(Guid Id) : IResponse;