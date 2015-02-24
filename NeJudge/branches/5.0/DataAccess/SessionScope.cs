using System;
using NHibernate;
using NHibernate.Context;

namespace DataAccess
{
	public class SessionScope : IDisposable
	{
		public SessionScope(ISessionFactory factory)
		{
			this.factory = factory;
			var session = factory.OpenSession();
			session.BeginTransaction();

			CurrentSessionContext.Bind(session);
		}

		public ISession Session
		{
			get { return factory.GetCurrentSession(); }
		}

		public void Dispose()
		{
			if (CurrentSessionContext.HasBind(factory))
			{
				using (var session = factory.GetCurrentSession())
				{
					if (session.Transaction != null && session.Transaction.IsActive)
						session.Transaction.Commit();

					CurrentSessionContext.Unbind(factory);
				}
			}
		}

		readonly ISessionFactory factory;
	}
}