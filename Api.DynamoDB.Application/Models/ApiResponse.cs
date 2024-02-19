namespace Api.DynamoDB.Application.Models
{
	public class ApiResponse
	{
		public bool Success { get; set; }
		public dynamic Data { get; set; }
		public string ErrorMessage { get; set; }

		public ApiResponse()
		{
			Success = true;
		}

		public ApiResponse(dynamic data)
		{
			Success = true;
			Data = data;
		}

		public ApiResponse(string errorMessage)
		{
			Success = false; 
			ErrorMessage = errorMessage;
		}
	}
}
