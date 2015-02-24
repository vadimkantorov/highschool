using System.Collections.Generic;
using System.Linq;
using Model;
using NHibernate;

namespace DataAccess.Queries.Messages
{
	public class MessagesForJudge : ICollectionQuery<MessageReading>
	{
		public Contest Contest { get; set; }

		public User Judge { get; set; }

		public IList<MessageReading> List(ISession session)
		{
			var query = session.CreateSQLQuery(
				@"SELECT {msg.*}, {mr.*}
					FROM Message msg
					LEFT OUTER JOIN MessageReading mr ON (mr.Message_Id = msg.Id  AND mr.User_Id = :user)
					WHERE msg.Contest_Id = :contest")
				.AddEntity("msg", typeof(Message))
				.AddEntity("mr", typeof(MessageReading))
				.SetParameter("contest", Contest.Id)
				.SetParameter("user", Judge.Id);

			return query.List().Cast<object[]>()
				.Select(tuple => new MessageReading
				{
					Message = (Message)tuple[0],
					User = Judge,
					IsRead = tuple[1] != null
				})
				.ToList();
		}
	}
}