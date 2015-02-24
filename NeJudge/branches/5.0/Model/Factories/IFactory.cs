using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Model.Factories
{
	public interface INamed
	{
		string Name { get; }
	}
	
	public interface IFactory<T> where T : INamed
	{
		void RegisterAll();
		T Find(string key);
		IEnumerable<T> GetAll();
	}

	public class NamedBase : INamed
	{
		static string GetDefaultName(Type handlerType)
		{
			var handlerSuffix = GuessSuffix(handlerType.Name);

			string typeName = handlerType.Name;
			if (!typeName.EndsWith(handlerSuffix))
				return null;
			return typeName.Substring(0, typeName.Length - handlerSuffix.Length).Replace("_", " ");
		}

		static string GuessSuffix(string typeName)
		{
			var reversed = typeName.Reverse();
			return string.Join("", new[] {reversed.SkipWhile(char.IsLower).First()}.Concat(reversed.TakeWhile(char.IsLower).Reverse()));
		}

		public string Name
		{
			get { return GetDefaultName(GetType()); }
		}
	}

	public class Factory<T> : IFactory<T> where T : INamed
	{
		public void RegisterAll()
		{
			using (var tempContainer = new WindsorContainer())
			{
				tempContainer.Register(Component.For<Assembly>().Instance(typeof(T).Assembly));
				RegisterInTemporaryContainer(tempContainer);

				foreach (var compiler in tempContainer.ResolveAll<T>())
					container.Register(Component.For<T>().Instance(compiler).Named(compiler.Name));
			}
		}

		protected virtual void RegisterInTemporaryContainer(IWindsorContainer tempContainer)
		{
			tempContainer.Register(AllTypes.FromAssembly(tempContainer.Resolve<Assembly>()).BasedOn<T>());
		}

		public T Find(string languageId)
		{
			return container.Resolve<T>(languageId);
		}

		public IEnumerable<T> GetAll()
		{
			return container.ResolveAll<T>();
		}

		public Factory(IWindsorContainer outerContainer)
		{
			container = new WindsorContainer();
			outerContainer.AddChildContainer(container);
		}

		readonly IWindsorContainer container;
	}
}