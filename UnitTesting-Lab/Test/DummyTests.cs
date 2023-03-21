using System;

namespace Test
    {
    [TestFixture]
    public class DummyTests
        {
        private const int healt = 10;
        private const int exp = 10;
        [Test]
        public void CheckIfDummyLosesHealthCorrectly()
            {
            Dummy dummy = new Dummy(healt, exp);

            dummy.TakeAttack(5);

            Assert.That(dummy.Health, Is.EqualTo(5), "Dummy loses health if attacked");
            }  
        
        [Test]
        public void DeadDummyShouldThrowExceptionWhenAttacked()
            {
            Dummy dummy = new Dummy(healt-10, exp);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(5), "Dummy accepts attack when dead.");        
            }
        [Test]
        public void IsDummyAlive()
            {
            Dummy dummy = new Dummy(healt-10, exp);

            Assert.That(dummy.IsDead, "Target is dead.");
            }      
        [Test]
        public void DeadGivesExperienceWhenDies()
            {
            Dummy dummy = new Dummy(healt-10, exp);

            int expectedExp = dummy.GiveExperience();

            Assert.That(expectedExp, Is.EqualTo(10), "Dummy doesn't give the right exp when dead");         
            }
        [Test]
        public void AliveDummyCantGiveExperience()
            {
            Dummy dummy = new Dummy(healt-9, exp);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(),"Alive dummy can give experience");
            }
        }
    }
