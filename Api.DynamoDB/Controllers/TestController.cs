using Api.DynamoDB.Application.Models.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.IO;
using Api.DynamoDB.Application.Models;
using System.Text;
using Api.DynamoDB.Domain.Enums;
using Api.DynamoDB.Application.Models.Attributes;

namespace Api.DynamoDB.Controllers
{
	[ApiController]
	[Route("api/NoDB")]
	public class TestController : ControllerBase
	{
		private readonly IWebHostEnvironment _env;

		public TestController(IWebHostEnvironment env)
		{
			_env = env;
		}

		[HttpGet()]
		public async Task<IActionResult> Get()
		{
			try
			{
				/*var type = typeof(ApiResponse);

				var fileProjectDll = type.Module.Name.Replace(".dll", "");
				var solutionPath = _env.ContentRootPath.Substring(0, _env.ContentRootPath.TrimEnd('\\').LastIndexOf('\\'));
				var guid = Guid.NewGuid().ToString();
				var filePath = type.FullName.Replace(fileProjectDll, guid).Replace(".", "\\").Replace(guid, fileProjectDll) + ".cs";
				var fullPath = Path.Combine(solutionPath, filePath);

				if (!System.IO.File.Exists(fullPath))
				{
					return NotFound("Arquivo .cs não encontrado.");
				}

				string codigoFonte = System.IO.File.ReadAllText(fullPath);*/

				/*string codigoFonte = @"
// The class
class MyClass
{
  // Class members
  string color = ""red"";        // field
  int maxSpeed = 200;          // field
}
";*/

				var resource = new ResourceModel();
				string codigoFonte = GetSourceCode(resource);


				Assembly assembly = CompilarCodigo(codigoFonte.Replace("ApiResponse", "ApiResponseTest"));
				if (assembly != null)
				{
					Type tipo = assembly.GetType("MyClass");
					if (tipo != null)
					{
						dynamic instancia = Activator.CreateInstance(tipo);
						instancia.MinhaFuncao();
					}
				}


				return Ok(codigoFonte);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		public static Assembly CompilarCodigo(string codigoFonte)
		{
			SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codigoFonte);

			string assemblyName = Path.GetRandomFileName();
			MetadataReference[] references = new MetadataReference[]
			{
			MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
			MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
			};

			CSharpCompilation compilation = CSharpCompilation.Create(
				assemblyName,
				syntaxTrees: new[] { syntaxTree },
				references: references,
				options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

			using (MemoryStream ms = new MemoryStream())
			{
				EmitResult result = compilation.Emit(ms);

				if (!result.Success)
				{
					Console.WriteLine("Erros de compilação:");
					foreach (Diagnostic diag in result.Diagnostics)
					{
						Console.WriteLine($"{diag.Id}: {diag.GetMessage()}");
					}
					return null;
				}
				else
				{
					ms.Seek(0, SeekOrigin.Begin);
					return Assembly.Load(ms.ToArray());
				}
			}
		}

		public static string GetSourceCode(ResourceModel resource)
		{
			var sb = new StringBuilder();

			sb.AppendLine("class DynamicValidatorClass\n{");

			resource.AttributesList.ForEach(attribute =>
			{
				if(attribute.FillingTypeOnCreate == FieldFillingType.Required)
				{
				sb.AppendLine($"[System.ComponentModel.DataAnnotations.Required(ErrorMessage = \"Informe um valor válido para o campo '{attribute.Name}'\")]");
				}

				sb.AppendLine($"public {GetStringType(attribute)} {attribute.Name} {{ get; set; }}");

				sb.AppendLine();
			});

			sb.AppendLine("}");

			return sb.ToString();
		}

		public static string GetStringType(AttributeModel attribute)
		{
			return attribute.Type switch
			{
				AttributeType.List => $"System.Collections.Generic.List<{GetStringType(attribute.ListItemType)}>",
				_ => GetStringType(attribute.Type)
			};
		}

		public static string GetStringType(AttributeType attributeType)
		{
			return attributeType switch
			{
				AttributeType.Bool => "bool",
				AttributeType.Date => "DateTime",
				AttributeType.DateTime => "DateTime",
				AttributeType.Decimal => "decimal",
				AttributeType.Number => "int",
				AttributeType.String => "string",
				_ => throw new Exception($"Informe um '{nameof(AttributeType)}' válido")
			};
		}
	}
}