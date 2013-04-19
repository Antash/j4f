using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;

namespace SearcherExtensibility
{
	public class PluginManager
	{
		[ImportMany(typeof (IFileProcessor), AllowRecomposition = true)]
		private	IEnumerable<Lazy<IFileProcessor, IPluginMetadata>> _processors;

		public int LoadPlugins(string path)
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
			return _processors.Count();
		}

		public IEnumerable<string> GetPluginList()
		{
			return _processors != null ?
				from t in _processors.Select(p => p.Metadata.Name) select t :
				Enumerable.Empty<string>();
		}

		public IFileProcessor GetProcessor(string pName)
		{
			if (_processors == null)
				return null;

			var fProc = _processors.Where(p => p.Metadata.Name.Equals(pName)).Select(l => l.Value).FirstOrDefault();
			if (fProc == null)
				throw new DllNotFoundException(String.Format("Plugin {0} was not loaded!", pName));

			return fProc;
		}
	}
}
