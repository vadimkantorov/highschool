using System.Collections.Generic;
using System.Linq;
using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Messages
{
	public class MessagesForContestant : ICollectionQuery<MessageReading>
	{
		public Contest Contest { get; set; }

		public User Contestant { get; set; }

		public IList<MessageReading> List(ISession session)
		{
			var query = session.CreateSQLQuery(
				@"SELECT {msg.*}, {mr.*}
					FROM Message msg
					LEFT OUTER JOIN MessageReading mr ON (mr.Message_Id = msg.Id  AND mr.User_Id = :user)
					WHERE msg.Contest_Id = :contest
						AND (msg.Type = 'Announcement'
							OR msg.Type = 'Clarification'
							OR (msg.Type = 'Question' AND msg.Author_Id = :user)
							OR (msg.Type = 'Answer' AND msg.Recipient_Id = :user)
							)")
				.AddEntity("msg", typeof(Message))
				.AddEntity("mr", typeof(MessageReading))
				.SetParameter("contest", Contest.Id)
				.SetParameter("user", Contestant.Id);

			return query.List().Cast<object[]>()
				.Select(tuple => new MessageReading
				{
					Message = (Message)tuple[0],
					User = Contestant,
					IsRead = tuple[1] != null
				})
				.ToList();
		}
	}
}