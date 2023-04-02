using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
    {
    public class UniversityRepository : IRepository<IUniversity>
        {
        private readonly List<IUniversity> universitys;

        public UniversityRepository()
            {
            this.universitys = new List<IUniversity>();
            }

        public IReadOnlyCollection<IUniversity> Models => this.universitys;

        public void AddModel(IUniversity model)
            {
            universitys.Add(model);
            }

        public IUniversity FindById(int id) => universitys.FirstOrDefault(x => x.Id == id);

        public IUniversity FindByName(string name) => universitys.FirstOrDefault(x => x.Name == name);
        }
    }
