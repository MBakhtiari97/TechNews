using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private TechNewsContext _context;

        public ReviewRepository(TechNewsContext context)
        {
            _context = context;
        }

        public bool AddReview(ReviewViewModel review)
        {
            if (!_context.BlackList.Any(u=>u.BlackListIpAddress==review.UserIpAddress))
            {
                Review newReview = new Review()
                {
                    ReviewDate = DateTime.Now,
                    DislikeCount = 0,
                    IsPublished = false,
                    ItemId = review.ItemId,
                    EmailAddress = review.EmailAddress,
                    LikeCount = 0,
                    ReviewDescription = review.Description,
                    ReviewTitle = review.Title,
                    UserIpAddress = review.UserIpAddress,
                    Username = review.Username,
                    ParentId = review.ParentId
                };
                _context.Add(newReview);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void DislikeComment(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            review.DislikeCount += 1;
            _context.SaveChanges();
        }

        public IEnumerable<Review> GetMostLikedReviews()
        {
            return _context.Reviews
                .OrderByDescending(r => r.LikeCount)
                .Include(r => r.Items)
                .Take(5)
                .ToList();
        }

        public IEnumerable<ReviewViewModel> GetReviewsByPostId(int postId)
        {
            var review = _context.Reviews
                .Where(r => r.ItemId == postId && r.IsPublished)
                .ToList();

            List<ReviewViewModel> listReview = new List<ReviewViewModel>();
            foreach (var item in review)
            {
                listReview.Add(new ReviewViewModel()
                {
                    EmailAddress = item.EmailAddress,
                    Description = item.ReviewDescription,
                    Title = item.ReviewTitle,
                    Username = item.Username,
                    LikeCount = item.LikeCount,
                    DislikeCount = item.DislikeCount,
                    ReviewDate = item.ReviewDate,
                    ItemId = item.ItemId,
                    ReviewId = item.ReviewId,
                    ParentId = item.ParentId,
                    UserIpAddress = item.UserIpAddress
                });
            }

            return listReview;

        }

        public void LikeComment(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            review.LikeCount += 1;
            _context.SaveChanges();
        }

        public void ReportReview(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            review.IsPublished = false;
            _context.SaveChanges();
        }

    }
}
