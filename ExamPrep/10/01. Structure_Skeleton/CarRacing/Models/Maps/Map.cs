using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
        {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
            {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
                {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
                }
            else if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
                {
                return string.Format(OutputMessages.OneRacerIsNotAvailable,racerTwo.Username,racerOne.Username);
                }
            else if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
                {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
                }

            double behaviorMultP1 = 0;
            double behaviorMultP2 = 0;
            if (racerOne.RacingBehavior == "strict")
                {
                behaviorMultP1 = 1.2;
                }
            else
                {
                behaviorMultP1 = 1.1;
                }   
            if (racerTwo.RacingBehavior == "strict")
                {
                behaviorMultP2 = 1.2;
                }
            else
                {
                behaviorMultP2 = 1.1;
                }
            double racerOneChanceOfWinning = racerOne.Car.HorsePower * racerOne.DrivingExperience * behaviorMultP1;
            double racerTwoChanceOfWinning = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * behaviorMultP2;
            racerOne.Race();
            racerTwo.Race();
            if (racerOneChanceOfWinning > racerTwoChanceOfWinning)
                {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username ,racerOne.Username);
                }
            else
                {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
                }
            }
        }
    }
