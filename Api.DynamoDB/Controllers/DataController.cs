using Api.DynamoDB.Application.Models;
using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/NoDB")]
	public class DataController : ControllerBase
	{
		private readonly IDataService _service;
		public DataController(IDataService service)
		{
			_service = service;
		}

		[HttpGet($"{{{nameof(tableName)}}}/Data")]
		public async Task<IActionResult> Get(string tableName)
		{
			try
			{
				return Ok(new ApiResponse(await _service.GetAllAsync(tableName)));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse(ex.Message));
			}
		}

		/*[HttpGet("/{tableName}/Data/{id}")]
		public async Task<IActionResult> Get(string tableName, string id)
		{
			try
			{
				return Ok(new ApiResponse<object>(
					await _service.GetAsync(tableName, id)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpPost("/{tableName}/Data")]
		public async Task<IActionResult> Post(string tableName, [FromBody] object payload)
		{
			try
			{
				return Ok(new ApiResponse<object>(
					await _service.CreateAsync(tableName, payload)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpPatch("/{tableName}/Data/{id}")]
		public async Task<IActionResult> Patch(string tableName, string id, [FromBody] object payload)
		{
			try
			{
				return Ok(new ApiResponse<object>(
					await _service.UpdateAsync(tableName, id, payload)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpDelete("/{tableName}/Data/{id}")]
		public async Task<IActionResult> Delete(string tableName, string id)
		{
			try
			{
				return Ok(new ApiResponse<bool>(
					await _service.DeleteAsync(tableName, id)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}*/
	}
}