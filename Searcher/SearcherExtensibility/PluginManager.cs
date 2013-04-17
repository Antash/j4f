using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SearcherExtensibility
{
	public class PluginManager
	{
		[ImportMany(typeof(IFileProcessor), AllowRecomposition = true)]
		private IEnumerable<Lazy<IFileProcessor, IFileProcessorMetadata>> Processors { get; set; }
		
		public PluginManager()
		{
			LoadPlugins(Path.GetDirectoryName(Assembly.GetAssembly(typeof(PluginManager)).Location));
		}

		public void LoadPlugins(string path)
		{
			var catalog = new AggregateCatalog();
			if (path != null)
			{
				catalog.Catalogs.Add(new DirectoryCatalog(path));
			}
			var container = new CompositionContainer(catalog);
			try
			{
				container.ComposeParts(this);
			}
			catch (CompositionException ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}

		public IFileProcessor GetProcessor(SearchType type)
		{
			var fProc = Processors.Where(p => p.Metadata.ProcessorType.Equals(type)).Select(l => l.Value).FirstOrDefault();
			if (fProc == null)
				throw new DllNotFoundException(String.Format("Plugin for {0} was not loaded!", type));
			return fProc;
		}
	}
}
