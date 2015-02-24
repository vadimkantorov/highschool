using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ne.Database.Classes;


namespace Ne.Judge
{
	public partial class SelectProblem : System.Web.UI.UserControl, ICallbackEventHandler
	{
		#region Fields & Properties
		int problemID, contestID;
		string everyproblemname;
		protected string prob, cont;

		bool past, current, forthcoming;
		ContestTime everytype;
		protected string fcalling = "";

		public string EveryContestTime
		{
			set { everytype = (ContestTime)Enum.Parse(typeof(ContestTime), value); }
		}

		public bool Past
		{
			set { past = value; }
		}

		public void ContestChangedEventHandler(string script, string atstr)
		{
			Page.ClientScript.RegisterClientScriptBlock(typeof(SelectProblem), new Random().Next().ToString(), script,true);
			fcalling = atstr;
		}

		public bool Forthcoming
		{
			set { forthcoming = value; }
		}

		public bool Current
		{
			set { current = value; }
		}

		public int ProblemID
		{
			get
			{
				if (string.IsNullOrEmpty(Request.Form[problemsDDL.UniqueID]))
					return problemID;
				return Convert.ToInt32(Request.Form[problemsDDL.UniqueID]);//TODO:ValidateID
			}
			set { problemID = value; if (problemID != 0) contestID = Problem.GetProblem(problemID).ContestID; }
		}

		public int ContestID
		{
			get
			{
				if (contestsDDL.SelectedValue == "")
					return contestID;
				return int.Parse(contestsDDL.SelectedValue.Split(':')[0]);//TODO:ValidateID 
			}
			set { contestID = value; }
		}

		public string SpecialProblemName
		{
			set { everyproblemname = value; }
		}
		#endregion
		#region ViewState Managing
		protected override object SaveViewState()
		{
			return new object[] { base.SaveViewState(), cont, prob, contestID, problemID };
		}
		protected override void LoadViewState(object s)
		{
			object[] arr = (object[])s;
			base.LoadViewState(arr[0]);
			cont = (string)arr[1];
			prob = (string)arr[2];
			contestID = (int)arr[3];
			problemID = (int)arr[4];
		}
		#endregion
		protected void Page_Load(object sender, EventArgs e)
		{
			//TODO: Test !IsPostBack - unsuccessful
			if (!IsPostBack)
			{
				cont = "document.getElementById('" + contestsDDL.ClientID + "').options[0].value"; //contestID == 0 && problemID == 0
				prob = "0";

				if (contestID != 0)
				{
					cont = String.Format("{0}:{1}" , contestID , Contest.GetContest(contestID).Time);
					prob = problemID.ToString();
				}

				ContestTime type = ContestTime.None;
				if (current)
					type |= ContestTime.Current;
				if (past)
					type |= ContestTime.Past;
				if (forthcoming)
					type |= ContestTime.Forthcoming;

				foreach (Contest c in Contest.GetContests(type))
					contestsDDL.Items.Add(new ListItem(c.Name , String.Format("{0}:{1}" , c.ID , c.Time)));

				if (contestID != 0)
					contestsDDL.SelectedValue = cont;
			}
			if( contestsDDL.Items.Count != 0 )
			{
				Page.ClientScript.RegisterClientScriptBlock(GetType(), "selc", "function SelectedContestChanged(arg, context){" +
					Page.ClientScript.GetCallbackEventReference(this, "arg", "ProcessCallbackResult", "context",true) + ";" + fcalling + ";}", true);
				string script = "SelectedContestChanged(";
				if( contestID != 0 )
					script += "\"" + cont + "\",'ddl');";
				else
					script += cont + ",'ddl');";

				Page.ClientScript.RegisterStartupScript(GetType(), "initproblemsDDL", script, true);
				contestsDDL.Attributes.Add("onchange", "javascript:SelectedContestChanged(this.options[this.selectedIndex].value,'ddl');");
			}
			else
				Page.ClientScript.RegisterStartupScript(GetType(), "check", "CheckContestLack()", true);
		}


		#region ICallbackEventHandler Members
		string result;

		public string GetCallbackResult()
		{
			return result;
		}

		public void RaiseCallbackEvent(string eventArgument)
		{
			result = "";
			try
			{
				string[] tmp = eventArgument.Split(':');
				if (tmp.Length == 2)
				{
					int contestID;
					if (int.TryParse(tmp[0], out contestID))
					{
						if (!string.IsNullOrEmpty(everyproblemname))
						{
							ContestTime c = (ContestTime)Enum.Parse(typeof(ContestTime), tmp[1]);
							if ((c & everytype) != 0)
								result = string.Format("{0}^{1}|", everyproblemname, 0);
						}
						foreach (Problem p in Problem.GetProblems(contestID))
							result += string.Format("Задача {0}. {1}^{2}|", p.ShortName, p.Name, p.ID);
					}
					else
						result = "Ошибка в обработке Callback^1";
				}
			}
			catch { result = "Ошибка в обработке Callback^1"; }
		}

		#endregion
	}
}
