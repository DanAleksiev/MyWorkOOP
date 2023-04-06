using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
    {
    public class Controller : IController
        {
        private EquipmentRepository equipments;
        private List<IGym> gyms;

        public Controller()
            {
            this.equipments = new EquipmentRepository();
            this.gyms = new List<IGym>();
            }

        public string AddGym(string gymType, string gymName)
            {
            IGym gym;
            if (gymType == nameof(BoxingGym))
                {
                gym = new BoxingGym(gymName);
                }
            else if (gymType == nameof(WeightliftingGym))
                {
                gym = new WeightliftingGym(gymName);
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
                }
            this.gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
            }

        public string AddEquipment(string equipmentType)
            {
            IEquipment equipment = null;
            if (equipmentType == nameof(BoxingGloves))
                {
                equipment = new BoxingGloves();
                }
            else if (equipmentType == nameof(Kettlebell))
                {
                equipment = new Kettlebell();
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
                }

            equipments.Add(equipment);

            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
            }

        public string InsertEquipment(string gymName, string equipmentType)
            {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            IEquipment equipment = equipments.FindByType(equipmentType);

            if (equipment == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
                }
            gym.AddEquipment(equipment);
            equipments.Remove(equipment);
            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
            }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
            {
            IAthlete athlete = null;
            if (athleteType == nameof(Boxer))
                {
                athlete = new Boxer(athleteName, motivation, numberOfMedals);
                }
            else if (athleteType == nameof(Weightlifter))
                {
                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
                }

            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            if (athlete.GetType().Name == nameof(Boxer) && gym.GetType().Name == nameof(BoxingGym))
                {
                gym.AddAthlete(athlete);
                }
            else if (athlete.GetType().Name == nameof(Weightlifter) && gym.GetType().Name == nameof(WeightliftingGym))
                {
                gym.AddAthlete(athlete);
                }
            else
                {
                return string.Format(OutputMessages.InappropriateGym);
                }

            return string.Format(OutputMessages.EntityAddedToGym, athleteType,gymName);
            }

        public string TrainAthletes(string gymName)
            {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            gym.Exercise();
            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
            }

        public string EquipmentWeight(string gymName)
            {
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            string weight = $"{gym.EquipmentWeight:f2}";
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, weight);
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();
            foreach (var gym in gyms)
                {
                sb.AppendLine(gym.GymInfo());
                }

            return sb.ToString().Trim();
            }

        }
    }