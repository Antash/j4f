using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace SearcherCore
{
	class PluginManager
	{
		[ImportMany]
		public IEnumerable<IFileProcessor> Processors { get; set; }

		public void LoadPlugins()
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
	}
}
