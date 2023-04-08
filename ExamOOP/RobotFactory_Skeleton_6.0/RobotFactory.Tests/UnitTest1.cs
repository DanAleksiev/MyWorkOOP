using NUnit.Framework;
using System.Xml.Linq;
using System;

namespace RobotFactory.Tests
{
    [TestFixture]
    public class Tests
    {
        private string model = "MX-20";
        private double price = 200;
        private int interfaceStandart = 12;
        private int capacity = 1;

        [Test]
        public void RobotConstructor()
        {
            Robot robot = new Robot(model, price, interfaceStandart);

            Assert.AreEqual(model, robot.Model);
            Assert.AreEqual(price, robot.Price);
            Assert.AreEqual(interfaceStandart, robot.InterfaceStandard);
            Assert.AreEqual(0, robot.Supplements.Count);




            string expectedResult = robot.ToString();
            Assert.That(robot.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void SupplementsConstructor()
            {
            Supplement sup = new Supplement(model, interfaceStandart);

            Assert.That(sup.Name, Is.EqualTo(model));
            Assert.That(sup.InterfaceStandard, Is.EqualTo(interfaceStandart));

            string expectedResult = sup.ToString();
            Assert.That(sup.ToString(), Is.EqualTo(expectedResult));
            }

        [Test]
        public void FactoryConstructor()
            {
            Factory fac = new Factory(model, capacity);

            Assert.That(fac.Name, Is.EqualTo(model));
            Assert.That(fac.Capacity, Is.EqualTo(capacity));
            Assert.That(fac.Robots.Count, Is.EqualTo(0));
            Assert.That(fac.Supplements.Count, Is.EqualTo(0));
           }

        [Test]
        public void ProduceRobot()
            {
            Factory fac = new Factory(model, capacity);
            Robot robot = new Robot(model, price, interfaceStandart);

            string errorMessage = $"Produced --> {robot}";
            string result = fac.ProduceRobot(model, price, interfaceStandart);
            Assert.That(errorMessage, Is.EqualTo(result));
            Assert.That(fac.Robots.Count, Is.EqualTo(1));

            string uns = "The factory is unable to produce more robots for this production day!";
            string unsucsses = fac.ProduceRobot(model, price, interfaceStandart);
            Assert.That(errorMessage, Is.EqualTo(result));
            Assert.That(fac.Robots.Count, Is.EqualTo(1));
            }

        [Test]
        public void ProduceSuplements()
            {
            Factory fac = new Factory(model, capacity);
            Supplement sup = new Supplement(model, interfaceStandart);
            string result = fac.ProduceSupplement(model, interfaceStandart);
            Assert.That(result, Is.EqualTo(sup.ToString()));
            Assert.That(fac.Supplements.Count, Is.EqualTo(1));
            }

        [Test]
        public void UpdateRobot()
            {
            Supplement sup = new Supplement(model, interfaceStandart);
            Robot robot = new Robot(model, price, interfaceStandart);
            
            Factory fac = new Factory(model, capacity);

            bool result = fac.UpgradeRobot(robot, sup);
            Assert.That(result, Is.EqualTo(true));

            fac.ProduceSupplement(model, interfaceStandart);
            fac.ProduceRobot(model, price, interfaceStandart);

            result = fac.UpgradeRobot(robot , sup);
            Assert.That(result, Is.EqualTo(false));
            }

        [Test]
        public void SellRobot()
            {
            Supplement sup = new Supplement(model, interfaceStandart);
            Robot robot = new Robot(model, price, interfaceStandart);
            Factory fac = new Factory(model, capacity);

            fac.ProduceRobot(model, price, interfaceStandart);

            Robot result = fac.SellRobot(200);
            Assert.That(result.ToString(), Is.EqualTo(robot.ToString()));

            Robot sellNOtFound = fac.SellRobot(100);

            Assert.That(sellNOtFound, Is.EqualTo(null));
            }
        }
}