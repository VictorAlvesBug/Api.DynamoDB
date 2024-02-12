using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/Data")]
	public class DataController : ControllerBase
	{
		private readonly ITableService _service;
		public DataController(ITableService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			return await _service.GetAllAsync();
		}

		[HttpGet("{tableName}")]
		public async Task<TableDescription> Get(string tableName)
		{
			return await _service.DescribeAsync(tableName);
		}

		[HttpPost("{tableName}")]
		public async Task Post(string tableName)
		{
			await _service.CreateAsync(tableName);
		}
	}
}