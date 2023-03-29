using System.Diagnostics;
using System.Xml.Linq;

namespace BluePritsForUnitTesting
    {
    [TestFixture]
    public class Tests
        {
        [SetUp]
        public void Setup()
            {
            }

        [Test]
        public void Test1()
            {
            string name = "Greta";
            double price = 2.5;
            int destructionLVL = 100;

            string errorMessage = "Price cannot be negative.";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new weapon = new(name, price, destructionLVL));
            Assert.AreEqual(ex.Message, errorMessage);

            // same thing!??!?!
            //Assert.Throws<ArgumentException>(() => new Weapon(name, price, destructionLVL));
            }

        [Test]
        public void TestWeaponsConstructor()
            {
            string name = "Greta";
            double price = 2.5;
            int destructionLVL = 100;
            weapon = new (name, price, destructionLVL);

            ////////
            Assert.That(weapon.Name, Is.EqualTo(name));
            // same thing as
            Assert.AreEqual(name, weapon.Name);
            ////////
            
            Assert.AreEqual(price, weapon.Price);
            Assert.AreEqual(destructionLVL, weapon.DestructionLevel);
            }
        }
    }