namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database dataB;

        [SetUp]
        public void Setup()
            {
            dataB = new Database();
            }

        [TearDown]
        public void TearDown()
            {
            dataB = null;
            }

        [Test]
        public void AddMethodTest()
            {
            dataB.Add(5);
            int[] result = dataB.Fetch();

            Assert.AreEqual(1, dataB.Count);
            Assert.AreEqual(5, result[0]);
            Assert.AreEqual(1, result.Length);
            }

        [Test]
        public void ShouldThrowIfMoreThanMaximumLength()
            {
            dataB = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => dataB.Add(8));
            Assert.That(exeption.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
            }

        [Test]
        public void CreateDatabaseWith10Elements()
            {
            dataB = new Database(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

            Assert.AreEqual(10, dataB.Count);
            }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
            {
            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => dataB.Remove());
            Assert.That(exeption.Message, Is.EqualTo("The collection is empty!"));
            }

        [Test]
        public void RemoveFromDatabase()
            {
            dataB = new Database(5, 15);
            dataB.Remove();
            var result = dataB.Fetch();

            Assert.AreEqual(1, dataB.Count);
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(5, result[0]);
            }

        [Test]
        public void FetchDataFromDatabase()
            {
            dataB = new Database(1, 2, 3);
            var result = dataB.Fetch();

            Assert.That(new int[] { 1, 2, 3 }, Is.EquivalentTo(result));
            }
        }
}
