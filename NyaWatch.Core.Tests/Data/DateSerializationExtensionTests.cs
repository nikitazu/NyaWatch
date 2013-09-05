using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NyaWatch.Core.Data.Tests
{
    [TestFixture]
    public class DateSerializationExtensionTests
    {
        [Test]
        public void TestSerializeDate()
        {
            DateTime? nulldate = null;
            DateTime? somedate = new DateTime?(new DateTime(2010, 10, 3));

            Assert.AreEqual(string.Empty, nulldate.SerializeDate(), "Serialize to empty not working");
            Assert.AreEqual("2010-10-03", somedate.SerializeDate(), "Serialize date not working");
        }

        [Test]
        public void TestDeserializeDate()
        {
            Assert.IsNull(string.Empty.DeserializeDate(), "Deserialize empty not working");
            Assert.AreEqual(new DateTime(2010, 10, 3), "2010-10-03".DeserializeDate(), "Deserialize date not working");
        }
    }
}
