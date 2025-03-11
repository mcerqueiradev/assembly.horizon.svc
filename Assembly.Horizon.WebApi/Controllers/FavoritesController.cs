using Assembly.Horizon.Application.Common.Responses;
using Assembly.Horizon.Application.CQ.Favorites.Commands.Create;
using Assembly.Horizon.Application.CQ.Favorites.Commands.Remove;
using Assembly.Horizon.Application.CQ.Favorites.Queries.isFavorite;
using Assembly.Horizon.Application.CQ.Favorites.Queries.Retrieve;
using Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByProperty;
using Assembly.Horizon.Application.CQ.Favorites.Queries.RetrieveByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Assembly.Horizon.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoritesController(ISender sender) : Controller
{
    [HttpPost("Register")]
    [ProducesResponseType(typeof(CreateFavoriteCommand), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CreateFavorites([FromBody] CreateFavoriteCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("{favoriteId}")]
    [ProducesResponseType(typeof(FavoriteResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(Guid favoriteId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveFavoriteQuery(favoriteId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpGet("property/{propertyId}")]
    [ProducesResponseType(typeof(IEnumerable<FavoriteResponse>), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> GetByPropertyId(Guid propertyId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RetrieveFavoritesByPropertyIdQuery(propertyId), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(RetrieveFavoritesByUserIdResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new RetrieveFavoritesByUserIdQuery { UserId = userId };
        var result = await sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }

    [HttpDelete("{favoriteId}")]
    [ProducesResponseType(typeof(RemoveFavoriteResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveFavorite(Guid favoriteId, CancellationToken cancellationToken)
    {
        var command = new RemoveFavoriteCommand(favoriteId);
        var result = await sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.IsSuccess);
        }

        return BadRequest(result.Error);
    }

    [HttpGet("check/{userId}/{propertyId}")]
    [ProducesResponseType(typeof(IsFavoriteResponse), 200)]
    [ProducesResponseType(typeof(Error), 400)]
    public async Task<IActionResult> CheckIsFavorite(Guid userId, Guid propertyId, CancellationToken cancellationToken)
    {
        var query = new IsFavoriteQuery(userId, propertyId);
        var result = await sender.Send(query, cancellationToken);
        return Ok(result.Value);
    }
}
