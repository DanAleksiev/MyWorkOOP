using FrontDeskApp;
using System;
namespace FrontDeskTest
    {
    [TestFixture]
    public class HotelTests
        {
        [Test]
        public void testTheConstructor()
            {
            Hotel hotel = new Hotel("Sheraton", 3);

            Assert.That(hotel.FullName, Is.EqualTo("Sheraton"));
            Assert.That(hotel.Category, Is.EqualTo(3));
            }


        [Test]
        public void NameCantBeNullOrEmpty()
            {
            Assert.Throws<ArgumentNullException>(() => new Hotel(null, 3));
            Assert.Throws<ArgumentNullException>(() => new Hotel("", 3));
            }


        [Test]
        public void CategoryIsnotLessThen1OrMoreThen5ReturnsError()
            {
            Assert.Throws<ArgumentException>(() => new Hotel("Sheraton", 0));
            Assert.Throws<ArgumentException>(() => new Hotel("Sheraton", 6));
            }

        [Test]
        public void BookAroomWithoutAdouuts()
            {
            Room room = new Room(3, 69);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);
            
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 2, 1, 70));
            }

        [Test]
        public void BookAroomWithoutWhenChildrenAreNegative()
            {
            Room room = new Room(3, 69);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -1, 1, 70));
            }

        [Test]
        public void BookAroomWithoutWhenYouStayLessThen1Day()
            {
            Room room = new Room(3, 69);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 0, 0, 70));
            }

        [Test]
        public void BookAroomAndCheckTheTurnover()
            {
            Room room = new Room(3, 30);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);
            hotel.BookRoom(2, 0, 2, 70);
            Assert.That(hotel.Turnover, Is.EqualTo(60));
            }

        [Test]
        public void BookAroomIfCapacity()
            {
            Room room = new Room(1, 30);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);
            hotel.BookRoom(2, 0, 2, 70);
            Assert.That(hotel.Turnover, Is.EqualTo(0));
            }

        [Test]
        public void TurnoverShouldBe0IfNoRoomWasFound()
            {
            Room room = new Room(3, 71);
            Hotel hotel = new Hotel("Sheraton", 3);
            hotel.AddRoom(room);
            hotel.BookRoom(2, 0, 2, 70);
            Assert.That(hotel.Turnover, Is.EqualTo(0));
            }
        }
    }
