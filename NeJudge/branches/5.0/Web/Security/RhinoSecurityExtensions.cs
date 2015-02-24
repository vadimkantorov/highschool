using System;
using System.Collections;
using System.Linq;
using Rhino.Security.Interfaces;
using Rhino.Security.Model;

namespace Web.Extensions
{
	public static class RhinoSecurityExtensions
	{
		public static IForPermissionBuilder Allow(this IPermissionsBuilderService pbs, Enum op)
		{
			return pbs.Allow(op.ToOperation());
		}

		public static IForPermissionBuilder Deny(this IPermissionsBuilderService pbs, Enum op)
		{
			return pbs.Deny(op.ToOperation());
		}
		
		public static void AllowOnEverything(this IPermissionsBuilderService pbs, UsersGroup group, params IEnumerable[] ops)
		{
			foreach (var op in ops.SelectMany(x => x.Cast<Enum>()))
				pbs.Allow(op.ToOperation()).For(group).OnEverything().DefaultLevel().Save();
		}

		public static string ToOperation<TOperation>(this TOperation op)
		{
			return string.Format("/{0}/{1}", op.GetType().Name, op);
		}

		public static void Save(this IPermissionBuilder pb, string label)
		{
			var perm = pb.Save();
			perm.Label = label;
		}
	}
}