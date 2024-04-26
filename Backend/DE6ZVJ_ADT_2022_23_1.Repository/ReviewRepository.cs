using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DE6ZVJ_ADT_2022_23_1.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(BookDbContext ctx) : base(ctx)
        {
        }

        public override Review GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(author => author.Id == id);
        }

        public void UpdateReview(int id, string description)
        {
            var review = this.GetOne(id);
            if (review == null)
            {
                throw new Exception("This review doesn't exist in the database");
            }
            else
            {
                review.Description = description;
                this.context.SaveChanges();
            }
        }
    }
}
