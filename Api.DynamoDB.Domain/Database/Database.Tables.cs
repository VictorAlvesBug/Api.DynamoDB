namespace Api.DynamoDB.Domain.Database
{
	public static partial class Database
	{

		/*
			Tabela de Recursos (nodb-resource)
		*/

		private static string RESOURCE_SCHEMA_PK_PREFIX = "owner";
		private static string RESOURCE_SCHEMA_SK_PREFIX = "resource";
		
		public static string GetResourceSchemaPK(string owner = null) => 
			Hyphenize(RESOURCE_SCHEMA_PK_PREFIX, owner);

		public static string GetResourceSchemaSK(string resourceId = null) =>
			Hyphenize(RESOURCE_SCHEMA_SK_PREFIX, resourceId);

		/*
			Tabelas para cada um dos recursos (nodb-{owner}-{resourceId})
		*/

		private static string TABLE_NODB_PREFIX = "nodb";

		public static string GetTableNodbName(string owner = null, string resourceId = null) =>
			Hyphenize(TABLE_NODB_PREFIX, owner, resourceId);

		/*
			Url de um recurso ({API_BASE_URL}/{TABLE_NAME}/{DATA_PATH_URL})
		*/

		public static string API_BASE_URL = "https://localhost:7210/api/NoDB";
		private static string DATA_PATH_URL = "Data";

		public static string GetResourceApiUrl(string owner = null, string resourceId = null) =>
			BuildPath(API_BASE_URL, GetTableNodbName(owner, resourceId), DATA_PATH_URL);
	}
}
