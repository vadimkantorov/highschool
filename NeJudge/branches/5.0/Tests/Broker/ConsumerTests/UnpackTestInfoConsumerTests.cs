using System;
using System.IO;
using Broker.Common.Messages.Broker;
using Broker.Consumers;
using DataAccess;
using Model;
using NHibernate;
using Tests;
using Xunit;

namespace Broker.Tests.ConsumerTests
{
	public class UnpackTestInfoConsumerTests
	{
		[Fact]
		public void unpack_tests_from_real_zip()
		{
			var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();

			byte[] zip = File.ReadAllBytes(@".\..\..\TestData\a_plus_b.zip");
			var problem = new Problem();

			using (var scope = new SessionScope(factory))
				scope.Session.Save(problem);
			Assert.Equal(0, problem.TestInfo.Tests.Count);

			using(var scope = new SessionScope(factory))
			{
				var consumer = new UnpackTestInfoConsumer(new TestsZipper("in", "out"), scope.Session);
				consumer.Consume(new UnpackTestInfo { ProblemId = problem.Id, ZipArchive = zip });
			}

			using(var scope = new SessionScope(factory))
			{
				var newProblem = scope.Session.Get<Problem>(problem.Id);
				Assert.Equal(5, newProblem.TestInfo.Tests.Count);
			}
		}
	}
}