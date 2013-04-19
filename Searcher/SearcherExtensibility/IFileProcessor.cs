using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SearcherExtensibility
{
	public interface IFileProcessor
	{
		bool ProcessFile(string fileName, string param);
		IEnumerable<string> FileExtentionPatterns { get; }
	}

	public interface IPluginMetadata
	{
		string Name { get; }
		string Info { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class PluginMetadataAttribute : ExportAttribute, IPluginMetadata
	{
		public PluginMetadataAttribute(string name, string info)
			: base(typeof(IFileProcessor))
		{
			Name = name;
			Info = info;
		}

		public string Name { get; private set; }
		public string Info { get; private set; }
	}
}
