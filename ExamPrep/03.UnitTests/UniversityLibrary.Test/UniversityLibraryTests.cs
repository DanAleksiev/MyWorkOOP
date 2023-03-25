namespace UniversityLibrary.Test
    {
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class UniversityLibraryTests
        {

        [Test]
        public void CanYouAddBooksToTheLibrary()
            {
            UniversityLibrary universityLibrary = new UniversityLibrary();
            TextBook tb = new TextBook("The Adventures of Po", "Bramborachek", "Scy-Fi");
            universityLibrary.AddTextBookToLibrary(tb);
            }

        [Test]
        public void GetTheCatalogue()
            {

            List<TextBook> books = new List<TextBook>();
            books.Add(new TextBook("DieHard", "Azis", "Action"));
            books.Add(new TextBook("IAmNumber4", "Paticus Lore", "Scy-Fi"));
            UniversityLibrary library = new UniversityLibrary();

            foreach (TextBook book in books)
                {
                library.AddTextBookToLibrary(book);
                }

            Assert.That(library.Catalogue, Is.EqualTo(books));
            }

        [Test]
        public void YouLoanIAmNumberFour()
            {
            List<TextBook> books = new List<TextBook>();
            books.Add(new TextBook("DieHard", "Azis", "Action"));
            books.Add(new TextBook("IAmNumber4", "Paticus Lore", "Scy-Fi"));
            UniversityLibrary library = new UniversityLibrary();
            string studentName = "Boris";
            string bookTitle = "IAmNumber4";
            foreach (TextBook book in books)
                {
                library.AddTextBookToLibrary(book);
                }
            string result = library.LoanTextBook(2, studentName);
            Assert.That(result, Is.EqualTo($"{bookTitle} loaned to {studentName}."));
            }  

        [Test]
        public void YouLoanIAmNumberFourButIsTaken()
            {
            List<TextBook> books = new List<TextBook>();
            books.Add(new TextBook("DieHard", "Azis", "Action"));
            books.Add(new TextBook("IAmNumber4", "Paticus Lore", "Scy-Fi"));
            UniversityLibrary library = new UniversityLibrary();
            foreach (TextBook book in books)
                {
                library.AddTextBookToLibrary(book);
                }

            string studentName = "Boris";
            string bookTitle = "IAmNumber4";
            string result = library.LoanTextBook(2, studentName);

            string unsucsessfullAttempt = library.LoanTextBook(2, studentName);

            Assert.That(unsucsessfullAttempt, Is.EqualTo($"{studentName} still hasn't returned {bookTitle}!"));
            }

        [Test]
        public void ReturnBooks()
            {
            List<TextBook> books = new List<TextBook>();
            books.Add(new TextBook("DieHard", "Azis", "Action"));
            books.Add(new TextBook("IAmNumber4", "Paticus Lore", "Scy-Fi"));
            UniversityLibrary library = new UniversityLibrary();
            foreach (TextBook book in books)
                {
                library.AddTextBookToLibrary(book);
                }

            string studentName = "Boris";
            string bookTitle = "IAmNumber4";
            string loan = library.LoanTextBook(2, studentName);
            string result = library.ReturnTextBook(2);

            Assert.That(result, Is.EqualTo($"{bookTitle} is returned to the library."));
            }
        }
    }
