using System;
using System.Linq;
using System.Reflection;
using SearcherCore;

namespace NETSearcherPlug
{
	[FileProcessorMetadataAttribute(SearchType.DotNetType)]
	public class NetAssemblyProcessor : IFileProcessor
    {
		public bool IsSuitable(string fileName, string param)
		{
			try
			{
				var assembly = Assembly.LoadFrom(fileName);
				return assembly.GetTypes().Any(t => t.Name.Contains(param));
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

		public string FileExtentionPattern
		{
			get { return "*.dll|*.exe"; }
		}
    }
}
