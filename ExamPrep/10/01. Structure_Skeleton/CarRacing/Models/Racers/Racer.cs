using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
    {
    public abstract class Racer : IRacer
        {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        protected Racer(string username, string racingBehavior, int drivingExperience, ICar car)
            {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
            }

        public string Username
            {
            get => username;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                    }
                username = value;
                }
            }

        public string RacingBehavior
            {
            get => racingBehavior;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                    }
                racingBehavior = value;
                }
            }

        public int DrivingExperience
            {
            get => drivingExperience;
            private set
                {
                if (value < 0 || value > 100)
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                    }
                drivingExperience = value;
                }
            }

        public ICar Car
            {
            get => car;
            private set
                {
                if (value == null)
                    {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                    }
                car = value;
                }
            }

        public void Race()
            {
            car.Drive();
            if (GetType().Name == nameof(ProfessionalRacer))
                {
                drivingExperience += 10;
                }

            if (GetType().Name == nameof(StreetRacer))
                {
                drivingExperience += 5;
                }
            }

        public bool IsAvailable()
            {
            if (car.FuelAvailable >= car.FuelConsumptionPerRace)
                {
                return true;
                }
            else { return false; }
            }

        public override string ToString()
            {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{GetType().Name}: {username}");
            sb.AppendLine($"--Driving behavior: {racingBehavior}");
            sb.AppendLine($"--Driving experience: {drivingExperience}");
            sb.AppendLine($"--Car: {car.Make} {car.Model} ({car.VIN})");

            return sb.ToString().Trim();
            }
        }
    }
