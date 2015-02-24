namespace Model
{
	public class Test : Entity
	{
		public Problem Problem { get; set; }

		public Blob Input { get; set; }

		public Blob Output { get; set; }

		public int Points { get; set; }

		public string Description { get; set; }

		public bool IsValid { get; set; }
	}
}