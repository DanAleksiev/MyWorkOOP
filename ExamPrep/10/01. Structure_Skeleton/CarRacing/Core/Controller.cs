// ReSharper disable InconsistentNaming
// ReSharper disable FunctionNeverReturns
using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CarRacing.Core
    {
    public class Controller : IController
        {
        private CarRepository cars;
        private RacerRepository racers;
        private IMap map;

        public Controller()
            {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
            }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
            {
            ICar car;
            if (type == nameof(SuperCar))
                {
                car = new SuperCar(make, model, VIN, horsePower);
                }
            else if (type == nameof(TunedCar))
                {
                car = new TunedCar(make, model, VIN, horsePower);
                }
            else
                {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
                }

            cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
            }

        public string AddRacer(string type, string username, string carVIN)
            {
            IRacer racer;
            ICar car = cars.FindBy(carVIN);

            if (car == null)
                {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
                }

            if (type == nameof(StreetRacer))
                {
                racer = new StreetRacer(username, car);
                }
            else if (type == nameof(ProfessionalRacer))
                {
                racer = new ProfessionalRacer(username, car);
                }
            else
                {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
                }

            racers.Add(racer);
            return string.Format(OutputMessages.SuccessfullyAddedRacer,username);
            }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
            {
            IRacer racer1 = racers.FindBy(racerOneUsername);
            IRacer racer2 = racers.FindBy(racerTwoUsername);

            if (racer1 == null)
                {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound,racerOneUsername));
                }
            if (racer2 == null)
                {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
                }
            return map.StartRace(racer1, racer2);
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();

            foreach(IRacer racer in racers.Models.OrderByDescending(x=>x.DrivingExperience).ThenBy(x=>x.Username))
                {
                sb.AppendLine(racer.ToString());
                }

            return sb.ToString().Trim();
            }
        }
    }