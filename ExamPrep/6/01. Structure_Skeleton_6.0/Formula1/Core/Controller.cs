using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Formula1.Core
    {
    public class Controller : IController
        {
        private PilotRepository pilots;
        private FormulaOneCarRepository cars;
        private RaceRepository races;

        public Controller()
            {
            this.pilots = new PilotRepository();
            this.cars = new FormulaOneCarRepository();
            this.races = new RaceRepository();
            }

        public string CreatePilot(string fullName)
            {

            if (pilots.FindByName(fullName) != null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
                }

            IPilot pilot = new Pilot(fullName);
            pilots.Add(pilot);

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
            }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
            {

            if (cars.FindByName(model) != null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
                }
            if (type != nameof(Ferrari) &&
                type != nameof(Williams))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
                }
            IFormulaOneCar car = null;
            if (type == nameof(Ferrari))
                {
                car = new Ferrari(model, horsepower, engineDisplacement);
                }
            if (type == nameof(Williams))
                {
                car = new Williams(model, horsepower, engineDisplacement);
                }

            cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
            }

        public string CreateRace(string raceName, int numberOfLaps)
            {
            if (races.FindByName(raceName) != null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
                }
            IRace race = new Race(raceName, numberOfLaps);
            races.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
            }

        public string AddCarToPilot(string pilotName, string carModel)
            {
            IFormulaOneCar car = cars.FindByName(carModel);
            IPilot pilot = pilots.FindByName(pilotName);

            if (pilot == null || pilot.Car != null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
                }
            if (car == null)
                {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
                }

            pilot.AddCar(car);
            cars.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
            }

        public string AddPilotToRace(string raceName, string pilotFullName)
            {
            IRace race = races.FindByName(raceName);
            IPilot pilot = pilots.FindByName(pilotFullName);

            if (race == null)
                {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
                }
            if (pilot == null || !pilot.CanRace || race.Pilots.Any(x => x.FullName == pilotFullName))
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
                }
            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
            }

        public string StartRace(string raceName)
            {
            IRace race = races.FindByName(raceName);
            if (race == null)
                {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
                }
            if (race.Pilots.Count < 3)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
                }
            if (race.tookPlace)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
                }

            List<IPilot> racers = (List<IPilot>)race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {racers[0].FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {racers[1].FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {racers[2].FullName} is third in the {raceName} race.");
            race.tookPlace = true;
            racers[0].WinRace();
            return sb.ToString().Trim();
            }

        public string PilotReport()
            {
            StringBuilder sb = new StringBuilder();
            foreach (IPilot pilot in pilots.Models.OrderByDescending(x => x.NumberOfWins))
                {
                sb.AppendLine(pilot.ToString());
                }
            return sb.ToString().Trim();
            }

        public string RaceReport()
            {
            StringBuilder sb = new StringBuilder();
            foreach (IRace race in races.Models.Where(x => x.tookPlace))
                {
                sb.AppendLine(race.RaceInfo());
                }
            return sb.ToString().Trim();
            }

        }
    }