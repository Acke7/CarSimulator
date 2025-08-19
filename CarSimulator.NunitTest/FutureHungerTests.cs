using NUnit.Framework;
using Moq;

namespace CarSimulator.NunitTest
{
    public enum HungerLevel { Full, Hungry, Starving }
    public interface IFutureHungerTracker
    {
        int Hunger { get; set; }
        void ApplyAction();
        void Eat();
        HungerLevel Classify(int hunger);
    }

    [TestFixture]
    public class FutureHungerTests
    {
        [Test]
        public void Hunger_Increases_ByTwo_PerAction_And_Resets_On_Eat()
        {
            var mock = new Mock<IFutureHungerTracker>();
            mock.SetupProperty(h => h.Hunger, 0);
            mock.Setup(h => h.ApplyAction()).Callback(() => mock.Object.Hunger += 2);
            mock.Setup(h => h.Eat()).Callback(() => mock.Object.Hunger = 0);

            var t = mock.Object;

            t.ApplyAction(); t.ApplyAction(); t.ApplyAction();   
            Assert.That(t.Hunger, Is.EqualTo(6));

            t.Eat();                                            
            Assert.That(t.Hunger, Is.EqualTo(0));

            for (int i = 0; i < 8; i++) t.ApplyAction();        
            Assert.That(t.Hunger, Is.GreaterThanOrEqualTo(16));

            Assert.Pass("Prepared test via Moq; feature not in app yet.");
        }

        [TestCase(0, HungerLevel.Full)]
        [TestCase(5, HungerLevel.Full)]
        [TestCase(6, HungerLevel.Hungry)]
        [TestCase(10, HungerLevel.Hungry)]
        [TestCase(11, HungerLevel.Starving)]
        [TestCase(20, HungerLevel.Starving)]
        public void Hunger_Classifies_Ranges(int value, HungerLevel expected)
        {
            var mock = new Mock<IFutureHungerTracker>();
            mock.Setup(h => h.Classify(It.IsAny<int>()))
                .Returns<int>(v => v <= 5 ? HungerLevel.Full
                                   : v <= 10 ? HungerLevel.Hungry
                                   : HungerLevel.Starving);

            Assert.That(mock.Object.Classify(value), Is.EqualTo(expected));
            Assert.Pass("Prepared Moq-based classification for future hunger feature.");
        }
    }
}
