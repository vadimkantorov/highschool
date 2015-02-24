using Model;
using Rhino.Security.Interfaces;

namespace Web.Extensions
{
	public class NhUserSecurityInterceptor
	{
		public void OnCreated(User user)
		{
			var questionsAndAnswers = authz.CreateEntitiesGroup(EntityGroups.QuestionsAndAnswers(user));
			var submissions = authz.CreateEntitiesGroup(EntityGroups.Submissions(user));

			pbs.Allow(MessageOperation.View).For(user).On(questionsAndAnswers).DefaultLevel().Save();
			pbs.Allow(SubmissionOperation.View).For(user).On(submissions).DefaultLevel().Save();
		}

		public NhUserSecurityInterceptor(IPermissionsBuilderService pbs, IAuthorizationRepository authz)
		{
			this.pbs = pbs;
			this.authz = authz;
		}

		readonly IPermissionsBuilderService pbs;
		private readonly IAuthorizationRepository authz;
	}
}