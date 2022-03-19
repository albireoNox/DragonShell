using game_engine.game_objects;
using NUnit.Framework;

namespace test_game_engine.game_objects
{
    class NumericAttributeTest
    {
        [SetUp]
        public void Setup() { /* NOOP */ }

        [Test]
        public void createNumericAttribute()
        {
            Attribute attr = new NumericAttribute("testAttr", 2.0);

            Assert.That(attr.numVal, Is.EqualTo(2.0));
            Assert.That(attr.objVal, Is.EqualTo(2.0));
            Assert.That(attr.name, Is.EqualTo("testAttr"));
            Assert.That(attr.type, Is.EqualTo(Attribute.AttributeType.NUM));
        }
    }
}
