using System;
using FrontDeskApp;

namespace FrontDeskApp
    {
    [TestFixture]
    public class BookingTests
        {
        [Test]
        public void testTheConstructor()
            {
            Room room = new Room(1, 50);
            Booking booking = new Booking(123, room, 2);

            Assert.That(booking.BookingNumber, Is.EqualTo(123));
            Assert.That(booking.Room, Is.EqualTo(room));
            Assert.That(booking.ResidenceDuration, Is.EqualTo(2));
            }

        }
    }
