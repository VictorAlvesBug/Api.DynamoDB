using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/Tables")]
	public class TableController : ControllerBase
	{
		private readonly ITableService _service;
		public TableController(ITableService service)
		{
			_service = service;
		}

		/*[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				return Ok(new ApiResponse<IEnumerable<string>>(
					await _service.GetAllAsync()
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpGet("{tableName}")]
		public async Task<IActionResult> Get(string tableName)
		{
			try
			{
				return Ok(new ApiResponse<TableDescription>(
					await _service.DescribeAsync(tableName)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpPost("{tableName}")]
		public async Task<IActionResult> Post(string tableName)
		{
			try
			{
				return Ok(new ApiResponse<TableDescription>(
					await _service.CreateAsync(tableName)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}*/
	}
}