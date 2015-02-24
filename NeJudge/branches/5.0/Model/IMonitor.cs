using System.Collections.Generic;

namespace Model
{
	public interface IMonitor
	{
		void Update(IEnumerable<Submission> submissions);
	}
}