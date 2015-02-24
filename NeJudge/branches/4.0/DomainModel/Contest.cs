using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
	public class ContestTranslation
	{
		string culture;
		string translatedName;
		string translatedInfo;

		#region ORM related fields

		int id;
		ContestTranslator translator;

		#endregion

		#region Properties

		public CultureInfo Culture
		{
			get { return CultureInfo.GetCultureInfo(culture); }
			set { culture = value.Name; }
		}

		public string TranslatedName
		{
			get { return translatedName; }
			set { translatedName = value; }
		}

		public string TranslatedInfo
		{
			get { return translatedInfo; }
			set { translatedInfo = value; }
		}

		#endregion
	}

	public class ContestTranslator
	{
		string defaultName;
		string defaultInfo;
		string defaultCulture;

		public CultureInfo DefaultCulture
		{
			get { return CultureInfo.GetCultureInfo(defaultCulture); }
			set { defaultCulture = value.Name; }
		}

		public void SetDefaultName(string value)
		{
			defaultName = value;
		}

		public void SetDefaultInfo(string value)
		{
			defaultInfo = value;
		}

		public string GetTranslatedName(CultureInfo culture)
		{
			return "";
		}

		public string GetTranslatedInfo(CultureInfo culture)
		{
			return "";
		}
	}
	
	public class Contest
	{
		#region Fields

		ContestTranslator translator;

		bool isOpen;
		bool needsUserRegistration;
		DateTime beginning;
		int? duration;
		int freeze;

		#region ORM related fields

		int id;

		#endregion

		#endregion

		#region Properties

		public ContestTranslator Translator
		{
			get { return translator; }
		}

		public string Name
		{
			get { return translator.GetTranslatedName(CultureInfo.CurrentCulture); }
		}

		public string Info
		{
			get { return translator.GetTranslatedInfo(CultureInfo.CurrentCulture); }
		}

		public bool IsOpen
		{
			get { return isOpen; }
			set { isOpen = value; }
		}

		public bool NeedsUserRegistration
		{
			get { return needsUserRegistration; }
			set { needsUserRegistration = value; }
		}
		
		public DateTime Beginning
		{
			get { return beginning; }
			set { beginning = value; }
		}

		public int Freeze
		{
			get { return freeze; }
			set { freeze = value; }
		}

		public int Duration
		{
			get
			{
				if( IsInfinite )
					throw new InvalidOperationException("Cannot get Duration for infinite contest");
				return duration.Value;
			}
			set { duration = value; }
		}

		public bool IsInfinite
		{
			get { return duration == null; }
			set { duration = null; }
		}

		#region ORM related properties

		public int ID
		{
			get { return id; }
		}

		#endregion

		#endregion
	}
}
