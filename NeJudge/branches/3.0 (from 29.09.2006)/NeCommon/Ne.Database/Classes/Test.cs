using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	public class Test
	{
		int problemID;
		int testNumber;
		string description;
		byte[] input;
		byte[] output;
		bool stored = false;
		int points;

		public Test(int problemID , int testNumber , string description , int points)
		{
			this.problemID = problemID;
			this.testNumber = testNumber;
			this.description = description;
			this.points = points;
			input = new byte[] { };
			output = new byte[] { };
		}

		public void MarkStored()
		{
			stored = true;
		}

		public Test(int problemID , int testNumber , string description)
			: this(problemID , testNumber , description , 0)
		{
		}

		public int Points
		{
			get { return points; }
			set { points = value; }
		}

		public int ProblemID
		{
			get { return problemID; }
			set { problemID = value; }
		}

		public int TestNumber
		{
			get { return testNumber; }
			set { testNumber = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public void LoadInput()
		{
			if ( stored )
				input = DataProvider.Provider.TestManager.GetInput(problemID , testNumber);
		}

		public void LoadOutput()
		{
			if ( stored )
				output = DataProvider.Provider.TestManager.GetOutput(problemID , testNumber);
		}

		public byte[] Input
		{
			get
			{
				//if(input == null)
				//	LoadInput();
				return input;
			}
			set { input = value; }
		}

		public byte[] Output
		{
			get
			{
				//if(output == null)
				//	LoadOutput();
				return output;
			}
			set { output = value; }
		}

		#region Database Access Members
		public static bool ValidateID(int problemID , int testNumber)
		{
			return DataProvider.Provider.TestManager.ValidateID(problemID , testNumber);
		}

		public static Test GetTest(int problemID , int testNumber)
		{
			return DataProvider.Provider.TestManager.GetTest(problemID , testNumber);
		}

		public static Test[] GetTests(int problemID)
		{
			return DataProvider.Provider.TestManager.GetTests(problemID);
		}

		public static void AddTest(Test c)
		{
			DataProvider.Provider.TestManager.AddTest(c);
		}

		public static void UpdateTest(Test c)
		{
			DataProvider.Provider.TestManager.UpdateTest(c);
		}

		public static void RemoveTest(int problemID , int testNumber)
		{
			DataProvider.Provider.TestManager.RemoveTest(problemID , testNumber);
		}

		public void Store()
		{
			if ( !stored )
			{
				DataProvider.Provider.TestManager.AddTest(this);
				stored = true;
			}
			else
				DataProvider.Provider.TestManager.UpdateTest(this);
		}
		#endregion
	}
}