using System;

namespace Model
{
	public class Entity
	{
		public int Id { get; private set; }
		public int Version { get; private set; }

		public Guid SecurityKey { get; private set; }

		public Entity()
		{
			SecurityKey = Guid.NewGuid();
		}
	}
}
