using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.Helpers;
using Ne.ContestTypeHandlers;

namespace Ne.Judge
{
	[RequireContestID]
	public partial class MonitorPage : Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();
				//Page.Response.AddHeader("Refresh","300");
				if ( rp.ContestIDDefined )
				{
					selcon.ContestID = rp.ContestID;

					Contest con = Contest.GetContest(rp.ContestID);
					if ( con.Time == ContestTime.Forthcoming )
						throw new NeJudgeException("Нельзя просмотреть монитор будущего соревнования");
					DateTime cur_time = TimeUtils.ZeroDateTime(DateTime.Now);
					DateTime t_begin = TimeUtils.ZeroDateTime(con.Beginning);
					DateTime t_end = TimeUtils.ZeroDateTime(con.Ending);

					// Последнее обновление
					TimeSpan dur = TimeUtils.ZeroTimeSpan(t_end - t_begin);
					TimeSpan elapsed = TimeUtils.ZeroTimeSpan((cur_time - t_begin < dur) ? cur_time - t_begin : dur);
					TimeSpan estimated = TimeUtils.ZeroTimeSpan(dur - elapsed);
					if ( elapsed >= dur )
					{
						st_label.InnerHtml += "<span style='color:red;font-size:small;display:block;'>(Соревнование окончено)</span>";
					}
					refresh.Text = TimeUtils.BeautifyTimeSpan(elapsed, false);
					// Продолжительность соревнования
					period.Text = TimeUtils.BeautifyTimeSpan(dur, false);
					left.Text = TimeUtils.BeautifyTimeSpan(estimated, false);


					ContestTypeHandler h = Factory.GetHandlerInstance(con.Type);
					DataTable[] dts = h.MonitorManager.Build(con.ID);
					for(int i = 0; i < dts.Length; i++)
					{
						DataTable dt = dts[i];
						monitorPH.Controls.Add(new LiteralControl(dt.TableName));
						DataGrid gv = new DataGrid();
						gv.Width = new Unit(100.0, UnitType.Percentage);
						gv.CellPadding = 5;
						gv.HeaderStyle.CssClass = "gridHeader";
						gv.DataSource = dt;
						gv.DataBind();
						h.MonitorManager.PaintDataGrid(gv, i, dt.Columns.Count - 3);
						monitorPH.Controls.Add(gv);
						if ( i != dts.Length - 1 )
							monitorPH.Controls.Add(new LiteralControl("<br />"));
					}
				}
				else
					info.Visible = false;
			}
		}
	}
}