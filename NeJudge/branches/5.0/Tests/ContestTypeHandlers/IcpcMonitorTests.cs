using System;
using System.IO;
using System.Linq;
using Broker.ContestTypeHandlers;
using Castle.MicroKernel;
using Model;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Serializers;
using Xunit;

namespace Tests.ContestTypeHandlers
{
	public class IcpcMonitorTests
	{
		Contest contest = new Contest 
		{
			Beginning = new DateTime(1990, 7, 7),
			Problems = new Problem[]
			           	{
			           		new Problem {ShortName = "A"},
							new Problem { ShortName = "B" }
						}
		};

		Submission S(string s)
		{
			var splitted = s.Split();
			return new Submission
			       	{
			       		Author = new User {DisplayName = splitted[0]},
						Problem = contest.Problems.Single(x => x.ShortName == splitted[1]),
						Result = new SubmissionResult {Verdict = splitted[2] == "AC" ? GenericVerdict.Accepted.ToString() : GenericVerdict.WrongAnswer.ToString()},
						SubmittedAt = contest.Beginning.AddMinutes(Convert.ToInt32(splitted[3])).AddSeconds(15)
			       	};
		}

		[Fact]
		public void monitor_is_built_correctly()
		{
			var builder = new IcpcMonitorBuilder(contest);
			var p1ac11u1	= S("u1 A AC 11");
			var p1wa15u1	= S("u1 A WA 15");
			var p2wa17u1	= S("u1 B WA 17");
			var p2ac21u1	= S("u1 B AC 21");
			var p1ac1u2		= S("u2 A AC 1");
			var p2ac5u2		= S("u2 B AC 5");


			var monitor = (IcpcMonitor)builder.BuildMonitor(new[] {p1ac11u1});
			Assert.Equal(1, monitor.Lines[0].AcceptedProblems);
			Assert.Equal(1, monitor.Lines[0].Results[0]);
			Assert.Equal(11, monitor.Lines[0].Time);

			monitor = (IcpcMonitor)builder.BuildMonitor(new[] { p1ac11u1, p1wa15u1 });
			Assert.Equal(1, monitor.Lines[0].AcceptedProblems);
			Assert.Equal(1, monitor.Lines[0].Results[0]);
			Assert.Equal(11, monitor.Lines[0].Time);

			monitor = (IcpcMonitor)builder.BuildMonitor(new[] { p1ac11u1, p1wa15u1, p2wa17u1 });
			Assert.Equal(1, monitor.Lines[0].AcceptedProblems);
			Assert.Equal(-1, monitor.Lines[0].Results[1]);
			Assert.Equal(11, monitor.Lines[0].Time);

			monitor = (IcpcMonitor)builder.BuildMonitor(new[] { p1ac11u1, p1wa15u1, p2wa17u1, p2ac21u1 });
			Assert.Equal(2, monitor.Lines[0].AcceptedProblems);
			Assert.Equal(2, monitor.Lines[0].Results[1]);
			Assert.Equal(11 + 21 + 20*1, monitor.Lines[0].Time);

			monitor = (IcpcMonitor)builder.BuildMonitor(new[] { p1ac11u1, p1wa15u1, p2wa17u1, p2ac21u1, p1ac1u2 });
			Assert.Equal(1, monitor.Lines[1].AcceptedProblems);
			Assert.Equal(1, monitor.Lines[1].Time);

			monitor = (IcpcMonitor)builder.BuildMonitor(new[] { p1ac11u1, p1wa15u1, p2wa17u1, p2ac21u1, p1ac1u2, p2ac5u2 });
			Assert.Equal(2, monitor.Lines[0].AcceptedProblems);
			Assert.Equal(1 + 5, monitor.Lines[0].Time);
		}
		
		[Fact]
		public void can_serialize_and_deserialize_icpc_monitor()
		{
			var ms = new MemoryStream();
			var serializer = new XmlMessageSerializer(new DefaultReflection(), new DefaultKernel());
			var submission = S("testuser A AC 11");
			var builder = new IcpcMonitorBuilder(contest);
			object monitor = builder.BuildMonitor(new[] {submission});

			serializer.Serialize(new[] {monitor}, ms);
			ms.Position = 0;
			serializer.Deserialize(ms);
		}
	}
}