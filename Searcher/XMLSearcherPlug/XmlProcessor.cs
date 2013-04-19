using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using SearcherExtensibility;

namespace XMLSearcherPlug
{
	[PluginMetadataAttribute("Xml tag")]
	public class XmlProcessor : IFileProcessor
	{
		public bool ProcessFile(string fileName, string param)
		{
			var doc = new XmlDocument();
			try
			{
				doc.Load(fileName);
				return doc.GetElementsByTagName(param).Count > 0;
			}
			catch (XmlException)
			{
				// not a valid xml file, skip
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
			get { return new[] { "*.xml" }; }
		}
	}
}
