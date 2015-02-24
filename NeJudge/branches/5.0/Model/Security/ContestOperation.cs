using System;

namespace Model
{
	public enum ContestOperation
	{
		View,					// смотреть контест
		Submit,					// посылать на проверку
		ViewMonitor,			// просмотр монитора (особенно, после заморозки)

		EditSettings,			// сдвигать границы контеста
		EditProblems,			// редактировать условия задач, тесты, авторские решения, чекеры
		ManageProblems,			// управление задачами (создание, деактивация)
		RejudgeProblems,		// проводить реджаджи по задачам из комплекта
		ManageMessages,			// просматривать сообщения, посланные в адрес жюри и отвечать на них
						
		ManageRights,			// менять права на этот контест
		ChangeAdministration,	// менять владельца и судью
		ManageParticipationApplications// просматривать и одобрять заявки на участие
	}
}
