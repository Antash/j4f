using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using SearcherExtensibility;

namespace XMLSearcherPlug
{
	[PluginMetadataAttribute("Xml tag")]
	public class XmlProcessor : IFileProcessor
	{
		private Regex _regx;

		public bool Init(string pat)
		{
			try
			{
				_regx = new Regex(string.Format(@"<[^?!<>]{0}[\s>]", pat.Replace("*", @"\S*").Replace("?", @"\S?")),
					RegexOptions.IgnoreCase);
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
			var doc = new XmlDocument();
			try
			{
				doc.Load(fileName);
				return _regx.IsMatch(doc.InnerXml);
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
