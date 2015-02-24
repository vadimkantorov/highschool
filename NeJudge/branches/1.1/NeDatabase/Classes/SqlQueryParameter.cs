namespace Ne.Database.New
{
	public struct SqlQueryParameter
	{
		public string Name;
		public object Value;

		public SqlQueryParameter(string name, string val)
		{
			Name = name;
			Value = val;
		}
	}
}