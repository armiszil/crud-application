using DE6ZVJ_ADT_2022_23_1.Logic;
using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_ADT_2022_23_1.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Test
{
    [TestFixture]
    public class BookLogicTest
    {
        BookLogic BL;
        [SetUp]
        public void Init()
        {
            var MockClassesRepository = new Mock<IBookRepository>();
            var books = new List<Book>()
            {
               new Book(){Id=1,Title="Title1",Pages=1,Genre="Something"},
                new Book(){Id=2,Title="Title2",Pages=1,Genre="Something"},
                 new Book(){Id=3,Title="Title3",Pages=1,Genre="Something"}
                 , new Book(){Id=4,Title="Title4",Pages=1,Genre="Something"}
            }.AsQueryable();
            MockClassesRepository.Setup((t) => t.GetAll()).Returns(books);
            for (int i = 0; i < 5; i++)
            {
                MockClassesRepository.Setup((t) => t.GetOne(i + 1)).Returns(books.ToList()[i]);
            }
            BL = new BookLogic(MockClassesRepository.Object);
        }

        [Test]
        public void AddBook_Throws_Exception()
        {
            Book book = new Book() { Title=null,Pages=4123,Genre="Horror"};
            //Arrange
            Assert.Throws<ArgumentException>(() => BL.AddNewBook(book));
        }
        [Test]
        public void DeleteBookTest_ThrowsException()
        {
            //Arrange
            Assert.Throws<Exception>(() => BL.DeleteBook(100));
        }
        [Test]
        public void AddNewBookTest()
        {
            Book book = new Book() { Title="ThisandThat",Pages=231,Genre="ThisAndThat" };
            //Act

            Book book12 = BL.AddNewBook(book);
            //Arrange
            Assert.That(book12.Title, Is.EqualTo("ThisandThat"));
        }
        [Test]
       public void LongestBookMethodTest()
        {
            var books = new List<Book>()
            {
               new Book(){Id=1,Title="Title1",Pages=1,Genre="Something"},
                new Book(){Id=2,Title="Title2",Pages=2,Genre="Something"},
                 new Book(){Id=3,Title="Title3",Pages=3,Genre="Something"}
                 , new Book(){Id=4,Title="Title4",Pages=4,Genre="Something"}
            }.AsQueryable();
            var longestmf = BL.LongestBookQuery(books);

            Assert.That(longestmf, Is.EqualTo(4));
            
        }

        [Test]
        public void ShortestBookMethodTest()
        {
            var books = new List<Book>()
            {
               new Book(){Id=1,Title="Title1",Pages=1,Genre="Something"},
                new Book(){Id=2,Title="Title2",Pages=2,Genre="Something"},
                 new Book(){Id=3,Title="Title3",Pages=3,Genre="Something"}
                 , new Book(){Id=4,Title="Title4",Pages=4,Genre="Something"}
            }.AsQueryable();
            int shortestmf = BL.LongestBookQuery(books);

            Assert.That(shortestmf, Is.EqualTo(1));

        }

    }
}
