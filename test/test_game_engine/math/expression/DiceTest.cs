using System;
using game_engine.math.expression;
using Moq;
using NUnit.Framework;

namespace test_game_engine.math.expression
{
    public class DiceTest
    {
        private Mock<Random> random;
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            random = new Mock<Random>();
            dice = new Dice(random.Object);
        }

        [TestCase("2d6")]
        [TestCase("d20")]
        [TestCase("3d4")]
        public void IsDiceToken(string token)
        {
            Assert.That(dice.isDiceToken(token), Is.True);
        }

        [TestCase("10")]
        [TestCase("2a20")]
        [TestCase("abc")]
        [TestCase("")]
        [TestCase(null)]
        public void IsNotDiceToken(string token)
        {
            Assert.That(dice.isDiceToken(token), Is.False);
        }

        [TestCase("abcd")]
        [TestCase("d20a")]
        [TestCase("a4d20")]
        [TestCase("d20d5")]
        [TestCase("d")]
        [TestCase("")]
        [TestCase("5d")]

        public void InvalidTokenFormats(string token)
        {
            Assert.That(() => dice.roll(token), Throws.TypeOf<FormatException>());
        }

        [Test]
        public void NullToken()
        {
            Assert.That(() => dice.roll(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void RollD20()
        {
            random.Setup(x => x.Next(1, 21)).Returns(5);
            Assert.That(dice.roll("d20"), Is.EqualTo(5));
        }

        [Test]
        public void Roll1D20()
        {
            random.Setup(x => x.Next(1, 21)).Returns(5);
            Assert.That(dice.roll("1d20"), Is.EqualTo(5));
        }

        [Test]
        public void Roll3D20()
        {
            random.SetupSequence(x => x.Next(1, 21))
                .Returns(1)
                .Returns(2)
                .Returns(3);
            Assert.That(dice.roll("3d20"), Is.EqualTo(6));
        }
    }
}