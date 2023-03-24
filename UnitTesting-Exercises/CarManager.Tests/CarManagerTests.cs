namespace CarManager.Tests
    {
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
        {
        [Test]
        public void TestTheCarConstructor()
            {
            Car[] car =
                {
                new Car("opel","astra",23.3,250)
                };
            Assert.AreEqual(1, car.Length);
            Assert.AreEqual("opel", car[0].Make);
            Assert.AreEqual("astra", car[0].Model);
            Assert.AreEqual(23.3, car[0].FuelConsumption);
            Assert.AreEqual(250, car[0].FuelCapacity);
            Assert.AreEqual(0, car[0].FuelAmount);
            }

        [Test]
        public void IfMakeIsNullOrEmptyThrowException()
            {
            string errorMessage = "Make cannot be null or empty!";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Car("", "astra", 23.3, 250));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));

            ArgumentException exNull = Assert
                .Throws<ArgumentException>(() => new Car(null, "astra", 23.3, 250));
            Assert.That(exNull.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void IfModelIsNullOrEmptyThrowException()
            {
            string errorMessage = "Model cannot be null or empty!";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Car("opel", "", 23.3, 250));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));

            ArgumentException exNull = Assert
                .Throws<ArgumentException>(() => new Car("ople", null, 23.3, 250));
            Assert.That(exNull.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void FuelConsumtionCantBeNegative()
            {
            string errorMessage = "Fuel consumption cannot be zero or negative!";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Car("opel", "astra", -12, 250));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void FuelCapacityCantBeNegative()
            {
            string errorMessage = "Fuel capacity cannot be zero or negative!";
            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => new Car("opel", "astra", 12, -250));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }

        [Test]
        public void IfRefuelingMethodWorks()
            {
            Car car = new Car("opel", "astra", 12, 250);
            car.Refuel(12);
            Assert.AreEqual(12, car.FuelAmount);
            }  
        
        [Test]
        public void YouShouldntBeAbleToOverfillTheThank()
            {
            Car car = new Car("opel", "astra", 12, 250);
            car.Refuel(260);
            Assert.AreEqual(250, car.FuelAmount);
            }

        [Test]
        public void RefuelCantBeNegative()
            {
            string errorMessage = "Fuel amount cannot be zero or negative!";
            Car car = new Car("opel", "astra", 12, 250);

            ArgumentException ex = Assert
                .Throws<ArgumentException>(() => car.Refuel(-123));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }  
        
        [Test]
        public void IfDryvingMethodWorks()
            {
            Car car = new Car("opel", "astra", 15, 250);
            car.Refuel(100);
            car.Drive(10);
            Assert.AreEqual(98.5, car.FuelAmount);
            }

        [Test]
        public void IfFuelIsNotEnoughThrow()
            {
            string errorMessage = "You don't have enough fuel to drive!";
            Car car = new Car("opel", "astra", 15, 250);
            car.Refuel(1);

            InvalidOperationException ex = Assert
                .Throws<InvalidOperationException>(() => car.Drive(10));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
            }
        }
    }