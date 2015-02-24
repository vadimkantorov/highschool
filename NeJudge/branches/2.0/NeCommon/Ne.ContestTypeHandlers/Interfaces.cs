using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using Ne.Database.Classes;

namespace Ne.ContestTypeHandlers
{
	public interface IMonitorManager
	{
		DataTable[] Build(int contestID);
		void PaintDataGrid(DataGrid dg, int index, int solvedIndex);
	}

	public interface IStatusManager
	{
		string[] GetHeaders();
		string[] GetInfo(Submission s);
	}

	public interface ITestLogManager
	{
		DataGrid BuildTestLogGrid(Submission sm);
	}

	public abstract class OutcomeManager
	{
		protected Dictionary<string, string> outcomes = new Dictionary<string,string>();

		public const string TestingFailure = "Testing Failure";
		public const string CannotJudge = "Cannot Judge";
		public const string Waiting = "Waiting";
		public const string Compiling = "Compiling";
		public const string Running = "Running";
		public const string CompilationError = "Compilation Error";

		public string[] GetOutcomes()
		{
			string[] ret = new string[outcomes.Count];
			int i = 0;
			foreach ( string s in outcomes.Keys )
				ret[i++] = s;
			return ret;
		}

		public string GetPrintableValue(string value)
		{
			if ( !outcomes.ContainsKey(value) )
				return null;
			return outcomes[value];
		}

		public OutcomeManager()
		{ 
			outcomes[TestingFailure] = "Ошибка тестирования";
			outcomes[CannotJudge] = "Тестирование невозможно";
			outcomes[Waiting] = "Ожидание";
			outcomes[Compiling] = "Компилируется";
			outcomes[Running] = "Тестируется";
			outcomes[CompilationError] = "Ошибка компиляции";
		}
	}

	public interface ITesterManager
	{
		Ne.Tester.Tester CreateTester();
	}

	public abstract class ContestTypeHandler
	{
		protected IMonitorManager mm;
		protected IStatusManager sm;
		protected OutcomeManager om;
		protected ITesterManager tm;
		protected ITestLogManager itm;

		public IMonitorManager MonitorManager
		{
			get { return mm; }
		}

		public IStatusManager StatusManager
		{
			get { return sm; }
		}

		public OutcomeManager OutcomeManager
		{
			get { return om; }
		}

		public ITestLogManager TestLogManager
		{
			get { return itm; }
		}

		public ITesterManager TesterManager
		{
			get { return tm; }
		}
	}
}
