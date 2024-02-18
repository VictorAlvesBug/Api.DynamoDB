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
	}
}
