using System.IO;
using System.Linq;
using Broker.RSB;
using Castle.MicroKernel;
using Rhino.ServiceBus.Impl;
using Xunit;

namespace Broker.Tests
{
	public class CompressingXmlMessageSerializerTests
	{
		public CompressingXmlMessageSerializerTests()
		{
			serializer = new CompressingXmlMessageSerializer(new DefaultReflection(), new DefaultKernel());
		}


		[Fact]
		public void serialize_actually_compresses()
		{
            var ms = new MemoryStream();
			
			var a_3000 = new string(Enumerable.Repeat('a', 3000).ToArray());
			var data = new object[] {a_3000};

			serializer.Serialize(data, ms);

			Assert.True(ms.Length < a_3000.Length);
		}


		[Fact]
		public void serialize_and_deserialize_are_consistent()
		{
			var ms = new MemoryStream();
			var data = new object[] {"hello"};
			
			serializer.Serialize(data, ms);
			
			ms.Position = 0;
			Assert.Equal(data, serializer.Deserialize(ms));
		}

		CompressingXmlMessageSerializer serializer;
	}
}