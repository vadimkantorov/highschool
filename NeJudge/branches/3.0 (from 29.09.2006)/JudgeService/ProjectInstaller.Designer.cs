namespace Ne.Judge.Server
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if ( disposing && (components != null) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.srvProcInstNeJudge = new System.ServiceProcess.ServiceProcessInstaller();
			this.srvInstNeJudge = new System.ServiceProcess.ServiceInstaller();
			// 
			// srvProcInstNeJudge
			// 
			this.srvProcInstNeJudge.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.srvProcInstNeJudge.Password = null;
			this.srvProcInstNeJudge.Username = null;
			// 
			// srvInstNeJudge
			// 
			this.srvInstNeJudge.ServiceName = "NeJudge Server Service";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.srvProcInstNeJudge,
            this.srvInstNeJudge});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller srvProcInstNeJudge;
		private System.ServiceProcess.ServiceInstaller srvInstNeJudge;
	}
}