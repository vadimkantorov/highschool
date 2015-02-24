namespace Broker.Common.Messages.Broker
{
	public class UnpackTestInfo
	{
		public byte[] ZipArchive { get; set; }

		public int ProblemId { get; set; }

		public int InsertAt { get; set; }
	}
}