using System.Collections.Generic;
using System.Web.Mvc;
using Castle.Windsor;

namespace Web.Extensions
{
	public class WindsorFilterAttributeFilterProvider : FilterAttributeFilterProvider
	{
		IWindsorContainer _container;

		public WindsorFilterAttributeFilterProvider(IWindsorContainer container)
		{
			_container = container;
		}

		protected override IEnumerable<FilterAttribute> GetControllerAttributes(ControllerContext controllerContext,
		                                                                        ActionDescriptor actionDescriptor)
		{
			var attributes = base.GetControllerAttributes(controllerContext, actionDescriptor);
			foreach (var attribute in attributes)
			{
				_container.InjectProperties(attribute);
			}
			return attributes;
		}

		protected override IEnumerable<FilterAttribute> GetActionAttributes(ControllerContext controllerContext,
		                                                                    ActionDescriptor actionDescriptor)
		{
			var attributes = base.GetActionAttributes(controllerContext, actionDescriptor);
			foreach (var attribute in attributes)
			{
				_container.InjectProperties(attribute);
			}
			return attributes;
		}
	}
}