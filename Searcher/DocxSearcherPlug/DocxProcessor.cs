using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using SearcherExtensibility;

namespace DocxSearcherPlug
{
	[PluginMetadata("Docx document")]
    public class DocxProcessor : IFileProcessor
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
				using (var package = Package.Open(fileName, FileMode.Open, FileAccess.Read))
				{
					var xmlDoc = new XmlDocument();
					xmlDoc.Load(package.GetPart(new Uri("/word/document.xml", UriKind.Relative)).GetStream());
					var mgr = new XmlNamespaceManager(xmlDoc.NameTable);
					mgr.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

					var xmlNodeList = xmlDoc.SelectNodes("/descendant::w:t", mgr);
					return xmlNodeList != null && 
						(from XmlNode node in xmlNodeList select _regx.IsMatch(node.InnerText)).Any(result => result);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		public IEnumerable<string> FileExtentionPatterns
		{
			get { return new[] {"*.docx", "*.docm"}; }
		}
    }
}
