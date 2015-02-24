using System;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web;
namespace Ne.Judge
{
	public struct Limits
	{
		public uint Memory;
		public float Time;
		public uint Output;

		public Limits(float time, uint mem, uint output)
		{
			Memory = mem;
			Time = time;
			Output = output;
		}
	}

	public class HtmlFunctions
	{
		public static void BeautifyDataGrid(DataGrid dt)
		{
			Color blue = Color.FromArgb(120, 200, 255);
			Color gray = Color.FromArgb(192, 192, 192);
			if (dt.Items.Count != 0)
				dt.Items[0].CssClass = "grid_first";
			for (int i = 1; i < dt.Items.Count; i++)
				dt.Items[i].CssClass = (dt.Items[i - 1].CssClass == "grid_first")
					? "grid_second" : "grid_first";
		}

		public static string BeautifyTimeSpan(TimeSpan t, bool short_days)
		{
			string str = "";
			if ( t > new TimeSpan(0, 0, 1, 0, 0) )
			{
				if ((int) t.TotalDays > 0)
				{
					str += ((int) t.TotalDays).ToString();
					str += (short_days) ? " д " : " дней ";
				}
				str += ((int) t.Hours).ToString("D2");
				str += ":";
				str += ((int) t.Minutes).ToString("D2");
			}
			else
			{
				str = String.Format("{0} сек", t.Seconds);
			}
			return str;
		}
	}

	public class TimeUtils
	{
		public static DateTime ZeroDateTime(DateTime t)
		{
			return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0, 0);
		}

		public static TimeSpan ZeroTimeSpan(TimeSpan t)
		{
			return new TimeSpan(t.Days, t.Hours, t.Minutes, 0, 0);
		}
	}

	public class RequirementsProcessor
	{
		private Type _type;
		private HttpContext _context;
		private object[] _attr;

		private int _tid, _pid, _uid;

		public bool PidDefined
		{
			get { return _pid != -1; }
		}

		public bool TidDefined
		{
			get { return _tid != -1; }
		}

		public bool UidDefined
		{
			get { return _uid != -1; }
		}

		public int ProblemID
		{
			get { return _pid; }
		}

		public int ContestID
		{
			get { return _tid; }
		}

		public int UserID
		{
			get { return _uid; }
		}

		public RequirementsProcessor(Type t, HttpContext con)
		{
			_type = t;
			_context = con;
			_pid = _tid = _uid = -1;
			_attr = null;
		}

		public void ProcessRequirements()
		{
			_attr = _type.GetCustomAttributes(true);
			foreach( Attribute a in _attr )
			{
				RequireModeAttribute ra = a as RequireModeAttribute;
				if ( ra != null )
				{
					foreach( string s in ra.Modes.Keys )
						if( _context.Request.QueryString["mode"] == s )
							ProcessAttribute(ra.Modes[s] as Attribute);
				}
				else
					ProcessAttribute(a);
			}
		}

		private void ProcessAttribute(Attribute a)
		{
			Ne.Database.BaseDb db = Ne.Database.DbFactory.ConstructDatabase();
			if( a is RequireContestIdAttribute )
			{
				if ( _context.Request.QueryString["tid"] != null )
				{
					bool ok = true;
					try
					{
						_tid = int.Parse(_context.Request.QueryString["tid"]);
					}
					catch
					{
						ok = false;
					}
					if ( !ok || !db.CheckTid(_tid) )
						throw new NeJudgeInvalidParametersException("tid");
				}	
			}
			else if( a is RequireProblemIdAttribute )
			{
				if ( _context.Request.QueryString["pid"] != null )
				{
					bool ok = true;
					try
					{
						_pid = int.Parse(_context.Request.QueryString["pid"]);
					}
					catch
					{
						ok = false;
					}
					if ( !ok || !db.CheckPid(_pid) )
						throw new NeJudgeInvalidParametersException("pid");
				}
			}
				/*else if(at is RequireQuestionIdAttribute)
				{
					int qid;
					try
					{
						qid = int.Parse(context.Request.QueryString["qid"]);
					}
					catch
					{
					
						qid = -1;
					}
					if(!db.CheckQid(qid))
						throw new NeJudgeInvalidParametersException("qid");
				}*/
			else if( a is RequireUserIdAttribute )
			{
				if ( _context.Request.QueryString["uid"] != null )
				{
					bool ok = true;
					try
					{
						_uid = int.Parse(_context.Request.QueryString["uid"]);
					}
					catch
					{
						ok = false;
					}
					if ( !ok || !db.CheckUid(_uid) )
						throw new NeJudgeInvalidParametersException("uid");
				}
			}
			/*else
				{
					throw new NotSupportedException("Attribute " + at.TypeId + " is not supported");
				}*/
			db.Close();
		}
	}
	
	/*public class Utils
	{
		public static void ProcessRequirements(Type t, HttpContext context)
		{
			object[] attr = t.GetCustomAttributes(true);
			foreach(object o in attr)
			{
				if(o is RequireModeAttribute)
				{
					RequireModeAttribute a = o as RequireModeAttribute;
					foreach(string s in a.Modes.Keys)
					{
						if(context.Request.QueryString["mode"] == s)
						{
							ProcessAttributes(a.Modes[s] as NeJudgeRequestAttribute[],context);
							break;
						}
					}
					break;
				}
			}
			ProcessAttributes(attr,context);
		}
		
		private static void ProcessAttributes(object[] attr, HttpContext context)
		{
			Ne.Database.BaseDb db = Ne.Database.DbFactory.ConstructDatabase();
			foreach(Attribute at in attr)
			{
				if(at is RequireContestIdAttribute)
				{
					int tid;
					try
					{
						tid = int.Parse(context.Request.QueryString["TID"]);
					}
					catch
					{
					
						tid = -1;
					}
					if(!db.CheckTid(tid))
						throw new NeJudgeInvalidParametersException("tid");
				}
				else if(at is RequireProblemIdAttribute)
				{
					int pid;
					try
					{
						pid = int.Parse(context.Request.QueryString["PID"]);
					}
					catch
					{
					
						pid = -1;
					}
					if(!db.CheckPid(pid))
						throw new NeJudgeInvalidParametersException("pid");
				}
				else if(at is RequireQuestionIdAttribute)
				{
					int qid;
					try
					{
						qid = int.Parse(context.Request.QueryString["qid"]);
					}
					catch
					{
					
						qid = -1;
					}
					if(!db.CheckQid(qid))
						throw new NeJudgeInvalidParametersException("qid");
				}
				else if(at is RequireUserIdAttribute)
				{
					int uid;
					try
					{
						uid = int.Parse(context.Request.QueryString["UID"]);
					}
					catch
					{
					
						uid = -1;
					}
					if(!db.CheckUid(uid))
						throw new NeJudgeInvalidParametersException("uid");
				}
				else
				{
					throw new NotSupportedException("Attribute " + at.TypeId + " is not supported");
				}
			}
			db.Close();
		}
	}*/

}