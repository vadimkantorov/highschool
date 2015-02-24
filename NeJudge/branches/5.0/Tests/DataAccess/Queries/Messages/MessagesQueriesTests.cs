using System;
using System.Linq;
using DataAccess.Queries.Messages;
using Model;
using NHibernate;
using Tests;
using Xunit;

namespace DataAccess.Tests.Queries.Messages
{
	public class MessagesQueriesTests
	{
		ISessionFactory factory;
		User isenbaev;
		Contest contest;

		public MessagesQueriesTests()
		{
			factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();

			isenbaev = new User
			           	{
			           		UserName = "Isenbaev"
			           	};

			contest = new Contest
			          	{
			          		Beginning = new DateTime(1990, 7, 7),
			          		Ending = new DateTime(1990, 7, 8)
			          	};
			var kapun = new User
			{
				UserName = "Kapun",
			};

			var announcement = new Announcement
			{
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};

			var clarification = new Clarification
			{
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};

			var myQuestion = new Question
			{
				Author = isenbaev,
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};

			var myAnswer = new Answer
			{
				Recipient = isenbaev,
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};
			var hisQuestion = new Question
			{
				Author = kapun,
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};

			var hisAnswer = new Answer
			{
				Recipient = kapun,
				SentAt = new DateTime(1990, 7, 7),
				Contest = contest
			};

			using (var scope = new SessionScope(factory))
			{
				scope.Session.Save(isenbaev);
				scope.Session.Save(kapun);
				scope.Session.Save(contest);
				scope.Session.Save(announcement);
				scope.Session.Save(clarification);
				scope.Session.Save(myQuestion);
				scope.Session.Save(hisQuestion);
				scope.Session.Save(myAnswer);
				scope.Session.Save(hisAnswer);
			}
		}

		[Fact]
		public void judge_should_view_all_messages()
		{
			using (var scope = new SessionScope(factory))
			{
				var readings = new MessagesForJudge { Contest = contest, Judge = isenbaev }
					.List(scope.Session);
				Assert.Equal(6, readings.Count);
			}
		}

		[Fact]
		public void reading_is_persisted_correctly()
		{
			using (var scope = new SessionScope(factory))
			{
				var readings = new MessagesForContestant { Contest = contest, Contestant = isenbaev }
					.List(scope.Session);
				Assert.True(readings.All(x => !x.IsRead));

				var answerReading = readings.Single(x => x.Message is Answer);
				Assert.False(answerReading.IsRead);

				answerReading.IsRead = true;
				scope.Session.Save(answerReading);
			}

			using (var scope = new SessionScope(factory))
			{
				var readings = new MessagesForContestant { Contest = contest, Contestant = isenbaev }
					.List(scope.Session);
				
				var answerReading = readings.Single(x => x.Message is Answer);
				Assert.True(answerReading.IsRead);
			}
		}

		[Fact]
		public void contestant_should_receive_announcements_clarifications_and_answers()
		{
			using (var scope = new SessionScope(factory))
			{
				var readings = new MessagesForContestant {Contest = contest, Contestant = isenbaev}
					.List(scope.Session);
				var msgs = readings.Select(x => x.Message);
				Assert.NotEmpty(msgs.OfType<Clarification>());
				Assert.NotEmpty(msgs.OfType<Announcement>());
				Assert.Equal(1, msgs.OfType<Question>().Count());
				Assert.Equal(1, msgs.OfType<Answer>().Count());
			}
		}
	}
}