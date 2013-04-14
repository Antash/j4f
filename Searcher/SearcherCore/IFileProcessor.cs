using System;
using System.ComponentModel.Composition;

namespace SearcherCore
{
	public enum SearchType
	{
		File,
		XmlTag,
		DotNetType
	}

	public interface IFileProcessor
	{
		bool IsSuitable(string fileName, string param);
		string [] FileExtentionPatterns { get; }
	}

	public interface IFileProcessorMetadata
	{
		SearchType ProcessorType { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class FileProcessorMetadataAttribute : ExportAttribute, IFileProcessorMetadata
	{
		public FileProcessorMetadataAttribute(SearchType type)
			: base(typeof(IFileProcessor))
		{
			ProcessorType = type;
		}

		public SearchType ProcessorType { get; set; }
	}
}
