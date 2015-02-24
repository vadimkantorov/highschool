using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	[RequireContestId]
	public class monitor : UserControl
	{
		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		protected Literal time;
		protected Literal refresh;
		protected Literal period;
		protected Literal lastac;
		protected Literal left;
		protected SelectContest selcon;
		protected ErrorMessage ErrorMessage;
		protected DataGrid monitorDG;
		protected HtmlGenericControl info;
		protected HtmlGenericControl st_label;
		protected DataGrid statDG;

		private void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();
				//Page.Response.AddHeader("Refresh","300");
				if(rp.TidDefined)
				{
					Contest con;
					BaseDb db = DbFactory.ConstructDatabase();
					using ( db )
					{
						con = db.GetContest(rp.ContestID);
						if ( con.Future )
						{
							Hide("Нельзя просмотреть монитор будущего соревнования");
							db.Close();
						}
						else
						{
							DateTime cur_time = TimeUtils.ZeroDateTime(DateTime.Now);
							DateTime t_begin = TimeUtils.ZeroDateTime(con.Beginning);
							DateTime t_end = TimeUtils.ZeroDateTime(con.Ending);

							// Последнее обновление
							TimeSpan dur = TimeUtils.ZeroTimeSpan(t_end - t_begin);
							TimeSpan elapsed = TimeUtils.ZeroTimeSpan( (cur_time - t_begin < dur) ? cur_time - t_begin : dur);
							TimeSpan estimated = TimeUtils.ZeroTimeSpan(dur - elapsed);
							if ( elapsed >= dur )
							{
								st_label.InnerHtml += 
									"<span style='color:red;font-size:small;display:block;'>(Соревнование окончено)</span>";
							}
							refresh.Text = HtmlFunctions.BeautifyTimeSpan(elapsed, false);
							// Продолжительность соревнования
							period.Text = HtmlFunctions.BeautifyTimeSpan(dur, false);
							left.Text = HtmlFunctions.BeautifyTimeSpan(estimated, false);
						}
					}
					/*DataTable mon_dt = new DataTable("Результаты"), stat_dt = new DataTable("Статистика");
					mon_dt.Columns.Add("ID");
					mon_dt.Columns.Add("Имя участника");
					//mon_dt.PrimaryKey = new DataColumn[]{mon_dt.Columns[0]};

					stat_dt.Columns.Add("Показатель");
					DataRow dr = stat_dt.NewRow();
					dr[0] = "Команд решили";
					stat_dt.Rows.Add(dr);
					dr = stat_dt.NewRow();
					dr[0] = "Команд решили/команд всего";
					stat_dt.Rows.Add(dr);

					int tid = rp.ContestID;
					selcon.TID = tid;
					BaseDb db = DbFactory.ConstructDatabase();
					//HyperLink1.NavigateUrl += tid;
					//Прочитали результаты
					Contest t;
			
					if ((t = db.GetContest(tid)).Future)
					{
						Hide("Нельзя просмотреть монитор будущего сорвевнования");
						db.Close();
					}
					else
					{
						DateTime cur_time = ZeroDateTime(DateTime.Now);
						DateTime t_begin = ZeroDateTime(t.Beginning);
						DateTime t_end = ZeroDateTime(t.Ending);

						// Последнее обновление
						TimeSpan dur = ZeroTimeSpan(t_end - t_begin);
						TimeSpan elapsed = ZeroTimeSpan( (cur_time - t_begin < dur) ? cur_time - t_begin : dur);
						TimeSpan estimated = ZeroTimeSpan(dur - elapsed);
						if ( elapsed >= dur )
						{
							st_label.InnerHtml += 
								"<span style='color:red;font-size:small;display:block;'>(Соревнование окончено)</span>";
						}
						refresh.Text = HtmlFunctions.BeautifyTimeSpan(elapsed, false);
						// Продолжительность соревнования
						period.Text = HtmlFunctions.BeautifyTimeSpan(dur, false);
						left.Text = HtmlFunctions.BeautifyTimeSpan(estimated, false);

						ArrayList ps = db.GetProblems(tid);
						if (ps.Count == 0)
						{
							Hide("В этом соревновании нет задач");
							return;
						}
						int i = 0;
						for (i = 0; i < ps.Count; i++)
						{
							mon_dt.Columns.Add(((Problem) ps[i]).ShortName);
							//mon_dt.Columns.Add(string.Format("<a style='grid_first' href='problem.aspx?pid={0}'>{1}</a>",((Problem)ps[i]).PID, ((Problem)ps[i]).ShortName));
							stat_dt.Columns.Add(((Problem) ps[i]).ShortName);
							//stat_dt.Columns.Add(string.Format("<a href='problem.aspx?pid={0}'>{1}</a>",((Problem)ps[i]).PID, ((Problem)ps[i]).ShortName));
						}
						mon_dt.Columns.Add("Всего решено");
						mon_dt.Columns.Add("Время");
						mon_dt.Columns.Add("Место");
						//Колонки созданы
						TimeSpan the_latest = new TimeSpan(0, 0, 0, 0, 0);
						bool has_ac = false;
						ArrayList user_data = new ArrayList();

						#region обработка очереди

						foreach (Submission s in db.GetSubmissions(tid, 0))
						{
							// Юзер, пославший текущий сабмишн
							UserData u = null;
							foreach (UserData ud in user_data)
							{
								if (ud.UID == s.UID)
								{
									u = ud;
									break;
								}
							}
							// Новая строка с юзером
							if (u == null)
							{
								u = new UserData();
								u.UID = (uint) s.UID;
								u.Solved = 0;
								u.Time = new TimeSpan(0, 0, 0, 0, 0);
								u.Problems = new ArrayList();
								foreach (Problem p in ps)
								{
									Attempts at = new Attempts();
									at.AcTime = new TimeSpan(0, 0, 0, 0, 0);
									at.Count = 0;
									at.PID = (uint) p.PID;
									u.Problems.Add(at);
								}
								user_data.Add(u);
							}
							Attempts subm_att = null;
							foreach (Attempts sat in u.Problems)
							{
								if (sat.PID == s.PID)
								{
									subm_att = sat;
									break;
								}
							}
							if (subm_att == null)
							{
								throw new ApplicationException("Невозможно найти задачу в описании участника");
							}
							if (s.Result.Code == Result.AC)
							{
								if (subm_att.Count <= 0)
								{
									// вычисление попыток
									subm_att.Count = -subm_att.Count + 1;
									subm_att.AcTime = s.Time - t_begin;
									subm_att.AcTime = ZeroTimeSpan(subm_att.AcTime);
									has_ac = true;
									if (the_latest < subm_att.AcTime)
									{
										the_latest = subm_att.AcTime;
									}
									// начисление времени с учетом штрафа
									u.Time += subm_att.AcTime + 
										new TimeSpan(0, 0, (subm_att.Count - 1)*20, 0, 0);
								}
							}
							else if (s.Result.Code != Result.WAIT && s.Result.Code != Result.RU)
							{
								if (subm_att.Count <= 0)
								{
									subm_att.Count--;
								}
							}
						}

						#endregion

						foreach (UserData ud in user_data)
						{
							foreach (Attempts atte in ud.Problems)
							{
								if (atte.Count > 0)
								{
									ud.Solved++;
								}
							}
						}

						if (has_ac)
						{
							lastac.Text = HtmlFunctions.BeautifyTimeSpan(the_latest, false);
						}
						else
						{
							lastac.Text = "Еще не было";
						}
						user_data.Sort(new UserCompare());
						if (user_data.Count == 0)
						{
							st_label.InnerHtml += "<span style='color:#CFC411;font-size:small;display:block;'>(Не было послано ни одного решения)</span>";
						}
						int[] ac_counts = new int[ps.Count];
						int[] total_counts = new int[ps.Count];
						foreach (UserData ud in user_data)
						{
							DataRow mon_dtr = mon_dt.NewRow();
							mon_dtr[0] = ud.UID;
							User us = db.GetUser((int) ud.UID);
							mon_dtr[1] = us.Fullname;
							mon_dt.Rows.Add(mon_dtr);
							for (int j = 0; j < ud.Problems.Count; j++)
							{
								int count = ((Attempts) ud.Problems[j]).Count;
								if (count > 0)
								{
									// Write a result to the problem's column
									mon_dtr[2 + j] = "<span style='color:blue;'>+";
									if (count != 1)
									{
										mon_dtr[2 + j] += (count - 1).ToString();
									}
									mon_dtr[2 + j] += "</span><span style='font-size:smaller;display:block;'>(";
									mon_dtr[2 + j] += HtmlFunctions.BeautifyTimeSpan(((Attempts) ud.Problems[j]).AcTime, true);
									mon_dtr[2 + j] += ")</span>";
									// Update AC received users count
									ac_counts[j]++;
									total_counts[j]++;
								}
								else if (count < 0)
								{
									mon_dtr[2 + j] = "<span style='color:red;'>";
									mon_dtr[2 + j] += count.ToString();
									mon_dtr[2 + j] += "</span>";
									total_counts[j]++;
								}
								else
								{
									mon_dtr[2 + j] = "";
								}
							}
							mon_dtr[mon_dt.Columns.Count - 2] = (int) ud.Time.TotalMinutes;
							mon_dtr[mon_dt.Columns.Count - 3] = ud.Solved;
						}
						db.Close();
						for (int l = 0; l < mon_dt.Rows.Count; l++)
						{
							mon_dt.Rows[l][mon_dt.Columns.Count - 1] = l + 1;
						}


						monitorDG.DataSource = mon_dt;
						monitorDG.DataBind();

						#region статистика

						for (int k = 0; k < ps.Count; k++)
						{
							stat_dt.Rows[0][k + 1] = ac_counts[k];
							double ac_percent = (total_counts[k] == 0)
								? 0 : (double)ac_counts[k] / (double)total_counts[k] * 100.0;
							stat_dt.Rows[1][k + 1] = String.Format("{0:F2}%", ac_percent);
						}
						statDG.DataSource = stat_dt;
						statDG.DataBind();

						#endregion

						Draw(mon_dt.Columns.Count - 3);
					}*/
					Monitor m = MonitorManager.GetMonitor(rp.ContestID, Context);
					m.Reload();
					if ( m.HasAccepted )
					{
						lastac.Text = String.Format("{0}<br><strong>Задача {1}</strong>", 
							HtmlFunctions.BeautifyTimeSpan(
							TimeUtils.ZeroTimeSpan(m.LastAccepted.Time - con.Beginning), false),
							m.LastAccepted.ProblemShortName);
					}
					else
					{
						lastac.Text = "Еще не было";
					}
					DataTable mon = m.FormatMonitor();
					monitorDG.DataSource = mon;
					monitorDG.DataBind();
					Draw(mon.Columns.Count-3);
					statDG.DataSource = m.FormatStats();
					statDG.DataBind();
				}
				else
				{
					Hide("");
				}
			}
		}
/*
		private class UserCompare : IComparer
		{
			public int Compare(object x, object y)
			{
				UserData dx = x as UserData;
				UserData dy = y as UserData;
				if (dx == null || dy == null)
				{
					return 0;
				}
				if (dx.Solved > dy.Solved)
				{
					return -1;
				}
				else if (dx.Solved < dy.Solved)
				{
					return 1;
				}
				else
				{
					if (dx.Time > dy.Time)
					{
						return 1;
					}
					else if (dx.Time < dy.Time)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}
		}

		private class UserData
		{
			public uint UID;
			public TimeSpan Time;
			public uint Solved;
			public ArrayList Problems;
		}

		private class Attempts
		{
			public int Count;
			public uint PID;
			public TimeSpan AcTime;
		}
*/

		private void Draw(int index)
		{
			if (monitorDG.Items.Count != 0)
				monitorDG.Items[0].CssClass = "grid_first";
			for (int i = 1; i < monitorDG.Items.Count; i++)
			{
				if (monitorDG.Items[i].Cells[index].Text != monitorDG.Items[i - 1].Cells[index].Text)
				{
					if (monitorDG.Items[i - 1].CssClass == "grid_first")
						monitorDG.Items[i].CssClass = "grid_second";
					else
						monitorDG.Items[i].CssClass = "grid_first";
				}
				else
				{
					monitorDG.Items[i].CssClass = monitorDG.Items[i - 1].CssClass;
				}
			}
		}

		private void Hide(string mess)
		{
			ErrorMessage.Message = "<br>" + mess;
			info.Visible = false;
		}
	}
}