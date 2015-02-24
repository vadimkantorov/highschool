using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web.Extensions
{
	public class NeBinder : DefaultModelBinder
	{
		protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
		                                    System.ComponentModel.PropertyDescriptor propertyDescriptor, object value)
		{
			if (propertyDescriptor.PropertyType == typeof(Choose))
			{
				var valueKey = string.IsNullOrEmpty(bindingContext.ModelName)
				               	? propertyDescriptor.Name
				               	: string.Format("{0}.{1}", bindingContext.ModelName, propertyDescriptor.Name);
				valueKey += ".SelectedValue";
				
				var listItemValue = bindingContext.ValueProvider.GetValue(valueKey).AttemptedValue;
				var items = propertyDescriptor.GetValue(bindingContext.Model) as Choose;
				if (items == null)
				{
					items = new Choose();
					propertyDescriptor.SetValue(bindingContext.Model, items);
				}
				items.SelectedValue = listItemValue;
				return;
			}


			base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
		}
	}
}