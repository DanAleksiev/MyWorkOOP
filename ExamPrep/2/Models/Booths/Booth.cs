using _01.Structure2._0.Models.Cocktails;
using _01.Structure2._0.Models.Delicacies;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _01.Structure2._0.Models.Booths
    {
    public class Booth : IBooth
        {
        private int id;
        private int capacity;
        private IRepository<IDelicacy> delicacys;
        private IRepository<ICocktail> coctails;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
            {
            BoothId = boothId;
            Capacity = capacity;

            delicacys = new DelicacyRepository(); 
            coctails = new CocktailRepository();

            currentBill = 0;
            turnover = 0;
            }

        public int BoothId
            {
            get => id;
            private set => id = value;
            }
        public int Capacity
            {
            get => capacity;
            private set
                {
                if (value <= 0)
                    {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                    }
                capacity = value;
                }

            }

        public IRepository<IDelicacy> DelicacyMenu => this.delicacys;

        public IRepository<ICocktail> CocktailMenu => this.coctails;

        public double CurrentBill => currentBill;

        public double Turnover => turnover;
        public bool IsReserved
            {
            get;
            private set;
            }

    public void ChangeStatus()
            {
            if (!IsReserved)
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
            this.turnover += CurrentBill;
            this.currentBill = 0;//test
            }

        public void UpdateCurrentBill(double amount)
            {
            currentBill += amount;
            }

        public override string ToString()
            { // correction may be needed
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {id}");
            sb.AppendLine($"Capacity: {capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine("-Cocktail menu:");
            foreach (var coctail in this.CocktailMenu.Models)
                {
                sb.AppendLine($"--{coctail}");
                }
            sb.AppendLine("-Delicacy menu:");
            foreach (var delicacy in this.DelicacyMenu.Models)
                {
                sb.AppendLine($"--{delicacy}");
                }
            return sb.ToString().Trim();
            }
        }
    }
