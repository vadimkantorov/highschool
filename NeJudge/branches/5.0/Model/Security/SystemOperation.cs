using System;

namespace Model
{
	public enum SystemOperation
	{
		ManageContests,			// управление контестами (создание, деактивация)
		ManageRights,			// менять людям системные права, добавлять права на контесты, управление ролями
		ManageUsers,				// массовое создание пользователей и просмотр всего списк
	}
}