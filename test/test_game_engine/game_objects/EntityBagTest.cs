using game_engine.game_objects;
using Moq;
using NUnit.Framework;

namespace test_game_engine.game_objects
{
    public class EntityBagTest
    {
        private EntityBag entityBag;

        [SetUp]
        public void Setup()
        {
            entityBag = new EntityBag();
        }

        [Test]
        public void addEntity()
        {
            var testEntity = new Entity("testName", "testType");
            entityBag.addEntity(testEntity);
            Assert.That(entityBag.getEntity("testName"), Is.EqualTo(testEntity));
        }
        
        [Test]
        public void addEntityWithAliases()
        {
            var testEntity = new Entity("testName", "testType");
            entityBag.addEntity(testEntity, new[] {"alias1", "alias2"});
            Assert.That(entityBag.getEntity("alias1"), Is.EqualTo(testEntity));
            Assert.That(entityBag.getEntity("alias2"), Is.EqualTo(testEntity));
        }
    }
}
