using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
    {
    public class RobotRepository : IRepository<IRobot>
        {
        private readonly List<IRobot> models;

        public RobotRepository()
            {
            this.models = new List<IRobot>();
            }

        public void AddNew(IRobot model)
            {
            models.Add(model);
            }

        public IRobot FindByStandard(int interfaceStandard)
            {
            foreach (var model in models)
                {
                List<int> interfaces = model.InterfaceStandards.ToList();

                if (interfaces.Contains(interfaceStandard))
                    {
                    return model;
                    }
                }
            return null;
           
            }


        public IReadOnlyCollection<IRobot> Models() => models;

        public bool RemoveByName(string typeName)
            {
            IRobot robot = models.FirstOrDefault(x => x.GetType().Name == typeName);

            return models.Remove(robot);
            }
        }
    }
