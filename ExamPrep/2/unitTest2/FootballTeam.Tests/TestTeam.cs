using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FootballTeam.Tests
    {
    [TestFixture]
    public class TestTeam
        {
        [Test]
        public void TeamConstructor()
            {
            FootballTeam team = new FootballTeam("Minior Pernik", 24);

            Assert.AreEqual("Minior Pernik", team.Name);
            Assert.AreEqual(24, team.Capacity);
            }

        [Test]
        public void NameShouldNotBeNullOrEmpty()
            {
            string errorMessage = "Name cannot be null or empty!";

            ArgumentException ex =
                Assert.Throws<ArgumentException>(()
                => new FootballTeam(string.Empty, 24));
            Assert.AreEqual(errorMessage, ex.Message);

            ArgumentException exNull =
                Assert.Throws<ArgumentException>(()
                => new FootballTeam(null, 23));
            Assert.AreEqual(errorMessage, exNull.Message);
            }

        [Test]
        public void CapacitySholdBeMoreThen14()
            {
            string errorMessage = "Capacity min value = 15";

            ArgumentException ex =
                Assert.Throws<ArgumentException>(()
                => new FootballTeam("Minior Pernik", 13));
            Assert.AreEqual(errorMessage, ex.Message);
            }

        [Test]
        public void PlayersSHouldntBemoreThenCapacity()
            {
            string errorMessage = "No more positions available!";
            FootballTeam team = new FootballTeam("Minior Pernik", 15);

            AddTeamMembers(team);
            string result = team.AddNewPlayer(new FootballPlayer("Bojinov", 12, "Forward"));
            Assert.AreEqual(errorMessage, result);
            }
        [Test]
        public void PlayerBeAdded()
            {
            FootballPlayer player = new FootballPlayer("Bojinov", 12, "Forward");
            string errorMessage = $"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}";
            FootballTeam team = new FootballTeam("Minior Pernik", 16);

            AddTeamMembers(team);
            team.Capacity += 1;

            string result = team.AddNewPlayer(player);
            Assert.AreEqual(errorMessage, result);
            }

        [Test]
        public void ChckThePlayerList()
            {
            string errorMessage;
            FootballTeam team = new FootballTeam("Minior Pernik", 16);
            AddTeamMembers(team);
            List<FootballPlayer> players = team.Players;

            Assert.AreEqual(players, team.Players);
            }

        [Test]
        public void PlayerScoreGole()
            {
            FootballPlayer player = new FootballPlayer("Bojinov", 12, "Forward");
            string errorMessage = $"{player.PlayerNumber} scored and now has {player.ScoredGoals + 1} for this season!";
            FootballTeam team = new FootballTeam("Minior Pernik", 16);

            AddTeamMembers(team);
            team.Capacity += 1;
            team.AddNewPlayer(player);

            string result = team.PlayerScore(player.PlayerNumber);
            Assert.AreEqual(errorMessage, result);
            }

        [Test]
        public void PickAPlayer()
            {
            FootballPlayer player = new FootballPlayer("Bojinov", 12, "Forward");
            FootballTeam team = new FootballTeam("Minior Pernik", 16);

            AddTeamMembers(team);
            team.Capacity += 1;
            team.AddNewPlayer(player);

            Assert.AreEqual(player, team.PickPlayer(player.Name));
            }
        private void AddTeamMembers(FootballTeam team)
            {
            for (int i = 1; i <= team.Capacity; i++)
                {
                FootballPlayer player = new FootballPlayer($"{i}", i, "Forward");
                team.AddNewPlayer(player);
                }
            }
        }
    }