using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SearcherExtensibility;

namespace NETSearcherPlug
{
	[FileProcessorMetadataAttribute(PluginType.DotNetType)]
	public class NetAssemblyProcessor : IFileProcessor
	{
		public bool IsSuitable(string fileName, string param)
		{
			try
			{
				var assembly = Assembly.LoadFrom(fileName);
				// search only for classes or interfaces
				return assembly.GetTypes().Any(t => t.Name.Contains(param) && (t.IsClass || t.IsInterface));
			}
			catch (BadImageFormatException)
			{
				// not a valid .net assembly, skip
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				return false;
			}
		}

		public IEnumerable<string> FileExtentionPatterns
		{
			get { return new[] { "*.dll", "*.exe" }; }
		}
	}
}
