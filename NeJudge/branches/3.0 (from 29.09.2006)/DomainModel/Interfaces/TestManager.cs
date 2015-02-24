using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class TestManager
	{
		protected string _connectionString;

		public TestManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(int problemID, int testNumber);
		public abstract Test GetTest(int problemID, int testNumber);
		public abstract Test[] GetTests(int problemID);
		public abstract void AddTest(Test c);
		public abstract void UpdateTest(Test c);
		public abstract void RemoveTest(int problemID, int testNumber);
		public abstract byte[] GetInput(int problemID, int testNumber);
		public abstract byte[] GetOutput(int problemID, int testNumer);
	}
}