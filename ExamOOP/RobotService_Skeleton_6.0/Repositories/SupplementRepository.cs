using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
    {
    public class SupplementRepository : IRepository<ISupplement>
        {
        private readonly List<ISupplement> models;

        public SupplementRepository()
            {
            this.models = new List<ISupplement>();
            }

        public void AddNew(ISupplement model)
            {
            models.Add(model);
            }

        public ISupplement FindByStandard(int interfaceStandard) => models.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);

        public IReadOnlyCollection<ISupplement> Models() => models;

        public bool RemoveByName(string typeName)
            {
            ISupplement supplement = models.FirstOrDefault(x => x.GetType().Name == typeName);

            return models.Remove(supplement);
            }
        }
    }
