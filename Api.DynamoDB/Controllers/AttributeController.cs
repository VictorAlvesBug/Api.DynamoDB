using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models;
using Api.DynamoDB.Application.Models.Attributes;
using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/Resources")]
	public class AttributeController : ControllerBase
	{
		private readonly IResourceService _service;
		public AttributeController(IResourceService service)
		{
			_service = service;
		}

		/// <summary>
		/// It gets the resource attributes list.
		/// </summary>
		/// <returns></returns>
		[HttpGet($"{{{nameof(resourceId)}}}/Attributes")]
		public async Task<IActionResult> Get(string resourceId)
		{
			try
			{
				return Ok(new ApiResponse(await _service.GetAttributesAsync(resourceId)));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		/// <summary>
		/// It creates an attribute with its configuration.
		/// </summary>
		/// <returns></returns>
		[HttpPost($"{{{nameof(resourceId)}}}/Attributes")]
		public async Task<IActionResult> Post(string resourceId, [FromBody] AttributeToCreateModel payload)
		{
			try
			{
				return Ok(new ApiResponse(await _service.CreateAttributeAsync(resourceId, payload)));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		/*[HttpPatch($"{{{nameof(id)}}}")]
		public async Task<IActionResult> Patch(string id, [FromBody] ResourceToUpdateModel payload)
		{
			try
			{
				return Ok(new ApiResponse(await _service.UpdateAsync(id, payload)));
			}
			catch (ConditionalCheckFailedException ex)
			{
				return BadRequest(new ApiResponse(
					$"O recurso de Id '{id}' não foi encontrado - {ex.Message}"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		[HttpDelete($"{{{nameof(id)}}}")]
		public async Task<IActionResult> Delete(string id)
		{
			try
			{
				await _service.DisableAsync(id);
				return Ok(new ApiResponse());
			}
			catch (ConditionalCheckFailedException ex)
			{
				return BadRequest(new ApiResponse(
					$"O recurso de Id '{id}' não foi encontrado - {ex.Message}"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}*/
	}
}