using _01.Structure2._0.Models.Booths;
using _01.Structure2._0.Models.Cocktails;
using _01.Structure2._0.Models.Delicacies;
using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
    {
    public class Controller : IController
        {
        private readonly IRepository<IBooth> booths;


        public Controller()
            {
            this.booths = new BoothRepository();
            }

        public string AddBooth(int capacity)
            {
            Booth newBooth = new Booth(booths.Models.Count + 1, capacity);
            booths.AddModel(newBooth);
            string result = string.Format(OutputMessages.NewBoothAdded, newBooth.BoothId, newBooth.Capacity);
            return result.Trim();
            }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
            {
            string result = string.Empty;
            string[] posibleSizeValues = { "Small", "Middle", "Large" };

            if (cocktailTypeName != nameof(MulledWine) &&
                cocktailTypeName != nameof(Hibernation))
                {
                result = string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
                }
            else if (!posibleSizeValues.Contains(size))
                {
                result = string.Format(OutputMessages.InvalidCocktailSize, size);
                }
            else if (booths.Models.Any(x => x.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size)))
                {
                result = string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
                }
            else
                {
                ICocktail newCocktail;
                if (cocktailTypeName == nameof(MulledWine))
                    {
                    newCocktail = new MulledWine(cocktailName, size);
                    }
                else
                    {
                    newCocktail = new Hibernation(cocktailName, size);
                    }
                IBooth booth = this.booths.Models.FirstOrDefault(x => x.BoothId == boothId);
                booth.CocktailMenu.AddModel(newCocktail);
                result = string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
                }

            return result.Trim();
            }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
            {
            string result = string.Empty;
            if (delicacyTypeName != nameof(Stolen) &&
                delicacyTypeName != nameof(Gingerbread))
                {
                result = string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
                }
            else if (booths.Models.Any(x => x.DelicacyMenu.Models.Any(d => d.Name == delicacyName)))
                {
                result = string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyTypeName);
                }
            else
                {
                IDelicacy currentDelicacy;
                if (delicacyTypeName == nameof(Stolen))
                    {
                    currentDelicacy = new Stolen(delicacyName);
                    }
                else
                    {
                    currentDelicacy = new Gingerbread(delicacyName);
                    }

                IBooth booth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
                booth.DelicacyMenu.AddModel(currentDelicacy);

                result = string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);
                }

            return result.Trim();
            }

        public string BoothReport(int boothId)
            => booths.Models.FirstOrDefault(b => b.BoothId == boothId).ToString().Trim();

        public string LeaveBooth(int boothId)
            {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {booth.CurrentBill:f2} lv");
            booth.Charge();
            booth.ChangeStatus();
            sb.AppendLine($"Booth {boothId} is now available!");
            return sb.ToString().Trim();
            }

        public string ReserveBooth(int countOfPeople)
            {
            string result = string.Empty;
            IBooth booth = booths.Models
                .Where(x => x.IsReserved == false && x.Capacity >= countOfPeople)
                .OrderBy(x => x.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();
            if (booth == null)
                {
                result = string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
                }
            else
                {
                booth.Charge();
                result = string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
                }

            return result.Trim();
            }

        public string TryOrder(int boothId, string order)
            {
            string result = string.Empty;

            string[] orderArr = order.Split('/');
            bool isCocktail = false;
            IBooth currentBooth = booths.Models.FirstOrDefault(x => x.BoothId == boothId);
            string type = orderArr[0];
            if (type != nameof(MulledWine) &&
                type != nameof(Hibernation) &&
                type != nameof(Gingerbread) &&
                type != nameof(Stolen))
                {
                return result = string.Format(OutputMessages.NotRecognizedType, type);
                }

            string itemName = orderArr[1];
            if (!currentBooth.CocktailMenu.Models.Any(x => x.Name == itemName) &&
                !currentBooth.DelicacyMenu.Models.Any(x => x.Name == itemName))
                {
                return string.Format(OutputMessages.NotRecognizedItemName, type, itemName);
                }

            int amount = int.Parse(orderArr[2]);
            if (type == nameof(MulledWine)||type == nameof(Hibernation))
                {
                isCocktail = true;
                }

            if (isCocktail)
                {
                string size = orderArr[3];

                ICocktail desiredCoctail = 
                    currentBooth.CocktailMenu.Models
                    .FirstOrDefault(c=>c.GetType().Name == type && c.Name == itemName && c.Size == size); 

                if (desiredCoctail == null)
                    {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                    }
                currentBooth.UpdateCurrentBill(desiredCoctail.Price * amount);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, amount, itemName);
                }
            else
                {
                IDelicacy desiredDelicacy = currentBooth
                    .DelicacyMenu.Models
                    .FirstOrDefault(f => f.GetType().Name == type && f.Name == itemName);
                
                if (desiredDelicacy == null)
                    {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemName);
                    }

                currentBooth.UpdateCurrentBill(desiredDelicacy.Price * amount);
                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, amount, itemName);
                }
            }
        }
    }