namespace Robots.Tests
    {
    using NUnit.Framework;
    using System;
    using System.Diagnostics;

    [TestFixture]
    public class RobotsTests
        {
        private string name = "Wolli";
        private int maxBattery = 1;

        [Test]
        public void testConstRobot()
            {
            Robot robot = new Robot(name,maxBattery);

            Assert.AreEqual( name, robot.Name);
            Assert.AreEqual( maxBattery, robot.Battery);
            Assert.AreEqual( maxBattery, robot.MaximumBattery);
            }

        [Test]
        public void testConstRobotManager()
            {
            RobotManager manager = new RobotManager(23);
            Assert.AreEqual(23, manager.Capacity);

            }
        [Test]
        public void RobotManagerThrowExWhenCountIsLessThen0()
            {
            string errorMessage = "Invalid capacity!";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new RobotManager(-1));
            Assert.AreEqual(ex.Message, errorMessage);

            RobotManager manager = new RobotManager(3);
            Assert.AreEqual(3, manager.Capacity);
            }

        [Test]
        public void RobotManagerAddRobot()
            {
            Robot robot = new Robot(name, maxBattery);
            Robot robot1 = new Robot($"{name}1", maxBattery);
            Robot robot2 = new Robot($"{name}2", maxBattery);
            Robot robot3 = new Robot($"{name}3", maxBattery);

            RobotManager manager = new RobotManager(3);
            manager.Add(robot);

            string errorMessage = $"There is already a robot with name {robot.Name}!";
            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => manager.Add(robot));
            Assert.AreEqual(ex.Message, errorMessage);

            manager.Add(robot1);
            manager.Add(robot2);

            string errorMessageMoreThenCap = "Not enough capacity!";
            InvalidOperationException exCap = Assert
                .Throws<InvalidOperationException>(() => manager.Add(robot3));
            Assert.AreEqual(exCap.Message, errorMessageMoreThenCap);

            Assert.AreEqual(3, manager.Count);
            }
        [Test]
        public void RobotManagerRemoveRobot()
            {
            Robot robot = new Robot(name, maxBattery);

            RobotManager manager = new RobotManager(3);
            manager.Add(robot);

            string errorMessage = $"Robot with the name Pesho doesn't exist!";
            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => manager.Remove("Pesho"));
            Assert.AreEqual(ex.Message, errorMessage);
            manager.Remove(robot.Name);

            Assert.AreEqual(0, manager.Count);
            }

        [Test]
        public void RobotManagerWorkRobot()
            {
            Robot robot = new Robot(name, maxBattery);

            RobotManager manager = new RobotManager(3);

            string errorMessage = $"Robot with the name {name} doesn't exist!";
            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => manager.Work(name, "Strugar", 1));
            Assert.AreEqual(ex.Message, errorMessage);

            manager.Add(robot);

            string errorMessageOverWork = $"{robot.Name} doesn't have enough battery!";
            InvalidOperationException exOverwork = Assert
                .Throws<InvalidOperationException>(() => manager.Work(name, "Strugar", 2));
            Assert.AreEqual(ex.Message, errorMessage);
            }


        [Test]
        public void RobotManagerChargeRobot()
            {
            Robot robot = new Robot(name, maxBattery);

            RobotManager manager = new RobotManager(3);

            string errorMessage = $"Robot with the name {robot.Name} doesn't exist!";
            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => manager.Charge(name));
            Assert.AreEqual(ex.Message, errorMessage);

            manager.Add(robot);
            manager.Charge(name);
            Assert.AreEqual(maxBattery, robot.Battery);
            }
        }
    }
