using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookingApp.Core
    {
    public class Controller : IController
        {
        private readonly IRepository<IHotel> hotels;
        public Controller()
            {
            this.hotels = new HotelRepository();
            }
        public string AddHotel(string hotelName, int category)
            {
            IHotel hotel = new Hotel(hotelName, category);
            IHotel checkForHotels =
                (IHotel)hotels.All()
                .FirstOrDefault(x => x.FullName == hotelName && x.Category == category);
            if (checkForHotels == null)
                {
                hotels.AddNew(hotel);
                return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
                }
            else
                {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
                }
            }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
            {
            var orderedHotel = hotels.All()
                .Where(h => h.Category == category)
                .OrderBy(h => h.Turnover)
                .ThenBy(x => x.FullName);

            if (orderedHotel == null)
                {
                return string.Format(OutputMessages.CategoryInvalid, category);
                }

            foreach (var hotel in orderedHotel)
                {
                var selectRoom = hotel.Rooms.All()
                    .Where(r => r.PricePerNight > 0)
                    .OrderBy(r => r.BedCapacity >= adults + children)
                    .OrderBy(r => r.BedCapacity).FirstOrDefault();

                if (selectRoom != null)
                    {
                    int bookingNum = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectRoom, duration, adults, children, bookingNum);
                    hotel.Bookings.AddNew(booking);
                    return string.Format(OutputMessages.BookingSuccessful, bookingNum, hotel.FullName);
                    }

                }
            return string.Format(OutputMessages.RoomNotAppropriate);
            }

        public string HotelReport(string hotelName)
            {
            StringBuilder sb = new StringBuilder();
            var hotel = hotels.Select(hotelName);
            if (hotel == null)
                {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
                }
            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine($"--Bookings:");
            if (hotel.Bookings.All().Count != 0)
                {
                foreach (var booking in hotel.Bookings.All())
                    {
                    sb.AppendLine($"Booking number: {booking.BookingSummary()}");
                    sb.AppendLine();
                    }
                }
            else
                {
                sb.AppendLine("none");
                }

            return sb.ToString().Trim();
            }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
            {
            if (hotels.All().FirstOrDefault(h => h.FullName == hotelName) == default)
                {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
                }

            if (roomTypeName != nameof(Apartment) &&
                roomTypeName != nameof(DoubleBed) &&
                roomTypeName != nameof(Studio))
                {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
                }

            IHotel hotel = hotels.Select(hotelName);
            if (hotel.Rooms.Select(roomTypeName) == null)
                {
                return string.Format(OutputMessages.RoomTypeNotCreated);
                }

            if (hotel.Turnover != 0)
                {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
                }

            hotel.Rooms.Select(roomTypeName).SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
            }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
            {
            IHotel checkForHotels = hotels.Select(hotelName);
            if (checkForHotels == null)
                {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
                }
            if (checkForHotels.Rooms.Select(roomTypeName) != null)
                {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
                }

            IRoom room;
            if (roomTypeName == nameof(Apartment))
                {
                room = new Apartment();
                }
            else if (roomTypeName == nameof(DoubleBed))
                {
                room = new DoubleBed();
                }
            else if (roomTypeName == nameof(Studio))
                {
                room = new Studio();
                }
            else
                {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
                }

            checkForHotels.Rooms.AddNew(room);
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
            }
        }
    }