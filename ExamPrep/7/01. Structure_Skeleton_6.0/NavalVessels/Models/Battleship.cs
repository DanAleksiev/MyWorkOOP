using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NavalVessels.Models
    {
    public class Battleship : Vessel, IBattleship
        {
        private const double armorThickness = 300;
        private bool sonar;

        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, armorThickness)
            {
            }

        public bool SonarMode => sonar;

        public void ToggleSonarMode()
            {
            if (sonar)
                {
                sonar = false;
                MainWeaponCaliber -= 40;
                Speed += 5;
                }
            else
                {
                sonar = true;
                MainWeaponCaliber += 40;
                Speed -= 5;
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
            sb.AppendLine(base.ToString());
            string sonar = null;
            if (SonarMode)
                {
                sonar = "ON";
                }
            else
                {
                sonar = "OFF";
                }
            sb.AppendLine($" *Sonar mode: {sonar}");

            return sb.ToString().Trim();
            }

        }
    }
