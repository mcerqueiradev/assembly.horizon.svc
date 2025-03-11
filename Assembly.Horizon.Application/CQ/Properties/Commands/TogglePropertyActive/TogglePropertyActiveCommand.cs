using Assembly.Horizon.Application.Common.Responses;
using MediatR;

namespace Assembly.Horizon.Application.CQ.Properties.Commands.TogglePropertyActive;

public record TogglePropertyActiveCommand(Guid PropertyId) : IRequest<Result<TogglePropertyActiveResponse, Success, Error>>;
