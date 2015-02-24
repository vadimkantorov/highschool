using System;
using DataAccess;
using NHibernate;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.MessageModules;

namespace Broker
{
	public class NhMessageModule : IMessageModule
	{
		public void Init(ITransport transport, IServiceBus bus)
		{
			transport.MessageArrived += OpenSession;
			transport.BeforeMessageTransactionCommit += DisposeOfSession;
		}

		public void Stop(ITransport transport, IServiceBus bus)
		{
			transport.MessageArrived -= OpenSession;
			transport.BeforeMessageTransactionCommit -= DisposeOfSession;
		}

		private bool OpenSession(CurrentMessageInformation arg)
		{
			scope = new SessionScope(factory);
			return false;
		}

		private static void DisposeOfSession(CurrentMessageInformation arg1)
		{
			scope.Dispose();
		}

		public NhMessageModule(ISessionFactory factory)
		{
			this.factory = factory;
		}

		[ThreadStatic]
		static SessionScope scope;
		readonly ISessionFactory factory;
	}
}