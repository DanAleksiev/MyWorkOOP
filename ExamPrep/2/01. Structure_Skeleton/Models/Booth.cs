using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ChristmasPastryShop.Models
    {
    public class Booth : IBooth
        {
        private int id;
        private int capacity;
        private DelicacyRepository delicacyMenu;
        private CocktailRepository cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved = false;

        public Booth(int boothId, int capacity)
            {
            BoothId = boothId;
            Capacity = capacity;

            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
            }

        public int BoothId
            {
            get => id;
            private set
                {
                id = value;
                }
            }

        public int Capacity
            {
            get => capacity;
            private set
                {
                if (value < 0)
                    {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                    }
                capacity = value;
                }
            }

        public IRepository<IDelicacy> DelicacyMenu => (IRepository<IDelicacy>)delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => (IRepository<ICocktail>)cocktailMenu;

        public double CurrentBill
            {
            get => currentBill;
            private set => currentBill = value;
            }

        public double Turnover
            {
            get => turnover;
            private set => turnover = value;
            }

        public bool IsReserved
            {
            get => isReserved;
            private set => isReserved = value;
            }
        public void UpdateCurrentBill(double amount)
            {
            CurrentBill += amount;
            }

        public void ChangeStatus()
            {
            if (isReserved == false)
                {
                isReserved = true;
                }
            else
                {
                isReserved = false;
                }
            }

        public void Charge()
            {
            turnover += currentBill;
            currentBill = 0;
            }

        public override string ToString()
            {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {id}");
            sb.AppendLine($"Capacity: {capacity}");
            sb.AppendLine($"Turnover: {turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach (var item in cocktailMenu.Models)
                {
                sb.AppendLine($"--{item.ToString()}");
                }
            sb.AppendLine($"-Delicacy menu:");
            foreach (var item in delicacyMenu.Models)
                {
                sb.AppendLine($"--{item.ToString()}");
                }

            return sb.ToString().Trim();
            }
        }
    }
