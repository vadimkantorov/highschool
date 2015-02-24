using System.Linq;
using Model;
using Rhino.Security.Interfaces;

namespace Web.Extensions
{
	public class NhContestSecurityInterceptor
	{
		/*Tuple<IAuthorizationRepository, IPermissionsBuilderService> Init(ISession session)
		{
			using (var childContainer = new WindsorContainer()
				.Register(Component.For<ISession>().Instance(session)))
			{
				container.AddChildContainer(childContainer);
				return Tuple.Create(childContainer.Resolve<IAuthorizationRepository>(), childContainer.Resolve<IPermissionsBuilderService>());
			}
		}

		readonly IWindsorContainer container;
		public void OnPostInsert(PostInsertEvent @event)
		{
			var contest = @event.Entity as Contest;
			if(contest != null)
			{
				var rs = Init(@event.Session);
				OnContestCreated(contest, rs);
			}
			return;
		}

		public bool OnPreUpdate(PreUpdateEvent ev)
		{
			var contest = ev.Entity as Contest;
			if (contest != null)
			{
				var rs = Init(ev.Session);
				var authz = rs.Item1;
				var pbs = rs.Item2;

				var isPublicIndex = PropertyIndex(ev, "IsPublic");
				var isActiveIndex = PropertyIndex(ev, "IsActive");
				var judgeIndex = PropertyIndex(ev, "Owner");
				var ownerIndex = PropertyIndex(ev, "Judge");

				var oldIsPublic = (bool)ev.OldState[isPublicIndex];
				var newIsPublic = (bool)ev.State[isPublicIndex];

				var oldIsActive = (bool)ev.OldState[isActiveIndex];
				var newIsActive = (bool)ev.State[isActiveIndex];

				var oldOwner = (User)ev.OldState[ownerIndex];
				var newOwner = (User)ev.State[ownerIndex];

				var oldJudge = (User)ev.OldState[judgeIndex];
				var newJudge = (User)ev.State[judgeIndex];

				if(oldIsPublic != newIsPublic)
					OnIsPublicChanged(contest, newIsPublic, pbs);

				if(oldIsActive != newIsActive)
					OnIsActiveChanged(contest, newIsActive, authz);

				if(oldOwner != newOwner)
					OnOwnerChanged(contest, oldOwner, authz);

				if (oldJudge != newJudge)
					OnJudgeChanged(contest, oldJudge, authz);
			}
			return false;
		}
		
		static int PropertyIndex(PreUpdateEvent @event, string propName)
		{
			return Array.IndexOf(@event.Persister.PropertyNames, propName);
		}*/

		public void OnCreated(Contest contest)
		{
			var judges = authz.CreateUsersGroup(UserGroups.ContestJudges(contest));
			var owners = authz.CreateUsersGroup(UserGroups.ContestOwners(contest));
			authz.CreateUsersGroup(UserGroups.ContestParticipants(contest));

			OnOwnerChanged(contest, null);
			OnJudgeChanged(contest, null);
			OnIsPublicChanged(contest, contest.IsPublic);

			var judgeOps = new[]
			               	{
								ContestOperation.View,
								ContestOperation.ViewMonitor,
								ContestOperation.ManageMessages,
			               	};

			var ownerOps = new[]
			               	{
			               		ContestOperation.ChangeAdministration,
								ContestOperation.EditProblems,
								ContestOperation.EditSettings,
								ContestOperation.ManageParticipationApplications,
								ContestOperation.ManageProblems,
								ContestOperation.ManageRights,
								ContestOperation.RejudgeProblems,
								ContestOperation.Submit,
			               	}.Concat(judgeOps);

			foreach (var op in judgeOps)
				pbs.Allow(op).For(judges).On(contest).DefaultLevel().Save();

			foreach (var op in ownerOps)
				pbs.Allow(op).For(owners).On(contest).DefaultLevel().Save();

			var submissions = authz.CreateEntitiesGroup(EntityGroups.Submissions(contest));
			var questionsAndAnswers = authz.CreateEntitiesGroup(EntityGroups.QuestionsAndAnswers(contest));

			pbs.Allow(MessageOperation.View).For(judges).On(questionsAndAnswers).DefaultLevel().Save();
			pbs.Allow(MessageOperation.View).For(owners).On(questionsAndAnswers).DefaultLevel().Save();

			pbs.Allow(SubmissionOperation.View).For(judges).On(submissions).DefaultLevel().Save();
			pbs.Allow(SubmissionOperation.View).For(owners).On(submissions).DefaultLevel().Save();
		}

		public void OnIsPublicChanged(Contest contest, bool newIsPublic)
		{
			if (contest.IsPublic == newIsPublic)
				return;

			var contestantOp = ContestOperation.Submit;
			var label = "PrivateContestRestrictions/" + contest.Id;
			if (newIsPublic)
			{
				foreach(var perm in pms.GetPermissionsByLabel(label))
					authz.RemovePermission(perm);
			}
			else
			{
				pbs.Deny(contestantOp).For(UserGroups.Everyone).On(contest).DefaultLevel().Save(label);
				pbs.Allow(contestantOp).For(UserGroups.ContestParticipants(contest)).On(contest).Level(2).Save(label);
			}
		}

		public void OnOwnerChanged(Contest contest, User oldOwner)
		{
			if(contest.Owner == oldOwner)
				return;
	
			var owners = UserGroups.ContestOwners(contest);
			if(oldOwner != null)
				authz.DetachUserFromGroup(oldOwner, owners);
			authz.AssociateUserWith(contest.Owner, owners);
		}

		public void OnJudgeChanged(Contest contest, User oldJudge)
		{
			if(contest.Judge == oldJudge)
				return;
			
			var judges = UserGroups.ContestJudges(contest);
			if(oldJudge != null)
				authz.DetachUserFromGroup(oldJudge, judges);
			authz.AssociateUserWith(contest.Judge, judges);
		}

		public NhContestSecurityInterceptor(IAuthorizationRepository authz, IPermissionsBuilderService pbs, IPermissionsService pms)
		{
			this.authz = authz;
			this.pbs = pbs;
			this.pms = pms;
		}

		readonly IAuthorizationRepository authz;
		readonly IPermissionsBuilderService pbs;
		readonly IPermissionsService pms;
	}
}