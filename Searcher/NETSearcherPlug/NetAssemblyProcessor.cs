using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SearcherExtensibility;

namespace NETSearcherPlug
{
	[PluginMetadataAttribute(".Net type")]
	public class NetAssemblyProcessor : IFileProcessor
	{
		private Regex _regx;

		public bool Init(string pat)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(pat))
					pat = "*";
				_regx = new Regex(pat.Replace("*", @"\S*").Replace("?", @"\S?"), RegexOptions.IgnoreCase);
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		public bool ProcessFile(string fileName)
		{
			try
			{
				var assembly = Assembly.LoadFrom(fileName);
				// search only for classes or interfaces
				return assembly.GetTypes().Any(t => _regx.IsMatch(t.Name) && (t.IsClass || t.IsInterface));
			}
			catch (BadImageFormatException)
			{
				// not a valid .net assembly, skip
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		public IEnumerable<string> FileExtentionPatterns
		{
			get { return new[] { "*.dll", "*.exe" }; }
		}
	}
}
