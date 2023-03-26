using FrontDeskApp;
using System;
namespace FrontDeskTest
    {
    [TestFixture]
    public class Tests
        {
        [SetUp]
        public void Setup()
            {
            }

        [Test]
        public void testTheConstructor()
            {
            Room room = new Room(21, 61.5);

            Assert.That(room.BedCapacity, Is.EqualTo(21));
            Assert.That(room.PricePerNight, Is.EqualTo(61.5));
            }


        [Test]
        public void BedCapacityIsNegativeOr0ReturnsError()
            {
            Assert.Throws<ArgumentException>(() => new Room(0, 62));
            }


        [Test]
        public void PriceIsNegativeOr0ReturnsError()
            {
            Assert.Throws<ArgumentException>(() => new Room(12, 0));
            }
        }
    }