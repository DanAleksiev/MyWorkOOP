namespace FightingArena.Tests
    {
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
        {
        [Test]
        public void TestWarriorConstructor()
            {
            Warrior[] warrior = { new Warrior("Pepin", 420, 69) };

            Assert.AreEqual(1, warrior.Length);
            Assert.AreEqual("Pepin", warrior[0].Name);
            Assert.AreEqual(420, warrior[0].Damage);
            Assert.AreEqual(69, warrior[0].HP);
            }

        [Test]
        public void NameOfWarriorCantBeNullOrEmpty()
            {
            string errorMessage = "Name should not be empty or whitespace!";
            string name = "Pepin";
            int damage = 420;
            int hp = 69;

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Warrior("", damage, hp));
            Assert.AreEqual(ex.Message, errorMessage); 

            ArgumentException exNull = Assert
                .Throws<ArgumentException>(() => new Warrior(null, damage, hp));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void DamageOfWarriorCantBeLessTanZero()
            {
            string errorMessage = "Damage value should be positive!";
            string name = "Pepin";
            int damage = 420;
            int hp = 69;

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Warrior(name, -1, hp));
            Assert.AreEqual(ex.Message, errorMessage);
            }    

        [Test]
        public void HPOfWarriorCantBeLessTanZero()
            {
            string errorMessage = "HP should not be negative!";
            string name = "Pepin";
            int damage = 420;
            int hp = 69;

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Warrior(name, damage, -1));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void AttackWithLowHp()
            {
            string errorMessage = "Your HP is too low in order to attack other warriors!";
            Warrior player = new Warrior("Krum", 5000, 29);
            Warrior enemy = new Warrior("Cesar", 50, 200);


            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(()=> player.Attack(enemy));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void AttackEnemyWithLowHp()
            {
            string errorMessage = $"Enemy HP must be greater than 30 in order to attack him!";
            Warrior player = new Warrior("Krum", 5000, 200);
            Warrior enemy = new Warrior("Cesar", 50, 29);


            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => player.Attack(enemy));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void AttackEliteEnemy()
            {
            string errorMessage = "You are trying to attack too strong enemy";
            Warrior player = new Warrior("Krum", 5000, 200);
            Warrior enemy = new Warrior("Cesar", 201, 300);


            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => player.Attack(enemy));
            Assert.AreEqual(ex.Message, errorMessage);
            }

        [Test]
        public void AttackAndKillEnemy()
            {
            
            Warrior player = new Warrior("Krum", 33, 200);
            Warrior enemy = new Warrior("Cesar", 100, 31);


            player.Attack(enemy);
            Assert.AreEqual(0,enemy.HP);
            }   
        
        [Test]
        public void EnemyTakesAHitAndDonsntDie()
            {
            
            Warrior player = new Warrior("Krum", 30, 200);
            Warrior enemy = new Warrior("Cesar", 100, 31);


            player.Attack(enemy);
            Assert.AreEqual(1,enemy.HP);
            }
        }
    }