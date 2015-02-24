using Model;
using Rhino.Security.Interfaces;

namespace Web.Security
{
	public class NhParticipationApplicationSecurityInterceptor
	{
		public void OnIsApprovedChanged(ParticipationApplication application, bool oldIsApproved)
		{
			if(application.IsApproved == oldIsApproved)
				return;

			var contestParticipants = UserGroups.ContestParticipants(application.Contest);
			if(application.IsApproved)
				authz.AssociateUserWith(application.User, contestParticipants);
			else
				authz.DetachUserFromGroup(application.User, contestParticipants);
		}

		public NhParticipationApplicationSecurityInterceptor(IAuthorizationRepository authz)
		{
			this.authz = authz;
		}

		readonly IAuthorizationRepository authz;
	}
}