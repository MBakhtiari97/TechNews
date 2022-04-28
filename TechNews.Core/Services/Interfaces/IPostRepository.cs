using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Item> GetLatestGamesReviews();
        IEnumerable<Item> GetMostCommentedPosts();
        IEnumerable<Item> GetMostSeenPostThisMonth();
        IEnumerable<Item> GetLatestJournals();
        IEnumerable<Item> GetLatestRecommended();
        IEnumerable<Item> GetLatestPosts();
        IEnumerable<Item> GetLatestTechNews();
        IEnumerable<ShowItemsInCategory> GetPostByCategoryId(int categoryId);
        IEnumerable<ShowPostInSearch> GetPostForSearch(string phrase);
        void LikePost(int itemId);

        Item GetItemByItemId(int itemId);
    }
}
