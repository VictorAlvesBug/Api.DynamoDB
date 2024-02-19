using Amazon.DynamoDBv2.Model;
using Api.DynamoDB.Application.Models.Attributes;
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
		private readonly ITableRepository _tableRepository;
		private readonly string _fakeOwner;

		public ResourceService(
			IRepository<ResourceEntity> genericRepository,
			IResourceRepository resourceRepository,
			ITableRepository tableRepository)
		{
			_genericRepository = genericRepository;
			_resourceRepository = resourceRepository;
			_tableRepository = tableRepository;
			_fakeOwner = "victoralvesbug";
		}

		public async Task<IEnumerable<ResourceItemToGetModel>> GetAllAsync()
		{
			var attributesToGet = ObjectExtensions.GetListOfAttributeNames<ResourceItemToGetModel>();

			var resources = await _genericRepository.GetAsync(
				Database.GetResourceSchemaPK(_fakeOwner),
				attributesToGet);

			if (!resources.SafeAny()) throw new Exception("Recursos não encontrados");

			var model = resources.ConvertTo<ResourceItemToGetModel>().ToList();

			model.ForEach(item => item.Url = Database.GetResourceApiUrl(_fakeOwner, item.Id));

			return model;
		}

		public async Task<ResourceModel> GetAsync(string id)
		{
			var entity = await _genericRepository.GetSingleAsync(
					Database.GetResourceSchemaPK(_fakeOwner),
					Database.GetResourceSchemaSK(id));

			if (entity == null) throw new Exception("Recurso não encontrado");

			var model = entity.ConvertTo<ResourceModel>();

			model.Url = Database.GetResourceApiUrl(model.Owner, model.Id);

			return model;
		}

		public async Task<ResourceModel> CreateAsync(ResourceToCreateModel resource)
		{
			var entity = resource.ToEntity(_fakeOwner);

			await _genericRepository.CreateItemAsync(entity);

			var tableName = Database.GetTableNodbName(entity.Owner, entity.Id);
			await _tableRepository.CreateAsync(tableName, "Id", null);

			var model = entity.ConvertTo<ResourceModel>();

			model.Url = Database.GetResourceApiUrl(model.Owner, model.Id);

			return model;
		}

		public async Task<ResourceModel> UpdateAsync(string id, ResourceToUpdateModel resource)
		{
			var entity = resource.ToEntity(_fakeOwner, id);

			var previewEntity = await _genericRepository.GetSingleAsync(entity.PK, entity.SK);

			if (previewEntity == null)
				throw new ConditionalCheckFailedException("The conditional request failed");

			entity = previewEntity.MergeWith(entity);

			await _genericRepository.UpdateWholeItemAsync(entity);

			var model = entity.ConvertTo<ResourceModel>();

			model.Url = Database.GetResourceApiUrl(model.Owner, model.Id);

			return model;
		}

		public async Task DisableAsync(string id)
		{
			var pk = Database.GetResourceSchemaPK(_fakeOwner);
			var sk = Database.GetResourceSchemaSK(id);

			await _resourceRepository.DisableAsync(pk, sk);
		}

		public async Task<IEnumerable<AttributeModel>> GetAttributesAsync(string resourceId)
		{
			var entity = await _genericRepository.GetSingleAsync(
					Database.GetResourceSchemaPK(_fakeOwner),
					Database.GetResourceSchemaSK(resourceId));

			if (entity == null) throw new Exception("Recurso não encontrado");

			var model = entity.AttributesList.ConvertTo<AttributeModel>();

			return model;
		}

		public async Task<AttributeModel> CreateAttributeAsync(
			string resourceId,
			AttributeToCreateModel attribute)
		{
			var resourceEntity = await _genericRepository.GetSingleAsync(
				Database.GetResourceSchemaPK(_fakeOwner),
				Database.GetResourceSchemaSK(resourceId));

			if (resourceEntity == null)
				throw new ConditionalCheckFailedException("Recurso não encontrado");

			var attributeEntity = attribute.ConvertTo<AttributeEntity>();

			resourceEntity.AttributesList ??= new List<AttributeEntity>();

			if (resourceEntity.AttributesList.Any(attr => attr.Name == attribute.Name))
				throw new Exception($"Já existe um atributo com Name '{attribute.Name}'");

			resourceEntity.AttributesList.Add(attributeEntity);

			await _genericRepository.UpdateWholeItemAsync(resourceEntity);

			var model = resourceEntity.ConvertTo<ResourceModel>();

			model.Url = Database.GetResourceApiUrl(model.Owner, model.Id);

			return attributeEntity.ConvertTo<AttributeModel>();
		}
	}
}
