using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using System.Runtime.Remoting;
using System.Text;

using log4net;

namespace Ne.Judge.Server
{
	public partial class Service : ServiceBase
	{
		static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public Service()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			log.Info("Starting NeJudgeServer");
			try
			{
				RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);
			}
			catch ( Exception ex )
			{
				log.Error("Error while configuring remoting services", ex);
				Environment.Exit(1);
			}
		}

		protected override void OnStop()
		{
			log.Info("Stopping NeJudgeServer");
		}
	}
}
