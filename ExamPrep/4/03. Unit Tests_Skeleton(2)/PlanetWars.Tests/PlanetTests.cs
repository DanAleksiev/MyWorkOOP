using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace PlanetWars.Tests
    {
    [TestFixture]
    public class PlanetTests
        {

        [Test]
        public void PLanetConstructor()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Assert.AreEqual(name, planet.Name);
            Assert.AreEqual(budget, planet.Budget);
            }

        [Test]
        public void NameCantBeNullOrEmpty()
            {
            string name = "NotEarth";
            double budget = 123;

            string errorMessage = "Invalid planet Name";

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Planet(string.Empty, budget));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));


            ArgumentException exNull = Assert
                .Throws<ArgumentException>(() => new Planet(null, budget));
            Assert.That(exNull.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void PlanetBudgetCantBeNegtive()
            {
            string name = "NotEarth";
            double budget = -123;

            string errorMessage = "Budget cannot drop below Zero!";

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Planet(name, budget));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void PlanetSpendAcuracy()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            planet.SpendFunds(23);
            Assert.That(planet.Budget, Is.EqualTo(100));
            }

        [Test]
        public void PlanetProfits()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            planet.Profit(27);
            Assert.That(planet.Budget, Is.EqualTo(150));
            }

        [Test]
        public void PlanetSpendMoreThenItHas()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);

            string errorMessage = "Not enough funds to finalize the deal.";

            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => planet.SpendFunds(124));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void AddWeaponsToTheArsenalOfThePLanet()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);

            planet.AddWeapon(weapon);
            Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }

        [Test]
        public void RemoveWeaponsToTheArsenalOfThePLanet()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);

            planet.AddWeapon(weapon);
            planet.RemoveWeapon("Greta");

            Assert.That(planet.Weapons.Count, Is.EqualTo(0));
            }

        [Test]
        public void PlanetUpdateWeaponsThatYouDontHave()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);
            planet.AddWeapon(weapon);

            string errorMessage = $"Berta does not exist in the weapon repository of {planet.Name}";

            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => planet.UpgradeWeapon("Berta"));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void PlanetUpdateWeaponsSuccessfull()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon(weapon.Name);
            Assert.AreEqual(10, planet.MilitaryPowerRatio);

            }

        [Test]
        public void SucssesfulyDestroyAnOponent()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);
            planet.AddWeapon(weapon);

            Planet planet2 = new Planet("Earth", budget);
            Weapon weapon2 = new Weapon("Greta", 12.30, 9);
            planet2.AddWeapon(weapon2);
            planet2.UpgradeWeapon(weapon.Name);

            string resul = planet2.DestructOpponent(planet);
            string expected = $"{planet.Name} is destructed!";
            Assert.That(expected, Is.EqualTo(resul));
            }

        [Test]
        public void UnSucssesfulyDestroyAnOponent()
            {
            string name = "NotEarth";
            double budget = 123;
            Planet planet = new Planet(name, budget);
            Weapon weapon = new Weapon("Greta", 12.30, 9);
            planet.AddWeapon(weapon);
            planet.UpgradeWeapon(weapon.Name);

            Planet planet2 = new Planet("Earth", budget);
            Weapon weapon2 = new Weapon("Greta", 12.30, 9);
            planet2.AddWeapon(weapon2);

            string errorMessage = $"{planet.Name} is too strong to declare war to!";

            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => planet2.DestructOpponent(planet));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        }
    }

