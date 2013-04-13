﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearcherCore
{
	//public interface ISearcher<out TResultType, in TParamType>
	//{
	//	IEnumerable<TResultType> Search(TParamType data);
	//}

	public enum SearcherType
	{
		FileSearcher,
		XmlSearcher,
		NetSearcher
	}

	public interface ISearcher
	{
		SearcherType GetSearcherType();
		void Search();
	}
}
