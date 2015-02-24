using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	public class Language
	{
		#region Fields

		string id = "";
		string name;
		string ext;
		string runCmd;
		string script;

		#endregion

		#region Properties

		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Extension
		{
			get { return ext; }
			set { ext = value; }
		}

		public string RunCommand
		{
			get { return runCmd; }
			set { runCmd = value; }
		}

		public string CompileScript
		{
			get
			{
				if ( script == null )
				{
					if ( id != "" )
						script = DataProvider.Provider.LanguageManager.GetScript(id);
					else
						script = "";
				}
				return script;
			}
			set { script = value; }
		}

		#endregion

		// Значение RunCommand по умолчанию (запуск решения осуществляется системным вызовом)
		public const string SystemExec = "{exec}";

		#region Constructors

		public Language()
		{}

		public Language(string id, string name, string extension) :
			this(id, name, extension, SystemExec)
		{}

		public Language(string id, string name, string extension, string runCommand)
		{
			this.id = id;
			this.name = name;
			this.ext = extension;
			this.runCmd = String.IsNullOrEmpty(runCommand) ? SystemExec : runCommand;
		}

		#endregion

		#region Database Access Members

		public void Store()
		{
			if ( !ValidateID(id) )
				DataProvider.Provider.LanguageManager.AddLanguage(this);
			else
				DataProvider.Provider.LanguageManager.UpdateLanguage(this);
		}

		public static bool ValidateID(string languageID)
		{
			return DataProvider.Provider.LanguageManager.ValidateID(languageID);
		}

		public static Language GetLanguage(string languageID)
		{
			return DataProvider.Provider.LanguageManager.GetLanguage(languageID);
		}

		public static Language[] GetLanguages()
		{
			return DataProvider.Provider.LanguageManager.GetLanguages();
		}

		public static void AddLanguage(Language l)
		{
			DataProvider.Provider.LanguageManager.AddLanguage(l);
		}

		public static void UpdateLanguage(Language l)
		{
			DataProvider.Provider.LanguageManager.UpdateLanguage(l);
		}

		public static void RemoveLanguage(string languageID)
		{
			DataProvider.Provider.LanguageManager.RemoveLanguage(languageID);
		}

		#endregion
	}
}