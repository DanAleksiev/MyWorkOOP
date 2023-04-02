using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Utilities.Messages;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
    {
    public class Controller : IController
        {
        private BoothRepository booths;
        private string[] allowedSizes = { "Small", "Middle", "Large" };

        public Controller()
            {
            this.booths = new BoothRepository();
            }

        public string AddBooth(int capacity)
            {
            IBooth booth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(booth);
            return string.Format(OutputMessages.NewBoothAdded, booth.BoothId, booth.Capacity);
            }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
            {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (delicacyTypeName != nameof(Gingerbread) &&
                delicacyTypeName != nameof(Stolen))
                {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
                }
            if (booth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == delicacyName) != null)
                {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
                }

            IDelicacy delicacy = null;
            if (delicacyTypeName == nameof(Gingerbread))
                {
                delicacy = new Gingerbread(delicacyName);
                }
            else
                {
                delicacy = new Stolen(delicacyName);
                }
            booth.DelicacyMenu.AddModel(delicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
            }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
            {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (cocktailTypeName != nameof(MulledWine) &&
                cocktailTypeName != nameof(Hibernation))
                {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
                }
            if (!allowedSizes.Contains(size))
                {
                return string.Format(OutputMessages.InvalidCocktailSize, cocktailName);
                }
            if (booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == cocktailName && x.Size == size) != null)
                {
                return string.Format(OutputMessages.CocktailAlreadyAdded, cocktailName);
                }

            
            ICocktail cocktal = null;
            if (cocktailTypeName == nameof(MulledWine))
                {
                cocktal = new MulledWine(cocktailName, size);
                }
            else
                {
                cocktal = new Hibernation(cocktailName, size);
                }
            booth.CocktailMenu.AddModel(cocktal);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
            }

        public string ReserveBooth(int countOfPeople)
            {
            IBooth booth = booths.Models
                .OrderBy(x => x.Capacity)
                .ThenByDescending(x=>x.BoothId)
                .FirstOrDefault(x => x.Capacity >= countOfPeople && x.IsReserved == false);

            if (booth == null)
                {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
                }

            booth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
            }
        public string TryOrder(int boothId, string order)
            {
            string itemTypeName = order.Split("/")[0];
            string itemName = order.Split("/")[1];
            int amount = int.Parse(order.Split("/")[2]);

            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            if (itemTypeName != nameof(MulledWine) &&
                itemTypeName != nameof(Hibernation) &&
                itemTypeName != nameof(Stolen) &&
                itemTypeName != nameof(Gingerbread))
                {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
                }

            if (itemTypeName == nameof(MulledWine) ||
                itemTypeName == nameof(Hibernation))
                {
                ICocktail cocktail = null;
                string size = order.Split("/")[3];
                if (itemTypeName == nameof(MulledWine))
                    {
                    cocktail = new MulledWine(itemName,size);
                    }
                else
                    {
                    cocktail = new Hibernation(itemName,size);
                    }
                ICocktail coc = booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == cocktail.Name &&x.Size==cocktail.Size);
                if (booth.CocktailMenu.Models.FirstOrDefault(x => x.Name == cocktail.Name && x.Size == cocktail.Size) == default)
                    {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                    }

                booth.UpdateCurrentBill(cocktail.Price * amount);
                }
            else
                {
                IDelicacy delicacy = null;

                if (itemTypeName == nameof(Gingerbread))
                    {
                    delicacy = new Gingerbread(itemName);
                    }
                else
                    {
                    delicacy = new Stolen(itemName);
                    }

                if (booth.DelicacyMenu.Models.FirstOrDefault(x => x.Name == delicacy.Name) == default)
                    {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                    }
                booth.UpdateCurrentBill(delicacy.Price);
                }

            return string.Format(OutputMessages.SuccessfullyOrdered, booth.BoothId, amount, itemName);
            }

        public string LeaveBooth(int boothId)
            {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");
            booth.Charge();
            booth.ChangeStatus();

            return sb.ToString().Trim();
            }

        public string BoothReport(int boothId)
            {
            IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);

            return booth.ToString().Trim();
            }

        }
    }