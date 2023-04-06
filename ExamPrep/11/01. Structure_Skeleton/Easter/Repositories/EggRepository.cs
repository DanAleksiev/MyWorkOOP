using Easter.Models.Eggs.Contracts;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter.Repositories
    {
    public class EggRepository : IRepository<IEgg>
        {
        private readonly List<IEgg> models;

        public EggRepository()
            {
            this.models = new List<IEgg>();
            }

        public IReadOnlyCollection<IEgg> Models => models;

        public void Add(IEgg model)
            {
            models.Add(model);
            }

        public IEgg FindByName(string name) => models.FirstOrDefault(x => x.Name == name);

        public bool Remove(IEgg model) => models.Remove(model);
        }
    }
