using System;
using System.Reflection;
using System.Collections.Generic;

using Ne.Configuration;

namespace Ne.ContestTypeHandlers
{
	public static class Factory
	{
		static Dictionary<string, ContestTypeHandler> dict;

		public static void Initialize(ContestTypeConfiguratorCollection col)
		{
			dict = new Dictionary<string, ContestTypeHandler>();
			foreach (ContestTypeConfigurator c in col)
			{
				//TODO: handle bad assemblies
				Assembly ass = Assembly.LoadFrom(c.AssemblyPath);
				Type prov = null;
				foreach (Type t in ass.GetTypes())
				{
					if (t.BaseType == typeof(ContestTypeHandler))
					{
						prov = t;
						break;
					}
				}
				dict.Add(c.ContestType, Activator.CreateInstance(prov) as ContestTypeHandler);
			}
		}

		public static ContestTypeHandler GetHandlerInstance(string contestType)
		{
			return dict[contestType];
		}
	}
}
