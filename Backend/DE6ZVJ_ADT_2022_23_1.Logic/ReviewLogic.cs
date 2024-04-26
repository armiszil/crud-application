using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_ADT_2022_23_1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public class ReviewLogic : IReviewLogic
    {
        protected IReviewRepository revrepo;
        //private ReviewRepository @object;

        //public ReviewLogic(ReviewRepository @object)
        //{
        //    this.@object = @object;
        //}
        public ReviewLogic(IReviewRepository revrepo)
        {
            this.revrepo = revrepo;
        }

        public Review AddNewReview(Review rev)
        {
            if (rev.Description == null)
            {
                throw new ArgumentException("Please enter the review");
            }
            else
            {
                this.revrepo.Add(rev);
                return rev;
            }
        }

        public void DeleteReview(int id)
        {
            Review DeleteRev = this.revrepo.GetOne(id);
            if (DeleteRev != null)
            {
                this.revrepo.Delete(DeleteRev);
            }
            else
            {
                throw new Exception("The ID is not in the Reviews database.");
            }
        }

        public IEnumerable<Review> GetAllReview()
        {
            return this.revrepo.GetAll();
        }

        public Review GetReview(int id)
        {
            Review RevReturn = this.revrepo.GetOne(id);
            if (RevReturn != null)
            {
                return RevReturn;
            }
            else
            {
                throw new Exception("The ID is not in the reviews database.");
            }
        }

        public void UpdateReviewContent(Review rev)
        {
           
                this.revrepo.UpdateReview(rev.Id, rev.Description);
            
        }
    }
}
 