using System;
using Model;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Rhino.Security.Impl.Util;
using Rhino.Security.Interfaces;
using Rhino.Security.Model;
using Rhino.Security.Services;
using Web.Extensions;

namespace Web.Services
{
	public class AuthorizationHelper
	{
		public ICriteria Add(ICriteria crit, Enum op)
		{
			authz.AddPermissionsToQuery(userSession.CurrentUser, op.ToOperation(), crit);
			return crit;
		}

		public ICriteria Add<TRef>(ICriteria crit, string path, Enum op)
		{
			crit.Add(AuthorizationService.GetPermissionQueryInternal(userSession.CurrentUser, op.ToOperation(), path + "." + Rhino.Security.Security.GetSecurityKeyProperty(typeof(TRef))));
			return crit;
		}

		public AuthorizationHelper(IAuthorizationService authz, IUserSession userSession)
		{
			this.authz = authz;
			this.userSession = userSession;
		}

		readonly IAuthorizationService authz;
		readonly IUserSession userSession;
	}
}