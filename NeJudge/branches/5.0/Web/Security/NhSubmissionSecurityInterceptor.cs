using Model;
using Rhino.Security.Interfaces;

namespace Web.Security
{
	public class NhSubmissionSecurityInterceptor
	{
		public void OnCreated(Submission submission)
		{
			authz.AssociateEntityWith(submission, EntityGroups.Submissions(submission.Problem.Contest));
			authz.AssociateEntityWith(submission, EntityGroups.Submissions(submission.Author));
		}

		public NhSubmissionSecurityInterceptor(IAuthorizationRepository authz)
		{
			this.authz = authz;
		}

		readonly IAuthorizationRepository authz;
	}
}