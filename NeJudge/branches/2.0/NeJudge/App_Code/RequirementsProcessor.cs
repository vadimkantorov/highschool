using System;
using System.Web;

using Ne.Database.Classes;
using Ne.Judge;

namespace Ne.Helpers
{
	public class RequirementsProcessor
	{
		private Type _type;
		private HttpContext _context;
		private object[] _attr;

		private int _tid, _pid, _sid;
		private string _uid, _mode;
		private string outcome;

		public bool ProblemIDDefined
		{
			get { return _pid != -1; }
		}

		public bool SubmissionIDDefined
		{
			get { return _sid != -1; }
		}

		public bool ModeDefined
		{
			get { return _mode != null; }
		}

		public bool OutcomeDefined
		{
			get { return outcome != null; }
		}

		public bool ContestIDDefined
		{
			get { return _tid != -1; }
		}

		public bool UserIDDefined
		{
			get { return _uid != null; }
		}

		public int ProblemID
		{
			get { return _pid; }
		}

		public string Mode
		{
			get { return _mode; }
		}

		public string Outcome
		{
			get { return outcome; }
		}

		public int ContestID
		{
			get { return _tid; }
		}

		public string UserID
		{
			get { return _uid; }
		}

		public int SubmissionID
		{
			get { return _sid; }
		}

		public RequirementsProcessor(Type t)
		{
			_type = t;
			_context = HttpContext.Current;
			_pid = _tid = _sid = -1;
			_attr = null;
		}

		public void ProcessRequirements()
		{
			_attr = _type.GetCustomAttributes(true);
			foreach (Attribute a in _attr)
				if( a is RequestAttribute )
					ProcessAttribute(a as RequestAttribute, true);
		}
		//TODO: acceptNulls
		private void ProcessAttribute(RequestAttribute a, bool acceptNulls)
		{
			if( a is RequireModeAttribute )
			{
				RequireModeAttribute ra = a as RequireModeAttribute;
				if( _context.Request.QueryString["mode"] == ra.Mode )
				{
					foreach( RequestAttribute at in ra.Requirements )
						ProcessAttribute(at, false);
					_mode = ra.Mode;
				}
			}
			if (a is RequireContestIDAttribute)
			{
				if (_context.Request.QueryString["contestID"] != null)
				{
					if (!int.TryParse(_context.Request.QueryString["contestID"], out _tid) || !Contest.ValidateID(_tid))
					{
						_tid = -1;
						throw new NeJudgeInvalidParametersException("contestID");
					}
				}
				else if( !acceptNulls ) throw new NeJudgeInvalidParametersException("contestID");

			}
			else if (a is RequireProblemIDAttribute)
			{
				if (_context.Request.QueryString["problemID"] != null)
				{
					if (!int.TryParse(_context.Request.QueryString["problemID"], out _pid) || !Problem.ValidateID(_pid))
					{
						_pid = -1;
						throw new NeJudgeInvalidParametersException("problemID");
					}
				}
				else if( !acceptNulls ) throw new NeJudgeInvalidParametersException("problemID");
			}
			else if (a is RequireEveryProblemIDAttribute)
			{
				if (_context.Request.QueryString["problemID"] != null)
				{
					if (!int.TryParse(_context.Request.QueryString["problemID"], out _pid) || (_pid != 0 && !Problem.ValidateID(_pid)))
					{
						_pid = -1;
						throw new NeJudgeInvalidParametersException("problemID");
					}
				}
			}
			else if (a is RequireOutcomeAttribute)
			{
				if (_context.Request.QueryString["outcome"] != null)
				{
					try
					{
						outcome = _context.Request.QueryString["outcome"];
					}
					catch
					{
						outcome = null;
						throw new NeJudgeInvalidParametersException("outcome");
					}
				}
			}
			else if (a is RequireUserIDAttribute)
			{
				if (_context.Request.QueryString["userID"] != null)
				{
					if (!User.ValidateID(_context.Request.QueryString["userID"]))
					{
						_uid = null;
						throw new NeJudgeInvalidParametersException("userID");
					}
					_uid = _context.Request.QueryString["userID"];
				}
			}
			else if (a is RequireSubmissionIDAttribute)
			{
				if (_context.Request.QueryString["submissionID"] != null)
					if ( !int.TryParse(_context.Request.QueryString["submissionID"], out _sid) ||
						!Submission.ValidateID(_sid) )
					{
						_sid = -1;
						throw new NeJudgeInvalidParametersException("submissionID");
					}
			}
		}
	}
}