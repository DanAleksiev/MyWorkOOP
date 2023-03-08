namespace Raiding.Models
    {
    public class Warrior : BaseHero
        {
        private const int Power = 100;

        public Warrior(string name) : base(name, Power)
            {
            }

        public override string CastAbility()
            {
            return $"{GetType().Name} - {this.Name} hit for {Power} damage";
            }
        }
    }
