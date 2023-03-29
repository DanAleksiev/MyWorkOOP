using NUnit.Framework;
using System;
using System.Numerics;

namespace PlanetWars.Tests
    {
    public class Tests
        {
        [TestFixture]
        public class WeaponsTests
            {
            [Test]
            public void TestWeaponsConstructor()
                {
                string name = "Greta";
                double price = 2.5;
                int destructionLVL = 100;
                Weapon weapon = new Weapon(name, price, destructionLVL);

                Assert.AreEqual(name, weapon.Name);
                Assert.AreEqual(price, weapon.Price);
                Assert.AreEqual(destructionLVL, weapon.DestructionLevel);
                }

            [Test]
            public void TestWeaponsPriceIfCanBeANegativeValue()
                {
                string name = "Greta";
                double price = -1;
                int destructionLVL = 100;

                string errorMessage = "Price cannot be negative.";

                ArgumentException ex = Assert
                    .Throws<ArgumentException>(() => new Weapon(name, price, destructionLVL));
                Assert.That(ex.Message, Is.EqualTo(errorMessage));
                // same thing!??!?!
                //Assert.Throws<ArgumentException>(() => new Weapon(name, price, destructionLVL));
                }

            [Test]
            public void TestWeaponsDestructionLVLIncreaseCommand()
                {
                string name = "Greta";
                double price = 12;
                int destructionLVL = 100;
                Weapon weapon = new Weapon(name, price, destructionLVL);
                weapon.IncreaseDestructionLevel();
                Assert.That(destructionLVL+1, Is.EqualTo(weapon.DestructionLevel));
                }

            [Test]
            public void TestWeaponsIsTheWeaponNuclear()
                {
                string name = "Greta";
                double price = 12;
                int destructionLVL = 100;
                Weapon weapon = new Weapon(name, price, destructionLVL);
                Assert.That(weapon.IsNuclear, Is.EqualTo(true));
                }

            [Test]
            public void TestWeaponsIsTheWeaponNotNucler()
                {
                string name = "Greta";
                double price = 9;
                int destructionLVL = 9;
                Weapon weapon = new Weapon(name, price, destructionLVL);

                Assert.That(weapon.IsNuclear, Is.EqualTo(false));
                }
            }
        }
    }
