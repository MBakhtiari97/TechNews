using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services.Interfaces
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetMostLikedReviews();
        IEnumerable<ReviewViewModel> GetReviewsByPostId(int postId);
        bool AddReview(ReviewViewModel review);
        void ReportReview(int reviewId);
        void LikeComment(int reviewId);
        void DislikeComment(int reviewId);
    }
}
