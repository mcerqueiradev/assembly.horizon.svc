using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Property.Commands.Create;

public class CreatePropertyCommand : IRequest<Result<CreatePropertyResponse, Success, Error>>
{

}
