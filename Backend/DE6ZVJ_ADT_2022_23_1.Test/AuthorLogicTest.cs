using DE6ZVJ_ADT_2022_23_1.Logic;
using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_ADT_2022_23_1.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DE6ZVJ_ADT_2022_23_1.Test
{
    [TestFixture]
    public class AuthorLogicTest
    {
        AuthorLogic AL;
        [SetUp]
        public void Init()
        {
            var MockAuthorRepo = new Mock<AuthorRepository>();
            var authors = new List<Author>()
            {
                new Author(){Name="Author1",Id=1},
                new Author(){Name="New Author",Id=2},
                new Author(){Name="Author1",Id=3},
                new Author(){Name="Author1",Id=4}
            }.AsQueryable();

            MockAuthorRepo.Setup((t) => t.GetAll()).Returns(authors);
            for (int i = 0; i < 4; i++)
            {
                MockAuthorRepo.Setup((t) => t.GetOne(i + 1)).Returns(authors.ToList()[i]);
            }
            AL = new AuthorLogic(MockAuthorRepo.Object);
        }
        [Test]
        public void AddAuthor_ThrowsArgumentException()
        {
            Author aut = new Author() { Name = null };
            //Arrange
            Assert.Throws<ArgumentException>(() => AL.AddNewAuthor(aut));
        }
        [Test]
        public void DeleteAuthorTest_ThrowsException()
        {
            //Arrange
            Assert.Throws<Exception>(() => AL.DeleteAuthor(100));
        }
        [Test]
        public void AuthorFirstNameTest()
        {
            //Arrange
            Author aut = new Author() { Name = "Some name" };
           
            string firstName = AL.FirstName(aut.Name);
            Assert.That(firstName, Is.EqualTo("Some"));
        }
        [Test]
        public void SecondNameTest()
        {
            //Arrange
            Author aut = new Author() { Name = "Some name" };

            string firstName = AL.SecondName(aut.Name);
            Assert.That(firstName, Is.EqualTo("name"));
        }
        [Test]
        public void AllCapsTest()
        {
            //Arrange
            Author aut = new Author() { Name = "Some name" };

            string firstName = AL.AllCaps(aut.Name);
            Assert.That(firstName, Is.EqualTo("SOME NAME"));
        }
    }
   



}
