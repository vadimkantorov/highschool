using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class LanguageManager
	{
		protected string _connectionString;

		public LanguageManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(string languageID);
		public abstract Language GetLanguage(string languageID);
		public abstract Language[] GetLanguages();
		public abstract void AddLanguage(Language l);
		public abstract void UpdateLanguage(Language l);
		public abstract void RemoveLanguage(string languageID);
		public abstract string GetScript(string languageID);
	}
}