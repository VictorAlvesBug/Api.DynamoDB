using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models;
using Api.DynamoDB.Application.Models.Resources;
using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/Resources")]
	public class ResourceController : ControllerBase
	{
		private readonly IResourceService _service;
		public ResourceController(IResourceService service)
		{
			_service = service;
		}

		/// <summary>
		/// It gets a string list of all my resources.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(new ApiResponse(await _service.GetAllAsync()));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		/// <summary>
		/// It gets a description of a single resource of mine.
		/// </summary>
		/// <returns></returns>
		[HttpGet($"{{{nameof(id)}}}")]
		public async Task<IActionResult> Get(string id)
		{
			try
			{
				return Ok(new ApiResponse(await _service.GetAsync(id)));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] ResourceToCreateModel payload)
		{
			try
			{
				return Ok(new ApiResponse(await _service.CreateAsync(payload)));
			}
			catch (ConditionalCheckFailedException ex)
			{
				return BadRequest(new ApiResponse(
					$"Já existe um recurso com Id '{payload.Id}' - {ex.Message}"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		[HttpPatch($"{{{nameof(id)}}}")]
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
		}
	}
}