using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NyaWatch.Core.Data.Tests
{
    [TestFixture]
    public class DictionaryDataExtensionTests
    {
        IDictionary<string, string> _item;
        IDictionary<string, string> _nullItem;

        [SetUp]
        public void Setup ()
        {
            _item = new Dictionary<string, string> ();
            _item["name"] = "Alex";
            _item["age"] = "25";
            _item["isMale"] = "true";
            _item["today"] = DateTime.Today.ToString (DictionaryDataExtension.DateFormat);
            _item["nulldate"] = null;
            _item["emptydate"] = string.Empty;
            _nullItem = null;
        }

        [TearDown]
        public void Teardown ()
        {
            _item = null;
            _nullItem = null;
        }

        //
        // Require data
        //

        [Test]
        public void TestRequireData ()
        {
            Assert.AreEqual ("Alex", _item.RequireData ("name"));
            Assert.Throws<KeyNotFoundException> (() => _item.RequireData ("noname"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.RequireData ("name"));
            Assert.Throws<ArgumentNullException> (() => _item.RequireData (null));
        }

        [Test]
        public void TestRequireInt ()
        {
            Assert.AreEqual (25, _item.RequireInt ("age"));
            Assert.Throws<KeyNotFoundException> (() => _item.RequireInt ("noage"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.RequireInt ("age"));
            Assert.Throws<ArgumentNullException> (() => _item.RequireInt (null));
            Assert.Throws<FormatException> (() => _item.RequireInt ("name"));
        }

        [Test]
        public void TestRequireBool ()
        {
            Assert.IsTrue (_item.RequireBool ("isMale"));
            Assert.Throws<KeyNotFoundException> (() => _item.RequireBool ("noIsMale"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.RequireBool ("isMale"));
            Assert.Throws<ArgumentNullException> (() => _item.RequireBool (null));
            Assert.Throws<FormatException> (() => _item.RequireBool ("name"));
        }

        [Test]
        public void TestRequireDate ()
        {
            Assert.AreEqual (DateTime.Today, _item.RequireDate ("today"));
            Assert.Throws<KeyNotFoundException> (() => _item.RequireDate ("notoday"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.RequireDate ("today"));
            Assert.Throws<ArgumentNullException> (() => _item.RequireDate (null));
            Assert.Throws<FormatException> (() => _item.RequireDate ("name"));
        }

        //
        //  Optional data
        //

        [Test]
        public void TestOptionalData ()
        {
            Assert.AreEqual ("Alex", _item.OptionalString ("name"));
            Assert.IsNull (_item.OptionalString ("noname"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.OptionalString ("name"));
            Assert.Throws<ArgumentNullException> (() => _item.OptionalString (null));
        }

        [Test]
        public void TestOptionalInt ()
        {
            Assert.AreEqual (25, _item.OptionalInt ("age"));
            Assert.IsNull (_item.OptionalInt ("noage"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.OptionalInt ("age"));
            Assert.Throws<ArgumentNullException> (() => _item.OptionalInt (null));
            Assert.Throws<FormatException> (() => _item.OptionalInt ("name"));
        }

        [Test]
        public void TestOptionalBool ()
        {
            Assert.AreEqual (true, _item.OptionalBool ("isMale"));
            Assert.IsNull (_item.OptionalBool ("noIsMale"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.OptionalBool ("isMale"));
            Assert.Throws<ArgumentNullException> (() => _item.OptionalBool (null));
            Assert.Throws<FormatException> (() => _item.OptionalBool ("name"));
        }

        [Test]
        public void TestOptionalDate ()
        {
            Assert.AreEqual (DateTime.Today, _item.OptionalDate ("today"));
            Assert.IsNull (_item.OptionalDate ("notoday"));
            Assert.IsNull (_item.OptionalDate ("nulldate"));
            Assert.IsNull (_item.OptionalDate ("emptydate"));
            Assert.Throws<ArgumentNullException> (() => _nullItem.OptionalDate ("today"));
            Assert.Throws<ArgumentNullException> (() => _item.OptionalDate (null));
        }
    }
}
