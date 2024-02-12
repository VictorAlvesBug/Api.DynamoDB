using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DynamoDB.Application.Models
{
	public class ApiResponse<T>
	{
		public bool Success { get; set; }
		public T Data { get; set; }
		public string ErrorMessage { get; set; }

		public ApiResponse(T data)
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
