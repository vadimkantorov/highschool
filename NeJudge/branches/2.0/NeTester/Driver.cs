//#define CREATE_PROBLEM

using System;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;

using log4net;

using Ne.ContestTypeHandlers;
using Ne.Database.Classes;
using Ne.Configuration;
using Ne.Database.Interfaces;

namespace Ne.Tester
{
	internal class Driver
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static void Main(string[] args)
		{
			Configurator.InitNeTesterConfiguration();
			DataProvider.Initialize(Configurator.NeTesterConfiguration.ProviderPath,
				Configurator.NeTesterConfiguration.ConnectionString);
			Factory.Initialize(Configurator.NeTesterConfiguration.ContestTypeConfigurators);
#if CREATE_PROBLEM
			string probDir = @"C:\spb\ru-olymp-team-russia-2001-tests\sch\c";
			string testsDir = Path.Combine(probDir, "tests");

			for ( int i = 1; i <= 25; ++i )
			{
				Test t = new Test(38, i, "");
				t.Input = new BinaryReader(new FileStream(
					Path.Combine(testsDir,
					String.Format("{0:D2}", i)), FileMode.Open)).ReadBytes(15000);
				t.Output = new BinaryReader(new FileStream(
					Path.Combine(testsDir,
					String.Format("{0:D2}.a", i)), FileMode.Open)).ReadBytes(15000);
				t.Store();
			}

			Problem p = Problem.GetProblem(38);
			p.CheckerBytes = new BinaryReader(new FileStream(
					Path.Combine(probDir, "checkc.exe"), FileMode.Open)).ReadBytes(500000);
			p.Store();
			return;
#endif
			log.Info("Starting NeTester");
			try
			{
				NeTesterConfiguration.CheckConfig();
			}
			catch ( NeConfigurationException ex )
			{
				log.Error(ex.Message);
				Environment.Exit(1);
			}
			RemotingConfiguration.Configure("NeTester.exe.config", false);
			log.Info("Waiting for submissions...");
			while ( true ) 
				System.Threading.Thread.Sleep(2000);
			log.Info("Stopping NeTester");
		}
	}
}