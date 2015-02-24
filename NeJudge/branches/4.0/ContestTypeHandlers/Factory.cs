using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

//using log4net; TODO: logging issues

namespace Ne.ContestTypeHandlers
{
	public static class Factory
	{
		static Dictionary<string, Type> handlerTypes = new Dictionary<string, Type>();
		//static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static void Initialize()
		{
			object section = ConfigurationManager.GetSection("contestTypeHandlers");
			if ( section == null || !(section is HandlersConfigurationSection) )
				throw new ConfigurationErrorsException("Configuration file doesn't contain valid <contestTypeHandlers>" +
					" configuration section");

			DirectoryInfo baseDir = new DirectoryInfo(((HandlersConfigurationSection) section).HandlersDirectory);
			Assembly currentAssembly = null;

			foreach ( FileInfo fi in baseDir.GetFiles() )
				if ( fi.Extension == ".dll" )
				{
					try
					{
						currentAssembly = Assembly.LoadFrom(fi.FullName);
					}
					catch ( BadImageFormatException )
					{
						//logger.WarnFormat("Assembly image {0} is invalid - skipping", fi.FullName);
						continue;
					}

					foreach ( Type type in currentAssembly.GetTypes() )
						if ( type.BaseType == typeof(ContestTypeHandler) )
						{
							object[] typeAttrs = type.GetCustomAttributes(typeof(ContestTypeAttribute), true);

							if( typeAttrs.Length != 1 )
							{
								//logger.WarnFormat("Invalid contest type handler {0} - skipping", type);
							}
							else
							{
								string handledType = ( (ContestTypeAttribute)typeAttrs[0] ).Type;

								if( handlerTypes.ContainsKey(handledType) )
								{
									//logger.WarnFormat("Contest type {0} is already handled - skipping", handledType);
								}
								else
									handlerTypes.Add(handledType, type);
							}
							break;
						}
				}
		}

		public static ContestTypeHandler CreateContestHandler(string contestType, int contestID)
		{
			if ( !handlerTypes.ContainsKey(contestType) )
				throw new ArgumentException(string.Format("Contest type {0} is unhandled", contestType));

			Type handler = handlerTypes[contestType];
			try
			{	
				return (ContestTypeHandler) Activator.CreateInstance(handler, contestID);
			}
			catch ( Exception ex )
			{
				if( ex is MissingMethodException || ex is MemberAccessException )
				{
					//logger.ErrorFormat("Contest type handler {0} doesn't expose a public constructor with integer parameter", handler);
				}
				throw;
			}
		}
	}
}
