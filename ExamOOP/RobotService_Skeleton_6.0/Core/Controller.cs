using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Utilities.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RobotService.Core
    {
    public class Controller : IController
        {
        private RobotRepository robots;
        private SupplementRepository supplements;
        public Controller()
            {
            robots = new RobotRepository();
            supplements = new SupplementRepository();
            }
        public string CreateRobot(string model, string typeName)
            {
            IRobot robot = null;
            if (typeName == nameof(DomesticAssistant))
                {
                robot = new DomesticAssistant(model);
                }
            else if (typeName == nameof(IndustrialAssistant))
                {
                robot = new IndustrialAssistant(model);
                }
            else
                {
                return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
                }

            robots.AddNew(robot);
            return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
            }

        public string CreateSupplement(string typeName)
            {
            ISupplement supplement = null;
            if (typeName == nameof(LaserRadar))
                {
                supplement = new LaserRadar();
                }
            else if (typeName == nameof(SpecializedArm))
                {
                supplement = new SpecializedArm();
                }
            else
                {
                return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
                }
            supplements.AddNew(supplement);
            return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
            }

        public string UpgradeRobot(string model, string supplementTypeName)
            {
            List<ISupplement> supp = supplements.Models().ToList();
            ISupplement supplement = supp.FirstOrDefault(x => x.GetType().Name == supplementTypeName);
            
            int value = supplement.InterfaceStandard;

            IRobot currentRob = null;
            foreach (var rob in robots.Models())
                {
                List<int> standarts = rob.InterfaceStandards.ToList();

                if (!standarts.Contains(value) && rob.Model == model)
                    {
                    currentRob = rob;
                    break;
                    }
                }

            if (currentRob == null)
                {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
                }

            currentRob.InstallSupplement(supplement);
            //robots.RemoveByName(currentRob.GetType().Name);

            supplements.RemoveByName(supplementTypeName);
            //robots.AddNew(currentRob);
            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
            }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
            {
            List<IRobot> robisInstalled = new List<IRobot>();
            foreach (var rob in robots.Models())
                {
                List<int> standarts = rob.InterfaceStandards.ToList();

                if (standarts.Contains(intefaceStandard))
                    {
                    robisInstalled.Add(rob);
                    }
                }

            if (robisInstalled.Count == 0)
                {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
                }

            robisInstalled.OrderByDescending(x => x.BatteryLevel);
            int sumBatterLvl = robisInstalled.Sum(x=>x.BatteryLevel);

            if (sumBatterLvl < totalPowerNeeded)
                {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName , totalPowerNeeded - sumBatterLvl);
                }
            int count = 0;
            foreach (var rob in robisInstalled)
                {
                if (rob.BatteryLevel >= totalPowerNeeded)
                    {
                    rob.ExecuteService(totalPowerNeeded);
                    count++;
                    totalPowerNeeded = 0;
                    break;
                    }
                else
                    {
                    totalPowerNeeded -= rob.BatteryLevel;
                    rob.ExecuteService(rob.BatteryLevel);
                    count++;
                    }
                }

            return string.Format(OutputMessages.PerformedSuccessfully,serviceName,count);
            }

        public string Report()
            {
            StringBuilder sb = new StringBuilder();
            foreach (var robot in robots.Models().OrderByDescending(x => x.BatteryLevel).ThenBy(x => x.BatteryCapacity).ThenBy(x=>x.Model))
                {
                sb.AppendLine(robot.ToString());
                }

            return sb.ToString().Trim();
            }

        public string RobotRecovery(string model, int minutes)
            {
            List<IRobot> robtList = robots.Models().Where(x => x.Model == model).ToList();
            int count = 0;
            foreach (IRobot robt in robtList.Where(x => x.BatteryLevel <= x.BatteryCapacity / 2))
                {
                robt.Eating(minutes);
                count++;
                }

            return string.Format(OutputMessages.RobotsFed, count);
            }

        }
    }