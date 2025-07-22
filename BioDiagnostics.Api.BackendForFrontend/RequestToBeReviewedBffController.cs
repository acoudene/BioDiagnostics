// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

using Core.Api.BackendForFrontend;
using Core.Dtos;
using Core.ViewObjects;
using BioDiagnostics.Dtos;
using BioDiagnostics.Proxies;
using BioDiagnostics.ViewObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Net.Sockets;

namespace BioDiagnostics.Api.BackendForFrontend;

/// <summary>
/// API to interact with views through ViewObjects
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RequestToBeReviewedBffController : ControllerBase
{
  private readonly ILogger<RequestToBeReviewedBffController> _logger;
  private readonly RestBffBehavior<RequestToBeReviewedVo, RequestToBeReviewedDto, IRequestToBeReviewedClient> _behavior;
  
  /// <summary>
  /// Constructor
  /// </summary>
  /// <param name="logger"></param>
  /// <param name="client"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public RequestToBeReviewedBffController(
    ILogger<RequestToBeReviewedBffController> logger, 
    IRequestToBeReviewedClient client)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _behavior = new RestBffBehavior<RequestToBeReviewedVo, RequestToBeReviewedDto, IRequestToBeReviewedClient>(client);
  }

  protected virtual RequestToBeReviewedDto ToDto(RequestToBeReviewedVo vo)
    => vo.ToDto();

  protected virtual RequestToBeReviewedVo ToViewObject(RequestToBeReviewedDto dto)
    => dto.ToViewObject();

  /// <summary>
  /// Get all ViewObjects
  /// </summary>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<List<RequestToBeReviewedVo>>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _behavior.GetAllAsync(ToViewObject, cancellationToken);

  /// <summary>
  /// Get a ViewObject from its id
  /// </summary>
  /// <param name="id"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  [HttpGet("{id:guid}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<RequestToBeReviewedVo?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await _behavior.GetByIdAsync(id, ToViewObject, cancellationToken);  

  /// <summary>
  /// Create if needed and update an item through ViewObject
  /// </summary>
  /// <param name="newOrToUpdateVo"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  [HttpPost("CreateOrUpdate")]
  [Consumes(MediaTypeNames.Application.Json)]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<RequestToBeReviewedVo>> CreateOrUpdateAsync(
     [FromBody] RequestToBeReviewedVo newOrToUpdateVo,
     CancellationToken cancellationToken = default)
    => await _behavior.CreateOrUpdateAsync(newOrToUpdateVo, ToDto, cancellationToken);     

  /// <summary>
  /// Delete an item from its id
  /// </summary>
  /// <param name="id"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  [HttpDelete("{id:guid}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public virtual async Task<ActionResult<RequestToBeReviewedVo?>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    => await _behavior.DeleteAsync(id, ToViewObject, cancellationToken);
  
}
