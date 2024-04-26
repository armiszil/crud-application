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
    public class ReviewLogicTest
    {


        ReviewLogic RL;
        [SetUp]
        public void Init()
        {
            var MockReviewRepo = new Mock<ReviewRepository>();
            var reviews = new List<Review>()
            {
               new Review(){Id=1,Description="Something",Name="Someone",Rating="ok"},
               new Review(){Id=2,Description="Something",Name="Someone",Rating="ok"},
               new Review(){Id=3,Description="Something",Name="Someone",Rating="ok"},
               new Review(){Id=4,Description="Something",Name="Someone",Rating="ok"},

            }.AsQueryable();

            MockReviewRepo.Setup((t) => t.GetAll()).Returns(reviews);
            for (int i = 0; i < 4; i++)
            {
                MockReviewRepo.Setup((t) => t.GetOne(i + 1)).Returns(reviews.ToList()[i]);
            }
            RL = new ReviewLogic(MockReviewRepo.Object);
        }
        [Test]
        public void AddReview_ThrowsArgumentException()
        {
            Review rev = new Review() { Name = null };
            //Arrange
            Assert.Throws<ArgumentException>(() => RL.AddNewReview(rev));
        }
        [Test]
        public void DeleteReviewTest_ThrowsException()
        {
            //Arrange
            Assert.Throws<Exception>(() => RL.DeleteReview(100));
        }
        [Test]
        public void AddNewReviewTest()
        {
            Review review = new Review() { Name = "ThisandThat", Description="sadasd", Rating = "ThisAndThat" };
            //Act

            Review REV123 = RL.AddNewReview(review);
            //Arrange
            Assert.That(REV123.Description, Is.EqualTo("sadasd"));
        }
    }
}
