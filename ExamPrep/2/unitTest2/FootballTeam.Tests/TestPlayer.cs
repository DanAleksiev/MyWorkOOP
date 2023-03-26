using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

namespace FootballTeam.Tests
    {
    public class TestPlayer
        {
        [Test]
        public void ConstructorTesting()
            {
            FootballPlayer player = new FootballPlayer("Diego", 7, "Forward");

            Assert.AreEqual("Diego", player.Name);
            Assert.AreEqual(7, player.PlayerNumber);
            Assert.AreEqual("Forward", player.Position);
            }

        [Test]
        public void NameShouldThorwErrorIfEmptyOrNull()
            {
            string errorMessage = "Name cannot be null or empty!";

            ArgumentException ex =
                Assert.Throws<ArgumentException>(()
                => new FootballPlayer(string.Empty, 7, "Forward"));
            Assert.AreEqual(errorMessage, ex.Message);

            ArgumentException exNull =
                Assert.Throws<ArgumentException>(()
                => new FootballPlayer(null, 7, "Forward"));
            Assert.AreEqual(errorMessage, exNull.Message);
            }

        [Test]
        public void PlayerCantBeNegativeOrMoreThen21()
            {
            string errorMessage = "Player number must be in range [1,21]";

            ArgumentException ex =
                Assert.Throws<ArgumentException>(()
                => new FootballPlayer("Diego", -1, "Forward"));
            Assert.AreEqual(errorMessage, ex.Message);

            ArgumentException exNull =
                Assert.Throws<ArgumentException>(()
                => new FootballPlayer("Diego", 23, "Forward"));
            Assert.AreEqual(errorMessage, exNull.Message);
            }

        [Test]
        public void PlayerHasToBeOnlyTheDesignatedPositions()
            {
            string errorMessage = "Invalid Position";

            ArgumentException ex =
                Assert.Throws<ArgumentException>(()
                => new FootballPlayer("Diego", 2, "back"));
            Assert.AreEqual(errorMessage, ex.Message);
            }

        [Test]
        public void CanThePlayerScoreGols()
            {
            FootballPlayer player = new FootballPlayer("Diego", 7, "Forward");

            player.Score();
            player.Score();
            player.Score();

            Assert.AreEqual(3, player.ScoredGoals);
            }
        }
    }