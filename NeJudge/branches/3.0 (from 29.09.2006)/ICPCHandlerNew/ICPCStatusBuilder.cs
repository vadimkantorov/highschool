using System;
using System.Collections.Generic;
using System.Text;

using Ne.Interfaces;

namespace Ne.ContestTypeHandlers
{
	using Cache = SubmissionInfoCache<ICPCSubmissionInfo>;

	public class ICPCStatusBuilder : IStatusBuilder
	{
		Cache cache;

		public LightweightRow GetHeaders()
		{
			LightweightRow ret = new LightweightRow();

			ret.Cells.Add(new PlaintextCell("���� �"));
			ret.Cells.Add(new PlaintextCell("������������ �����"));
			ret.Cells.Add(new PlaintextCell("������������ ������"));

			return ret;
		}

		public LightweightRow GetInformation(int submissionID)
		{
			if ( cache.GetSubmissionInfo(submissionID) == null )
				cache.AddSubmissionInfo(submissionID);

			ICPCSubmissionInfo info = cache.GetSubmissionInfo(submissionID);
			LightweightRow ret = new LightweightRow();

			ret.Cells.Add(new PlaintextCell(info.StopTest > 0 ? info.StopTest.ToString() : string.Empty));
			ret.Cells.Add(new PlaintextCell(Math.Round(info.MaxTime / 1000.0, 4) + " ���"));
			ret.Cells.Add(new PlaintextCell(info.MaxMemory / 1024 + " ��"));

			return ret;
		}

		public ICPCStatusBuilder(Cache cache)
		{
			this.cache = cache;
		}
	}
}
