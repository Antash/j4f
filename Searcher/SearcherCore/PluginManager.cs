using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SearcherCore
{
	class PluginManager
	{
		[ImportMany(typeof(IFileProcessor), AllowRecomposition = true)]
		private IEnumerable<Lazy<IFileProcessor, IFileProcessorMetadata>> Processors { get; set; }
		
		public PluginManager()
		{
			LoadPlugins();
		}

		private void LoadPlugins()
		{
			var catalog = new AggregateCatalog();
			var currentPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(PluginManager)).Location);
			if (currentPath != null)
			{
				catalog.Catalogs.Add(new DirectoryCatalog(currentPath));
			}
			var container = new CompositionContainer(catalog);
			try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.ToString());
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
