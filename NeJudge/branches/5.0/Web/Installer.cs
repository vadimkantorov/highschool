using System.Collections;
using System.Data.SqlClient;
using Broker.Common;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataAccess;
using Microsoft.Practices.ServiceLocation;
using Model;
using Model.Utils;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Rhino.Security;
using Rhino.Security.Interfaces;
using Web.Controllers;
using Web.Extensions;
using Web.Services;


namespace Web
{
	[System.ComponentModel.RunInstaller(true)]
	public class Installer : System.Configuration.Install.Installer
	{
		public override void Install(IDictionary stateSaver)
		{
			var neCfg = new DefaultDatabaseConfiguration();
			CreateDatabase(neCfg.Server, neCfg.Database, neCfg.Username, neCfg.Password);

			var nhibernateCfg = neCfg.DatabaseConfiguration;
			Rhino.Security.Security.Configure<User>(nhibernateCfg, SecurityTableStructure.Prefix, u => new SecurityInfo(u.DisplayName, u.Id));

			var container = new WindsorContainer()
				.AddFacility<RhinoSecurityFacility>()
				.Register(
					Component.For(typeof (IEntityInformationExtractor<>)).ImplementedBy(typeof (EntityInformationExtractor<>)).LifeStyle.Transient,
					Component.For<UserController>(),
					Component.For<IClock>().ImplementedBy<SystemClock>(),
					Component.For<NhUserSecurityInterceptor>());
			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
			
			new SchemaExport(nhibernateCfg).Execute(false, true, false);

			using (var factory = nhibernateCfg.BuildSessionFactory())
			using (var scope = new SessionScope(factory))
			{
				container.Register(Component.For<ISession>().Instance(scope.Session));
				authz = container.Resolve<IAuthorizationRepository>();
				pbs = container.Resolve<IPermissionsBuilderService>();
				session = container.Resolve<ISession>();
				interceptor = container.Resolve<NhUserSecurityInterceptor>();

				SaveSecurityObjects();
				SaveAdminAndAnonymous();
				
				SaveLanguages();
			}
		}

		void SaveSecurityObjects()
		{
			SaveOperations<ContestOperation>();
			SaveOperations<SystemOperation>();
			SaveOperations<SubmissionOperation>();
			SaveOperations<MessageOperation>();

			var activeContests = authz.CreateEntitiesGroup(EntityGroups.ActiveContests);

			var everyone = authz.CreateUsersGroup(UserGroups.Everyone);
			var superAdmins = authz.CreateUsersGroup(UserGroups.SuperAdmins);

			pbs.Allow(ContestOperation.View).For(everyone).On(activeContests).DefaultLevel().Save();
			pbs.Allow(ContestOperation.Submit).For(everyone).On(activeContests).DefaultLevel().Save();

			pbs.Allow(ContestOperation.View).For(superAdmins).OnEverything().DefaultLevel().Save();
		}

		void SaveOperations<TOperation>()
		{
			foreach (var c in EnumUtils.Values<TOperation>())
				authz.CreateOperation(c.ToOperation());
		}

		void SaveLanguages()
		{
			var langs = new LanguageDiscovery();
			foreach (var l in langs.DiscoverAvailableLanguages())
				session.Save(l);
		}

		void SaveAdminAndAnonymous()
		{
			var superAdmins = authz.GetUsersGroupByName(UserGroups.SuperAdmins);
			var everyone = authz.GetUsersGroupByName(UserGroups.Everyone);
			const string emptyPassword = "";
			
			var admin = new User {DisplayName = "Administrator", UserName = "admin", Password = new Password(emptyPassword)};
			session.Save(admin);
			interceptor.OnCreated(admin);
			
			var anon = new User {DisplayName = "Anonymous", UserName = UserSession.AnonymousUserName, Password = new Password(emptyPassword)};
			session.Save(anon);
			interceptor.OnCreated(anon);
			
			pbs.AllowOnEverything(superAdmins, EnumUtils.Values<SystemOperation>(), EnumUtils.Values<MessageOperation>(), EnumUtils.Values<MessageOperation>(), EnumUtils.Values<ContestOperation>());

			authz.AssociateUserWith(admin, everyone);
			authz.AssociateUserWith(admin, superAdmins);

			authz.AssociateUserWith(anon, everyone);
		}

		static void CreateDatabase(string server, string database, string username, string password)
		{
			const string script =
				@"IF exists(SELECT * FROM sysdatabases WHERE [Name] = '{0}')
					DROP DATABASE {0}
				CREATE DATABASE {0}";

			var connStrBuilder = new SqlConnectionStringBuilder {DataSource = server, InitialCatalog = "master"};
			if (username == null && password == null)
				connStrBuilder.IntegratedSecurity = true;
			else
			{
				connStrBuilder.UserID = username;
				connStrBuilder.Password = password;
			}

			using (var conn = new SqlConnection(connStrBuilder.ToString()))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText = string.Format(script, database);
					cmd.ExecuteNonQuery();
				}
			}
		}

		IAuthorizationRepository authz;
		IPermissionsBuilderService pbs;
		ISession session;
		NhUserSecurityInterceptor interceptor;
	}
}
