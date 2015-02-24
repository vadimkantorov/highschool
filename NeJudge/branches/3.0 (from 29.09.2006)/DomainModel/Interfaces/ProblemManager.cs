using System.Xml;

using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class ProblemManager
	{
		protected string _connectionString;

		public ProblemManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(int problemID);
		public abstract Problem GetProblem(int problemID);
		public abstract Problem[] GetProblems(int contestID);
		public abstract void AddProblem(Problem problem);
		public abstract void UpdateProblem(Problem problem);
		public abstract void RemoveProblem(int problemID);
		public abstract XmlDocument GetProblemXmlData(int problemID, string dataKey);
		public abstract byte[] GetCheckerBytes(int problemID);
	}
}