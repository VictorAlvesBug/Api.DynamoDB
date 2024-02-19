using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/Tables")]
	public class IndexController : ControllerBase
	{
		private readonly IIndexService _service;
		public IndexController(IIndexService service)
		{
			_service = service;
		}

		/*[HttpGet("/{tableName}/Indexes")]
		public async Task<IActionResult> Get(string tableName)
		{
			try
			{
				return Ok(new ApiResponse<TableIndexesModel>(
					await _service.GetAllAsync(tableName)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}*/

		/*[HttpGet("{tableName}/Indexes/")]
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
		}*/

		/*[HttpPost("{tableName}/Indexes/{indexName}")]
		public async Task<IActionResult> Post(
			string tableName, 
			string indexName, 
			IndexToCreateModel indexToCreateModel)
		{
			try
			{
				indexToCreateModel.TableName = tableName;
				indexToCreateModel.IndexName = indexName;

				return Ok(new ApiResponse<TableDescription>(
					await _service.CreateAsync(indexToCreateModel)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}

		[HttpDelete("{tableName}/Indexes/{indexName}")]
		public async Task<IActionResult> Delete(
			string tableName, 
			string indexName, 
			[FromBody] IndexToDeleteModel indexToDeleteModel)
		{
			try
			{
				indexToDeleteModel.TableName = tableName;
				indexToDeleteModel.IndexName = indexName;

				return Ok(new ApiResponse<TableDescription>(
					await _service.DeleteAsync(indexToDeleteModel)
				));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<object>(ex.Message));
			}
		}*/
	}
}