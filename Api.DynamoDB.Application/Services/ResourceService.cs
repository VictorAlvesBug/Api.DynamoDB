using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models.Resources;
using Api.DynamoDB.Application.Services.Interfaces;
using Api.DynamoDB.Domain.Database;
using Api.DynamoDB.Domain.Entities.Resources;
using Api.DynamoDB.Helpers.Extensions;
using Api.DynamoDB.Infrastructure.Repositories.Interfaces;

namespace Api.DynamoDB.Application.Services
{
	public class ResourceService : IResourceService
	{
		private readonly IRepository<ResourceEntity> _genericRepository;
		private readonly IResourceRepository _resourceRepository;
		private readonly string _fakeOwner;

		public ResourceService(
			IRepository<ResourceEntity> genericRepository,
			IResourceRepository resourceRepository)
		{
			_genericRepository = genericRepository;
			_resourceRepository = resourceRepository;
			_fakeOwner = "victoralvesbug";
		}

		public async Task<IEnumerable<ResourceItemToGetModel>> GetAllAsync()
		{
			var attributesToGet = ObjectExtensions.GetListOfAttributeNames<ResourceItemToGetModel>();

			var resources = await _genericRepository.GetAsync(
				Database.GetResourceSchemaPK(_fakeOwner), 
				attributesToGet);

			return resources.ConvertTo<ResourceItemToGetModel>();
		}

		public async Task<ResourceModel> GetAsync(string id)
		{
			var entity = await _genericRepository.GetSingleAsync(
					Database.GetResourceSchemaPK(_fakeOwner),
					Database.GetResourceSchemaSK(id));

			return entity.ConvertTo<ResourceModel>();
		}

		public async Task<ResourceModel> CreateAsync(ResourceToCreateModel resource)
		{
			var entity = resource.ToEntity(_fakeOwner);

			await _genericRepository.CreateItemAsync(entity);

			return entity.ConvertTo<ResourceModel>();
		}

		public async Task<ResourceModel> UpdateAsync(string id, ResourceToUpdateModel resource)
		{
			var entity = resource.ToEntity(_fakeOwner, id);

			var previewEntity = await _genericRepository.GetSingleAsync(entity.PK, entity.SK);

			if (previewEntity == null) 
				throw new ConditionalCheckFailedException("The conditional request failed");

			entity = previewEntity.MergeWith(entity);

			await _genericRepository.UpdateWholeItemAsync(entity);

			return entity.ConvertTo<ResourceModel>();
		}

		public async Task DisableAsync(string id)
		{
			var pk = Database.GetResourceSchemaPK(_fakeOwner);
			var sk = Database.GetResourceSchemaSK(id);

			await _resourceRepository.DisableAsync(pk, sk);
		}
	}
}
