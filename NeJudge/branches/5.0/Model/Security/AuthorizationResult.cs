namespace Model
{
	public class AuthorizationResult
	{
		public bool OperationIsAllowed { get; set; }

		public string Explanation { get; set; }

		public object Operation { get; set; }

		public AuthorizationResult(object operation)
		{
			Operation = operation;
		}

		public static AuthorizationResult Allowed(object op)
		{
			return new AuthorizationResult(op) {OperationIsAllowed = true};
		}

		public static AuthorizationResult Denied(object op, string explanation)
		{
			return new AuthorizationResult(op) { Explanation = explanation };
		}
	}
}