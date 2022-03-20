using System;
using game_engine.game_objects;
using NUnit.Framework;
using Attribute = game_engine.game_objects.Attribute;

namespace test_game_engine.game_objects
{
    class StringAttributeTest
    {
        [SetUp]
        public void Setup() { /* NOOP */ }

        [Test]
        public void createStringAttribute()
        {
            Attribute attr = new StringAttribute("testAttr", "test");

            Assert.That(attr.stringVal, Is.EqualTo("test"));
            Assert.That(attr.objVal, Is.EqualTo("test"));
            Assert.Throws<NotSupportedException>(() => _ = attr.numVal);
            Assert.That(attr.name, Is.EqualTo("testAttr"));
            Assert.That(attr.type, Is.EqualTo(Attribute.AttributeType.TEXT));
        }
    }
}
