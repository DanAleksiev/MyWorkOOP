using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
    {
    public abstract class Vessel : IVessel
        {
        private string name;
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        private ICaptain captain;
        private List<string> targets;

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
            {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;

            targets = new List<string>();
            }

        public string Name
            {
            get => name;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                    }
                name = value;
                }
            }

        public ICaptain Captain
            {
            get => captain;
            set
                {
                if (value == null)
                    {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainToVessel);
                    }
                captain = value;
                }
            }
        public double ArmorThickness
            {
            get => armorThickness;
             set => armorThickness = value;
            }

        public double MainWeaponCaliber
            {
            get => mainWeaponCaliber;
             set => mainWeaponCaliber = value;
            }

        public double Speed
            {
            get => speed;
             set => speed = value;
            }

        public ICollection<string> Targets => targets;

        public void Attack(IVessel target)
            {
            if (target == null)
                {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
                }
            if (target.ArmorThickness - this.mainWeaponCaliber < 0)
                {
                target.ArmorThickness = 0;
                }
            else
                {
                target.ArmorThickness -= this.mainWeaponCaliber;
                }
            this.targets.Add(target.Name);
            }

        public virtual void RepairVessel()
            {
            }

        public override string ToString()
            {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"- {name}");
            sb.AppendLine($" *Type: {GetType().Name}");
            sb.AppendLine($" *Armor thickness: {armorThickness}");
            sb.AppendLine($" *Main weapon caliber: {mainWeaponCaliber}");
            sb.AppendLine($" *Speed: {speed} knots");
            sb.Append($" *Targets: ");
            if (targets.Count > 0)
                {
                foreach (var target in targets)
                    {
                    sb.Append($"{target}");
                    sb.AppendLine();

                    }
                }
            else { sb.AppendLine("None"); }

            return sb.ToString().Trim();
            }
        }
    }
