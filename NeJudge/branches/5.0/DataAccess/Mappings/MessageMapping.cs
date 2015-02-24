using FluentNHibernate.Mapping;
using Model;

namespace DataAccess.Mappings
{
	public class MessageReadingMapping : EntityMapping<MessageReading>
	{
		public MessageReadingMapping()
		{
			References(x => x.Message);
			References(x => x.User);
		}
	}

	public class MessageMapping : EntityMapping<Message>
	{
		public MessageMapping()
		{
			References(x => x.Author);
			References(x => x.Contest);
			References(x => x.Next);
			Map(x => x.SentAt);
			Map(x => x.Subject);
			Map(x => x.Text);
			DiscriminateSubClassesOnColumn("Type");
		}
	}

	public class AnnouncementMapping : SubclassMap<Announcement>
	{
		public AnnouncementMapping()
		{
			Not.LazyLoad();
		}
	}

	public class QuestionMapping : SubclassMap<Question>
	{
		public QuestionMapping()
		{
			Not.LazyLoad();
		}
	}

	public class ClarificationMapping : SubclassMap<Clarification>
	{
		public ClarificationMapping()
		{
			Not.LazyLoad();
		}
	}

	public class AnswerMapping : SubclassMap<Answer>
	{
		public AnswerMapping()
		{
			Not.LazyLoad();
			References(x => x.Recipient);
		}
	}
}