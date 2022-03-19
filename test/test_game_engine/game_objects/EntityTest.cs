using game_engine.game_objects;
using NUnit.Framework;

namespace test_game_engine.game_objects
{
    class EntityTest
    {
        [SetUp]
        public void Setup() { /* NOOP */ }

        [Test]
        public void createEntity()
        {
            var entity = new Entity("testName", "testType");
            Assert.That(entity.name, Is.EqualTo("testName"));
            Assert.That(entity.type, Is.EqualTo("testType"));
        }

        [Test]
        public void getAttributeEmpty()
        {
            var entity = new Entity("testName", "testType");
            Assert.That(entity.getAttribute("testAttribute"), Is.Null);
        }
        
        [Test]
        public void addAttribute()
        {
            var entity = new Entity("testName", "testType");
            entity.addAttribute(new NumericAttribute("testAttribute", 2.0));
            Assert.That(entity.getAttribute("testAttribute").numVal, Is.EqualTo(2.0));
        }
        
        [Test]
        public void addAttributeWithAliases()
        {
            var entity = new Entity("testName", "testType");
            entity.addAttribute(new NumericAttribute("testAttribute", 2.0), new[] {"alias1", "alias2"});
            Assert.That(entity.getAttribute("alias1").numVal, Is.EqualTo(2.0));
            Assert.That(entity.getAttribute("alias2").numVal, Is.EqualTo(2.0));
        }
    }
}
