using Rhino.ServiceBus.DataStructures;

namespace Web.Services
{
	public interface ICache<TKey, TValue>
	{
		TValue Get(TKey key);
		void Put(TKey key, TValue value);
	}

	class Cache<TKey, TValue> : ICache<TKey, TValue>
	{
		public TValue Get(TKey id)
		{
			TValue monitor = default(TValue);
			monitors.Read(r => r.TryGetValue(id, out monitor));
			return monitor;
		}

		public void Put(TKey id, TValue monitor)
		{
			monitors.Write(w => w.Add(id, monitor));
		}
		readonly Hashtable<TKey, TValue> monitors = new Hashtable<TKey, TValue>();
	}
}