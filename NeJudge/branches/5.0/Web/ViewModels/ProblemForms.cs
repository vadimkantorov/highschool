using System.Collections.Generic;
using System.Web;
using Model;
using Web.Extensions;

namespace Web.ViewModels
{
	public class ProblemForm
	{
		public string ShortName { get; set; }

		public string Name { get; set; }
	}
	
	public class EditProblemForm : ProblemForm
	{
		public Choose CheckerLanguages { get; set; }

		public string CheckerSource { get; set; }

		public string CheckerArguments { get; set; }

		public HttpPostedFileBase NewTestsArchive { get; set; }

		public int InsertNewTestsAt { get; set; }

		public int ProblemId { get; set; }

		public IEnumerable<Test> Tests { get; set; }

		public string ProblemBody { get; set; }

		public Choose DocumentFormatters { get; set; }

		public ResourceUsage Limits { get; set; }		
	}
	
	public class NewProblemForm : ProblemForm
	{
		public int ContestId { get; set; }
	}

	public class ViewProblemForm : ProblemForm
	{
		public ResourceUsage Limits { get; set; }

		public string RenderedBody { get; set; }
	}
}