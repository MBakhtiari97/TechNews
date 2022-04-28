using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services
{
    public class PostRepository : IPostRepository
    {
        private TechNewsContext _context;

        public PostRepository(TechNewsContext context)
        {
            _context = context;
        }

        public Item GetItemByItemId(int itemId)
        {
            return _context.Items
                .Include(i => i.Reviews)
                .Include(i => i.SelectedCategory)
                .ThenInclude(i => i.Categories)
                .Include(i=>i.Tags)
                .SingleOrDefault(i => i.ItemId == itemId);
        }

        public IEnumerable<Item> GetLatestGamesReviews()
        {
            return _context.Items
                .Include(i => i.SelectedCategory)
                .Include(i => i.Reviews)
                .Where(i => i.SelectedCategory.First().CategoryId == 5)
                .OrderByDescending(i => i.ItemSubmitDate)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Item> GetLatestJournals()
        {
            return _context.SelectedCategories
                .Include(p => p.Items)
                .ThenInclude(p => p.Reviews)
                .Include(p => p.Categories)
                .Where(p => p.CategoryId == 8)
                .Select(p => p.Items)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Item> GetLatestPosts()
        {
            return _context.Items
                .Include(i => i.SelectedCategory)
                .ThenInclude(i => i.Categories)
                .Include(i => i.Reviews)
                .OrderByDescending(i => i.ItemSubmitDate)
                .Take(8)
                .ToList();
        }

        public IEnumerable<Item> GetLatestRecommended()
        {
            return _context.SelectedCategories
                .Include(p => p.Items)
                .Include(p => p.Categories)
                .Where(p => p.CategoryId == 9)
                .Select(p => p.Items)
                .Take(6)
                .ToList();
        }

        public IEnumerable<Item> GetLatestTechNews()
        {
            return _context.Items
                .Include(i => i.SelectedCategory)
                .Include(i => i.Reviews)
                .Where(i => i.SelectedCategory.First().CategoryId == 3)
                .OrderByDescending(i => i.ItemSubmitDate)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Item> GetMostCommentedPosts()
        {
            return _context.Items
                .Include(i => i.Reviews)
                .OrderByDescending(i => i.Reviews.Count)
                .Take(4)
                .ToList();
        }

        public IEnumerable<Item> GetMostSeenPostThisMonth()
        {
            return _context.Items
                .Include(i => i.Reviews)
                .Where(i => i.ItemSubmitDate.Month == DateTime.Now.Month)
                .OrderByDescending(i => i.LikeCount)
                .Take(3)
                .ToList();
        }

        public IEnumerable<ShowItemsInCategory> GetPostByCategoryId(int categoryId)
        {
            List<ShowItemsInCategory> posts = new List<ShowItemsInCategory>();
            foreach (var post in _context.SelectedCategories
                         .Include(s=>s.Items)
                         .ThenInclude(s=>s.Reviews)
                         .Where(s=>s.CategoryId==categoryId)
                         .Select(s=>s.Items))
            {
                posts.Add(new ShowItemsInCategory()
                {
                    ItemTitle = post.ItemTitle,
                    CategoryId = categoryId,
                    ImageTitle = post.ItemImage,
                    ItemDescription = post.ItemDescription,
                    PublishDate = post.ItemSubmitDate,
                    ReviewsCount = post.Reviews.Count,
                    ItemId = post.ItemId,
                    ShortDescription = post.ShortDescription
                });
            }

            return posts;
        }

        public IEnumerable<ShowPostInSearch> GetPostForSearch(string phrase)
        {
            List<ShowPostInSearch> posts = new List<ShowPostInSearch>();
            foreach (var post in _context.Items
                         .Include(i=>i.Reviews)
                         .Where(i=>i.ItemTitle.Contains(phrase)||i.ItemDescription.Contains(phrase)||i.Tags.Select(i=>i.TagTitle).Contains(phrase)))
            {
                posts.Add(new ShowPostInSearch()
                {
                    ItemTitle = post.ItemTitle,
                    ImageTitle = post.ItemImage,
                    ItemDescription = post.ItemDescription,
                    PublishDate = post.ItemSubmitDate,
                    ReviewsCount = post.Reviews.Count,
                    ItemId = post.ItemId,
                    ShortDescription = post.ShortDescription
                });
            }

            return posts;
        }
        public void LikePost(int itemId)
        {
            var post = _context.Items.Find(itemId);
            post.LikeCount += 1;
            _context.SaveChanges();
        }
    }
}
