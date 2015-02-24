namespace Model.Testing
{
	public class RunResult
	{
		public ResourceUsage ResourceUsage { get; set; }
		public RunStatus Status { get; set; }
		public uint ExitCode { get; set; }

		public override string ToString()
		{
			return string.Format("Status = {0}, ResourceUsage = {{ {1} }}, ExitCode = {2}", Status, ResourceUsage, ExitCode);
		}
	}
}