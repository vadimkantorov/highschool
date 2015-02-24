using System.IO;
using System.IO.Compression;
using Castle.MicroKernel;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Serializers;

namespace Broker.RSB
{
	public class CompressingXmlMessageSerializer : IMessageSerializer
	{
		public void Serialize(object[] messages, Stream message)
		{
			using(var gzip = new GZipStream(message, CompressionMode.Compress, true))
			    inner.Serialize(messages, gzip);
		}

		public object[] Deserialize(Stream message)
		{
			using(var gzip = new GZipStream(message, CompressionMode.Decompress, true))
			    return inner.Deserialize(gzip);
		}

		public CompressingXmlMessageSerializer(IReflection reflection, IKernel kernel)
		{
			inner = new XmlMessageSerializer(reflection, kernel);
		}

		readonly XmlMessageSerializer inner;
	}
}