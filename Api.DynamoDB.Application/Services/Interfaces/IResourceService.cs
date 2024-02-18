using Api.DynamoDB.Application.Models.Resources;

namespace Api.DynamoDB.Application.Services.Interfaces
{
	public interface IResourceService
	{
		Task<IEnumerable<ResourceItemToGetModel>> GetAllAsync();
		Task<ResourceModel> GetAsync(string id);
		Task<ResourceModel> CreateAsync(ResourceToCreateModel resource);
		Task<ResourceModel> UpdateAsync(string id, ResourceToUpdateModel resource);
		Task DisableAsync(string id);
	}
}
