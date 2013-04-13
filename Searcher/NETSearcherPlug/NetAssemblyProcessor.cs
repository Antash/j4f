using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using SearcherCore;

namespace NETSearcherPlug
{
	[Export(typeof(IFileProcessor))]
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
    }
}
