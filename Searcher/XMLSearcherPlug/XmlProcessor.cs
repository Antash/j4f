using System;
using System.Collections.Generic;
using System.Xml;
using SearcherExtensibility;

namespace XMLSearcherPlug
{
	[FileProcessorMetadataAttribute(SearchType.XmlTag)]
	public class XmlProcessor : IFileProcessor
    {
		public bool IsSuitable(string fileName, string param)
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
				Console.WriteLine(ex.ToString());
				return false;
			}
		}

		public IEnumerable<string> FileExtentionPatterns
		{
			get { return new [] {"*.xml"}; }
		}
    }
}
