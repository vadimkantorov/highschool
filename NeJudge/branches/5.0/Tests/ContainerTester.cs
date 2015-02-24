using System;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;

namespace Tests
{
	public static class ContainerTester
	{
		public static void Test(IWindsorContainer container)
		{
			foreach (var handler in container.Kernel.GetAssignableHandlers(typeof(object)))
			{
				if (handler is DefaultGenericHandler)
				{
					Type[] genericArguments = handler
						.ComponentModel
						.Service
						.GetGenericArguments();

					foreach (Type genericArgument in genericArguments)
					{
						Type[] genericParameterConstraints =
							genericArgument.GetGenericParameterConstraints();
						foreach (Type genericParameterConstraint in genericParameterConstraints)
						{
							container.Resolve(
								handler
									.ComponentModel
									.Service
									.MakeGenericType(genericParameterConstraint));
						}
					}
				}
				else
				{
					container.Resolve(handler.ComponentModel.Service);
				}
			}
		}
	}
}