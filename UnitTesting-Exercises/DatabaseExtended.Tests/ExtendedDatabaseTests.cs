namespace DatabaseExtended.Tests
    {
    using ExtendedDatabase;
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
        public void TestIfTheAddMethodWorks()
            {
            dataB.Add(new Person(1, "Grigor"));
            Person person = dataB.FindById(1);
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Grigor", person.UserName);
            Assert.AreEqual(1, dataB.Count);
            }

        [Test]
        public void TestTheRemovalOfPeople()
            {
            dataB.Add(new Person(1, "Grigor"));
            dataB.Add(new Person(2, "Pepo"));

            dataB.Remove();

            Person person = dataB.FindByUsername("Grigor");
            Assert.AreEqual(1, person.Id);
            Assert.AreEqual("Grigor", person.UserName);
            Assert.AreEqual(1, dataB.Count);
            }

        [Test]
        public void RemoveFromEmptyDatabaseShouldThrow()
            {
            Assert.Throws<InvalidOperationException>(() => dataB.Remove());
            }

        [Test]
        public void TestIfTheArrayHasTheRightNumberOfPeople()
            {
            Person[] people = CreateFullArray();
            dataB = new Database(people);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => dataB.Add(new Person(17, "Jorko")));
            Assert.That(exeption.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
            }
        [Test]

        public void TestIfTheUsernameIsNullOrEmpty()
            {
            ArgumentNullException exeption = Assert
                .Throws<ArgumentNullException>(() => dataB.FindByUsername(null));
            Assert.That(exeption.ParamName, Is.EqualTo("Username parameter is null!"));

            ArgumentNullException emptyExeption = Assert
                .Throws<ArgumentNullException>(() => dataB.FindByUsername(string.Empty));
            Assert.That(exeption.ParamName, Is.EqualTo("Username parameter is null!"));
            }

        [Test]
        public void TestNotExistingUserName()
            {
            dataB.Add(new Person(1, "Grigor"));

            InvalidOperationException exeption = Assert
                            .Throws<InvalidOperationException>(() => dataB.FindByUsername("Po"));
            Assert.That(exeption.Message, Is.EqualTo("No user is present by this username!"));
            }

        [Test]
        public void IsThereADuplicateUsername()
            {
            dataB.Add(new Person(1, "Grigor"));
            Person person1 = dataB.FindById(1);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => dataB.Add(new Person(2, "Grigor")));
            Assert.That(exeption.Message, Is.EqualTo("There is already user with this username!"));
            }

        [Test]
        public void IsThereADuplicateID()
            {
            dataB.Add(new Person(1, "Grigor"));
            Person person1 = dataB.FindById(1);

            InvalidOperationException exeption = Assert
                .Throws<InvalidOperationException>(() => dataB.Add(new Person(1, "Jorko")));
            Assert.That(exeption.Message, Is.EqualTo("There is already user with this Id!"));
            }

        [Test]
        public void IDShouldBePositiveNumber()
            {
            ArgumentOutOfRangeException exeption = Assert
                .Throws<ArgumentOutOfRangeException>(() => dataB.FindById(-1));
            Assert.That(exeption.ParamName, Is.EqualTo("Id should be a positive number!"));
            }
        [Test]
        public void TestNotExistingID()
            {
            dataB.Add(new Person(1, "Grigor"));

            InvalidOperationException exeption = Assert
                            .Throws<InvalidOperationException>(() => dataB.FindById(2));
            Assert.That(exeption.Message, Is.EqualTo("No user is present by this ID!"));
            }

        private Person[] CreateFullArray()
            {
            Person[] people = new Person[16];
            for (int i = 0; i < people.Length; i++)
                {
                people[i] = new Person(i, i.ToString());
                }
            return people;
            }
        }
    }
