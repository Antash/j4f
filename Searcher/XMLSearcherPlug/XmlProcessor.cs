using System;
using System.Xml;
using SearcherCore;

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

		public string FileExtentionPattern
		{
			get { return "*.xml"; }
		}
    }
}
