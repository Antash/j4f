using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using SearcherExtensibility;

namespace DocxSearcherPlug
{
	[PluginMetadata("Docx document")]
    public class DocxProcessor : IFileProcessor
    {
		private Regex _regx;

		public bool Init(string pat, bool isCaseSensitive)
		{
			IsCaseSensitive = isCaseSensitive;
			try
			{
				if (string.IsNullOrWhiteSpace(pat))
					pat = "*";
				_regx = new Regex(pat.Replace("*", @"\S*").Replace("?", @"\S?"),
					IsCaseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);
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
					var sb = new StringBuilder();
					if (xmlNodeList != null)
					{
						foreach (XmlNode node in xmlNodeList)
							sb.Append(node.InnerXml);
						return _regx.IsMatch(sb.ToString());
					}
					return false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return false;
			}
		}

		public bool IsCaseSensitive { get; private set; }

		public IEnumerable<string> FileExtentionPatterns
		{
			get { return new[] {"*.docx", "*.docm"}; }
		}
    }
}
