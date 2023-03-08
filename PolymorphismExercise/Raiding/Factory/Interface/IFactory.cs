using Raiding.Models;

namespace Raiding.Factory.Interface
{
    public interface IFactory
    {
        BaseHero AddHeroToTheGroup(string heroName, string heroClass);
    }
}
