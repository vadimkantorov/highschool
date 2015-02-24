using Web.Extensions;

namespace Web.ViewModels
{
	public class NewSolutionForm
	{
		public int ProblemId { get; set; }

		public Choose Languages { get; set; }

		public string Code { get; set; }
	}
}