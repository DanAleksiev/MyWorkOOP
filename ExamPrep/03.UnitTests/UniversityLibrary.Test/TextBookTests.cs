namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    [TestFixture]
    public class Tests
    {
        private string title = "The Adventures of Po";
        private string author = "Bramborachek";
        private string category = "Scy-Fi";

        [Test]
        public void TextBookConstructor()
            {
            TextBook tb = new TextBook(title,author,category);

            Assert.That(tb.Title, Is.EqualTo(title));
            Assert.That(tb.Author, Is.EqualTo(author));
            Assert.That(tb.Category, Is.EqualTo(category));
            }

        [Test]
        public void TestToString()
            {
            TextBook tb = new TextBook(title, author, category);
            tb.InventoryNumber = 1;

            int inventoryNumber = 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Book: {title} - {inventoryNumber}");
            sb.AppendLine($"Category: {category}");
            sb.AppendLine($"Author: {author}");

            Assert.That(tb.ToString(), Is.EqualTo(sb.ToString().TrimEnd()));
            }

        [Test]
        public void TestInvetoryNumbers()
            {
            TextBook tb = new TextBook(title, author, category);
            int inventoryNumber = 1;
            tb.InventoryNumber = inventoryNumber;

            Assert.That(tb.InventoryNumber, Is.EqualTo(inventoryNumber));
            }

        [Test]
        public void TestHolder()
            {
            string holder = "Frank";
            TextBook tb = new TextBook(title, author, category);
            tb.Holder = holder;

            Assert.That(tb.Holder, Is.EqualTo(holder));
            }
        }
}