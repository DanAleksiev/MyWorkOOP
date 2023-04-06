using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Models.Gyms
    {
    public abstract class Gym : IGym
        {
        private string name;
        private int capacity;
        private readonly List<IEquipment> equipments;
        private readonly List<IAthlete> athletes;

        protected Gym(string name, int capacity)
            {
            Name = name;
            Capacity = capacity;

            equipments = new List<IEquipment>();
            athletes = new List<IAthlete>();
            }

        public string Name
            {
            get => name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArithmeticException(ExceptionMessages.InvalidAthleteName);
                    }
                name = value;
                }
            }

        public int Capacity
            {
            get => capacity;
            private set => capacity = value;
            }


        public ICollection<IEquipment> Equipment => equipments;

        public ICollection<IAthlete> Athletes => athletes;

        public double EquipmentWeight => equipments.Sum(x => x.Weight);
        public void AddAthlete(IAthlete athlete)
            {
            athletes.Add(athlete);
            }

        public void AddEquipment(IEquipment equipment)
            {
            if (athletes.Count == capacity)
                {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
                }
            equipments.Add(equipment);
            }

        public bool RemoveAthlete(IAthlete athlete) => athletes.Remove(athlete);

        public void Exercise()
            {
            foreach (var athlete in athletes)
                {
                athlete.Exercise();
                }
            }

        public string GymInfo()
            {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{name} is a {GetType().Name}:");
            sb.Append($"Athletes: ");
            if (athletes.Count > 0)
                {
                List<string>names = new List<string>();
                foreach (var athlete in athletes)
                    {
                    names.Add(athlete.FullName);
                    }
                sb.Append($"{string.Join(", ", names)}");
                sb.AppendLine();
                }
            else
                {
                sb.Append("No athletes");
                sb.AppendLine();
                }
            sb.AppendLine($"Equipment total count: {equipments.Count}");
            sb.AppendLine($"Equipment total weight: {this.EquipmentWeight:f2} grams");
            return sb.ToString().Trim();
            }

        }
    }
