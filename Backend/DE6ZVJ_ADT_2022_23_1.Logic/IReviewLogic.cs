using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public interface IReviewLogic
    {
        public Review AddNewReview(Review newReview);
        public void DeleteReview(int id);
        Review GetReview(int id);
        IEnumerable<Review> GetAllReview();
        void UpdateReviewContent(Review value);

    }
}
