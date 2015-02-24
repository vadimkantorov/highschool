using System.Collections.Generic;
using Broker.Scheduling;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.Windsor;

namespace Broker
{
	public class PrioritySequenceResolver : ISubDependencyResolver
	{
		public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			return container.Resolve<IEnumerable<Priority>>();
		}

		public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
		{
			return dependency.TargetType == typeof(IEnumerable<Priority>);
		}

		public PrioritySequenceResolver(IKernel container)
		{
			this.container = container;
		}

		readonly IKernel container;
	}
}