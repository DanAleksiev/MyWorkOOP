using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
    {
    public class Submarine : Vessel, ISubmarine
        {
        private const double armorThickness = 200;
        private bool submergeMode;

        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, armorThickness)
            {
            }

        public bool SubmergeMode => submergeMode;

        public void ToggleSubmergeMode()
            {
            if (submergeMode)
                {
                submergeMode = false;
                MainWeaponCaliber -= 40;
                Speed += 4;
                }
            else
                {
                submergeMode = true;
                MainWeaponCaliber += 40;
                Speed -= 4;
                }
            }
        public override void RepairVessel()
            {
            if (ArmorThickness > armorThickness)
                {
                ArmorThickness = armorThickness;
                }
            }
        public override string ToString()
            {
            StringBuilder sb = new StringBuilder();
            sb .AppendLine(base.ToString());
            string sonar = null;
            if (submergeMode)
                {
                sonar = "ON";
                }
            else
                {
                sonar = "OFF";
                }
            sb.AppendLine($" *Submerge mode: {sonar}");

            return sb.ToString().Trim();
            }
        }
    }
