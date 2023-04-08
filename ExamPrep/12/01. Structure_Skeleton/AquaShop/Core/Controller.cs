using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
    {
    public class Controller : IController
        {
        private readonly DecorationRepository decorations;
        private readonly List<IAquarium> aquariums;

        public Controller()
            {
            this.decorations = new DecorationRepository();
            this.aquariums = new List<IAquarium>();
            }

        public string AddAquarium(string aquariumType, string aquariumName)
            {
            IAquarium aquarium;
            if (aquariumType == nameof(SaltwaterAquarium))
                {
                aquarium = new SaltwaterAquarium(aquariumName);
                }
            else if (aquariumType == nameof(FreshwaterAquarium))
                {
                aquarium = new FreshwaterAquarium(aquariumName);
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
                }

            aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
            }

        public string AddDecoration(string decorationType)
            {
            IDecoration decor;
            if (decorationType == nameof(Ornament))
                {
                decor = new Ornament();
                }
            else if (decorationType == nameof(Plant))
                {
                decor = new Plant();
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
                }

            decorations.Add(decor);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
            }

        public string InsertDecoration(string aquariumName, string decorationType)
            {
            IDecoration decor = decorations.FindByType(decorationType);
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);

            if (decor == null)
                {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
                }

            aquarium.AddDecoration(decor);
            decorations.Remove(decor);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
            }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
          {
            IFish fish;
            bool isSaltFish = false;
            if (fishType == nameof(FreshwaterFish))
                {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
                }
            else if (fishType == nameof(SaltwaterFish))
                {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
                isSaltFish = true;
                }
            else
                {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
                }
            IAquarium aqua = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            if (isSaltFish == true && aqua.GetType().Name == nameof(SaltwaterAquarium))
                {
                aqua.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
                }
            if (isSaltFish == false && aqua.GetType().Name == nameof(FreshwaterAquarium))
                {
                aqua.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
                }
            return string.Format(OutputMessages.UnsuitableWater);
            }

        public string FeedFish(string aquariumName)
            {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
            }

        public string CalculateValue(string aquariumName)
            {
            IAquarium aquarium = aquariums.FirstOrDefault(x => x.Name == aquariumName);
            decimal totalValue = aquarium.Decorations.Sum(x => x.Price) + aquarium.Fish.Sum(x => x.Price);
            string formatedV = $"{totalValue:f2}";
            return string.Format(OutputMessages.AquariumValue, aquariumName, formatedV);
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();
            foreach (var aquarium in aquariums)
                {
                sb.AppendLine(aquarium.GetInfo());
                }
            return sb.ToString().Trim();
            }
        }
    }