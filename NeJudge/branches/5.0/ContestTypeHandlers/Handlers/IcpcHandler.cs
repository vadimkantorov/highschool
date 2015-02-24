using System;
using System.Text;
using Model;

namespace Broker.ContestTypeHandlers
{
	public class IcpcHandler : ContestTypeHandlerBase
	{
		protected override bool HasPoints { get { return false; } }
		public override IMonitorBuilder CreateMonitorBuilder(Contest contest)
		{
			return new IcpcMonitorBuilder(contest);
		}

		public override string  RenderMontior(object monitor)
		{
			var icpc = (IcpcMonitor) monitor;
			var sb = new StringBuilder();

			sb.Append("<table>")
				.Append("<tr>")
				.Append("<th>Команда</th>");
				foreach (var shortName in icpc.ProblemShortNames)
					sb.AppendFormat("<th>{0}</th>", shortName);
				sb.Append("<th>Время</th>")
				.Append("<th>Задачи</th>")
				.Append("</tr>");
				foreach (var line in icpc.Lines)
				{
					sb.Append("<tr>")
					  .AppendFormat("<td>{0}</td>", line.UserDisplayName);
					foreach (int value in line.Results)
						sb.AppendFormat("<td>{0}</td>", value > 0 ? "+"+(value == 1 ? "" : (value-1).ToString()) : value.ToString());
					sb.AppendFormat("<td>{0}</td>", line.Time)
					 .AppendFormat("<td>{0}</td>", line.AcceptedProblems)
					 .Append("</tr>");
				}
			sb.Append("</table>");
			return sb.ToString();
		}
	}
}