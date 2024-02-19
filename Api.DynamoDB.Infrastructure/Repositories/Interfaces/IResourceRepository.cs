﻿namespace Api.DynamoDB.Infrastructure.Repositories.Interfaces
{
	public interface IResourceRepository
	{
		Task DisableAsync(string pk, string sk);
	}
}
