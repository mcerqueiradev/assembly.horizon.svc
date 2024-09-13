using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Users.Queries.Auth
{
    public class AuthUserQuery : IRequest<Result<AuthUserResponse, Success, Error>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
