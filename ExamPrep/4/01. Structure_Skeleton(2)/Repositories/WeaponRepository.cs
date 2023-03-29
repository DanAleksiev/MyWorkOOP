using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PlanetWars.Repositories
    {
    public class WeaponRepository : IRepository<IWeapon>
        {
        private List<IWeapon> models;

        public WeaponRepository()
            {
            this.models = new List<IWeapon>();
            }

        public IReadOnlyCollection<IWeapon> Models => this.models.AsReadOnly();

        public void AddItem(IWeapon model)
            {
            models.Add(model);
            }

        public IWeapon FindByName(string name)
            {
            IWeapon currentWeapon = models.FirstOrDefault(x => x.GetType().Name == name);
            return currentWeapon;
            }

        public bool RemoveItem(string name)
            {
            IWeapon weapon = models.FirstOrDefault(x => x.GetType().Name == name);
            return models.Remove(weapon);
            }
        }
    }
