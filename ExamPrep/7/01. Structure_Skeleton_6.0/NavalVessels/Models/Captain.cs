using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NavalVessels.Models
    {
    public class Captain : ICaptain
        {
        private string fullName;
        private int combatExperience;
        private readonly List<IVessel> vessels;

        public Captain(string fullName)
            {
            FullName = fullName;

            vessels = new List<IVessel>();
            }

        public string FullName
            {
            get => fullName;
            private set
                {
                if (string.IsNullOrWhiteSpace(value))
                    {
                    throw new ArgumentNullException(ExceptionMessages.InvalidCaptainName);
                    }
                fullName = value;
                }
            }

        public int CombatExperience => this.combatExperience;

        public ICollection<IVessel> Vessels => this.vessels;

        public void AddVessel(IVessel vessel)
            {
            if (vessel == null)
                {
                throw new NullReferenceException(ExceptionMessages.InvalidVesselForCaptain);
                }
            this.vessels.Add(vessel);
            }

        public void IncreaseCombatExperience()
            {
            combatExperience += 10;
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {vessels.Count} vessels.");
            if (vessels.Count > 0)
                {
                foreach (IVessel ve in vessels)
                    {
                    sb.AppendLine(ve.ToString());
                    }
                }
            return sb.ToString().Trim();
            }
        }
    }
