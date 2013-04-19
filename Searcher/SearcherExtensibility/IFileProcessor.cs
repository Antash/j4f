﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace SearcherExtensibility
{
	public interface IFileProcessor
	{
		bool Init(string pat);
		bool ProcessFile(string fileName);
		IEnumerable<string> FileExtentionPatterns { get; }
	}

	public interface IPluginMetadata
	{
		string Name { get; }
	}

	[MetadataAttribute]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class PluginMetadataAttribute : ExportAttribute, IPluginMetadata
	{
		public PluginMetadataAttribute(string name)
			: base(typeof(IFileProcessor))
		{
			Name = name;
		}

		public string Name { get; private set; }
	}
}
