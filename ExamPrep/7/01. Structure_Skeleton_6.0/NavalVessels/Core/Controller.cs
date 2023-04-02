using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace NavalVessels.Core
    {
    public class Controller : IController
        {
        private VesselRepository vessels;
        private readonly List<ICaptain> captains;
        public Controller()
            {
            vessels = new VesselRepository();
            captains = new List<ICaptain>();
            }

        public string HireCaptain(string fullName)
            {
            if (captains.Any(x => x.FullName == fullName))
                {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
                }

            ICaptain captain = new Captain(fullName);
            captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
            }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
            {
            if (vessels.FindByName(name) != null)
                {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
                }
            if (vesselType != nameof(Battleship) &&
                vesselType != nameof(Submarine))
                {
                return string.Format(OutputMessages.InvalidVesselType);
                }
            IVessel vessel;
            if (vesselType == nameof(Battleship))
                {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
                }
            else
                {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
                }
            vessels.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
            }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
            {
            IVessel vessel = vessels.FindByName(selectedVesselName);
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == selectedCaptainName);

            if (captain == null)
                {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
                }
            if (vessel == null)
                {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
                }
            if (vessel.Captain != null)
                {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
                }
            vessel.Captain = captain;
            captain.AddVessel(vessel);

            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
            }

        public string CaptainReport(string captainFullName)
            {
            ICaptain captain = captains.FirstOrDefault(x => x.FullName == captainFullName);
            return captain.Report();
            }

        public string VesselReport(string vesselName)
            {
            IVessel vessel = vessels.FindByName(vesselName);
            return vessel.ToString();
            }

        public string ToggleSpecialMode(string vesselName)
            {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
                {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
                }

            if (vessel.GetType().Name == nameof(Battleship))
                {
                Battleship bs = (Battleship)vessel;
                bs.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
                }
            else
                {
                Submarine bs = (Submarine)vessel;
                bs.ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
                }
            }

        public string ServiceVessel(string vesselName)
            {
            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
                {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
                }
            vessel.RepairVessel();
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
            }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
            {
            IVessel attacker = vessels.FindByName(attackingVesselName);
            IVessel defender = vessels.FindByName(defendingVesselName);

            if (attacker == null || defender == null)
                {
                string name;
                if (defender == null)
                    {
                    name = defendingVesselName;
                    }
                else
                    {
                    name = attackingVesselName;
                    }
                return string.Format(OutputMessages.VesselNotFound, name);
                }
            if (attacker.ArmorThickness == 0 || defender.ArmorThickness == 0)
                {
                string name;
                if (defender.ArmorThickness == 0)
                    {
                    name = defendingVesselName;
                    }
                else
                    {
                    name = attackingVesselName;
                    }
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, name);
                }

            attacker.Attack(defender);
            attacker.Captain.IncreaseCombatExperience();
            defender.Captain.IncreaseCombatExperience();
            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defender.ArmorThickness);
            }

        }
    }