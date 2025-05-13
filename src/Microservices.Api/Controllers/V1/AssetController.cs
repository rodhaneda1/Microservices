using MediatR;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microservices.Application.Querys;

namespace Microservices.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator)
            =>_mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAssetValueAsync([FromQuery] GetAssetValueRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}