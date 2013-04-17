using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SearcherExtensibility
{
	public enum PluginType
	{
		XmlTag,
		DotNetType
	}

	public interface IFileProcessor
	{
		bool IsSuitable(string fileName, string param);
		IEnumerable<string> FileExtentionPatterns { get; }
	}

	public interface IFileProcessorMetadata
	{
		PluginType ProcessorType { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class FileProcessorMetadataAttribute : ExportAttribute, IFileProcessorMetadata
	{
		public FileProcessorMetadataAttribute(PluginType type)
			: base(typeof(IFileProcessor))
		{
			ProcessorType = type;
		}

		public PluginType ProcessorType { get; set; }
	}
}
