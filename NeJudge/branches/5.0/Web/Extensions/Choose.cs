using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Extensions
{
	public class Choose : List<SelectListItem>
	{

		public Choose()
			: this(Enumerable.Empty<SelectListItem>())
		{ }

		public Choose(IEnumerable<SelectListItem> items)
			: base(items)
		{ }

		public static Choose Create(IEnumerable<string> texts)
		{
			return Create(texts, x => x, x => x);
		}

		public static Choose Create<T, TValue>(IEnumerable<T> items, Func<T, string> getDisplay, Func<T, TValue> getValue)
		{
			return Create(items, getDisplay, getValue, x => false);
		}

		public static Choose Create<T, TValue>(IEnumerable<T> items, Func<T, string> getDisplay, Func<T, TValue> getValue, TValue selectedValue)
		{
			return Create(items, getDisplay, getValue, x => getValue(x).Equals(selectedValue));
		}

		public string SelectedValue
		{
			get
			{
				var selectedItem = this.FirstOrDefault(x => x.Selected);
				if (selectedItem != null)
					return selectedItem.Value;

				return selectedValue;
			}
			set
			{
				var selectedItem = this.FirstOrDefault(x => x.Value == value);
				if (selectedItem != null)
					selectedItem.Selected = true;

				selectedValue = value;
			}
		}

		static Choose Create<T, TValue>(IEnumerable<T> items, Func<T, string> getDisplay, Func<T, TValue> getValue, Predicate<T> selectedValue)
		{
			return new Choose(items.Select(x => new SelectListItem
			                                    	{
			                                    		Text = getDisplay(x),
			                                    		Value = getValue(x).ToString(),
			                                    		Selected = selectedValue(x)
			                                    	}));
		}

		string selectedValue;
	}
}