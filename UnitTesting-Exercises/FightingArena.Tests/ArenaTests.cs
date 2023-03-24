namespace FightingArena.Tests
    {
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
        {
        [Test]
        public void TestArenaConstructor()
            {
            Arena arena = new Arena();
            arena.Enroll(new Warrior("Grisho", 200, 200));

            Assert.AreEqual(1, arena.Count);
            }
        [Test]
        public void NoDuplicateWarriors()
            {
            string errorMessage = "Warrior is already enrolled for the fights!";
            Arena arena = new Arena();

            arena.Enroll(new Warrior("Grisho", 200, 200));
            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => arena.Enroll(new Warrior("Grisho", 200, 200)));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void FightException()
            {
            string missingName = "Peshkata";
            string errorMessage = $"There is no fighter with name {missingName} enrolled for the fights!";

            Warrior compatitor1 = new Warrior("Jorko", 200, 200);
            Warrior compatitor2 = new Warrior("Nikifor", 200, 200);
            Arena arena = new Arena();
            arena.Enroll(compatitor1);
            arena.Enroll(compatitor2);

            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => arena.Fight(missingName, compatitor1.Name));
            Assert.AreEqual(ex.Message, errorMessage);

            InvalidOperationException exDeff = Assert
                .Throws<InvalidOperationException>(() => arena.Fight(compatitor1.Name, missingName));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void FightTesting()
            {
            Arena arena = new Arena();
            Warrior player = new Warrior("Krum", 30, 200);
            Warrior enemy = new Warrior("Cesar", 100, 31);
            arena.Enroll(player);
            arena.Enroll(enemy);
            arena.Fight(player.Name, enemy.Name);
            Assert.AreEqual(1, enemy.HP);
            }
        }
    }
