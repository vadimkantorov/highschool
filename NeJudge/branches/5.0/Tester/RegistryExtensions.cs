using Microsoft.Win32;

namespace Tester
{
	public static class RegistryExtensions
	{
		public static T ThrowingGetValue<T>(this RegistryKey root, string subKeyName, string valueName, string performedAction)
		{
			using (var openedSubKey = root.OpenSubKey(subKeyName))
			{
				if (openedSubKey == null)
					throw new RegistryKeyNotFoundException(performedAction, root, subKeyName, valueName);
				object value = openedSubKey.GetValue(valueName);
				if (value == null || !(value is T))
					throw new RegistryKeyNotFoundException(performedAction, root, subKeyName, valueName);
				return (T) value;
			}
		}
	}
}