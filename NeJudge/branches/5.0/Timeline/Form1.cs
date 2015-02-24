using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Broker.ContestTypeHandlers;
using DataAccess;
using Model;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Timeline
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (tooltips == null)
				return;
			var selectedTooltip = tooltips.FirstOrDefault(x => Distance(x.LocationOnPlot, e.Location) <= x.PlotPointRadius);
			if (selectedTooltip == null)
				HideTooltip();
			if (selectedTooltip != lastSelectedTooltip)
				ShowTooltip(selectedTooltip);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var timelineBuilder = new TimelineBuilder();
			var ofd = new OpenFileDialog { Filter = "Лог контеста | *.log" };
			if (ofd.ShowDialog() != DialogResult.OK)
				return;

			var subms = new LogImporter().Import(ofd.FileName);
			var monitor = (IcpcMonitor)new IcpcMonitorBuilder(subms.First().Problem.Contest).BuildMonitor(subms);
			var teams = subms.Select(x => x.Author).Distinct().OrderBy(x => monitor.Lines.FindIndex(y => y.UserDisplayName == x.DisplayName)).ToArray();
			
			var selectTeams = new SelectTeams(teams, timelineBuilder.MaxContestants);
			if(selectTeams.ShowDialog() != DialogResult.OK)
				return;
			var selectedTeams = selectTeams.SelectedTeams;

			var timeline = timelineBuilder.BuildTimeline(subms.Where(x => selectedTeams.Contains(x.Author)));
			pictureBox1.Image = timeline.Plot;
			pictureBox2.Image = timeline.Legend;
			tooltips = timeline.Tooltips;
		}

		private static void PrepareDatabase(ISessionFactory sessionFactory)
		{
			var rnd = new Random();
			var contestants = new List<User>();
			var userNames = new[]
			                	{
									"Синие ведёрки",
									"Team.GOV!",
									"Монтировка",
									"Потом придумаем",
									"Батька Зу",
									"Big Bang",
									"Fusion",
									"Веда",
									"Стройбат",
									"Ядерный взрыв",
									"Легко видеть",
									"Нетрудно догадаться",
									"Очевидно",
									"И действительно!",
									"C'est tout!",
			                	}.OrderBy(x => rnd.Next()).ToArray();
			int userCount = rnd.Next(10, userNames.Length);
			for (int i = 0; i < userCount; i++)
				contestants.Add(new User {DisplayName = userNames[i], UserName = "Team #" + i});

			var contest = new Contest { Beginning = new DateTime(1990, 7, 7, 9, 0, 0), Ending = new DateTime(1990, 7, 7, 14, 0, 0)};

			var submissions = new List<Submission>();
			for (int i = 0; i < 50; i++)
			{
				var shortName = (rnd.Next(10) + 'A').ToString();
				submissions.Add(new Submission
				{
					Author = contestants[rnd.Next(userCount)],
					SubmittedAt = contest.Beginning + TimeSpan.FromMinutes(rnd.Next(300)),
					Result = new SubmissionResult { Verdict = "Accepted" },
					Problem = new Problem { Contest = contest, ShortName = shortName, Statement = new FormattedDocument { Name = "Task " + shortName } }
				});
			}

			using (var session = sessionFactory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				foreach (var s in submissions)
					session.Save(s);

				tx.Commit();
			}
		}

		void ShowTooltip(TooltipInfo tooltip)
		{
			lastSelectedTooltip = tooltip;
			tbTooltip.Text = new StringBuilder()
				.AppendFormat("Задача {0} («{1}»)", tooltip.ProblemShortName, tooltip.ProblemDisplayName).AppendLine()
				.AppendFormat("Сдана на {0}-й минуте", tooltip.Minute).AppendLine()
				.AppendFormat("Команда «{0}»", tooltip.UserName)
				.ToString();
			frmTooltip.Location = Cursor.Position;
			frmTooltip.Show();
		}

		void HideTooltip()
		{
			lastSelectedTooltip = null;
			frmTooltip.Hide();
		}

		static double Distance(Point a, Point b)
		{
			Func<double, double> sqr = x => x*x;
			return Math.Sqrt(sqr(a.X - b.X) + sqr(a.Y - b.Y));
		}

		IEnumerable<TooltipInfo> tooltips;
		TooltipInfo lastSelectedTooltip;
		static readonly Label tbTooltip = new Label { Dock = DockStyle.Fill, BackColor = SystemColors.Info, ForeColor = SystemColors.InfoText, AutoSize = true};
		static readonly Form frmTooltip = new Form
		{
			StartPosition = FormStartPosition.Manual,
			Controls = { tbTooltip },
			FormBorderStyle = FormBorderStyle.None,
			AutoSize = true,
			AutoSizeMode = AutoSizeMode.GrowAndShrink,
		};
	}
}