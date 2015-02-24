using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Ne.SubmissionsBroker
{
	[RunInstaller(true)]
	public partial class ServiceInstaller : Installer
	{
		public ServiceInstaller()
		{
			InitializeComponent();
		}
	}
}