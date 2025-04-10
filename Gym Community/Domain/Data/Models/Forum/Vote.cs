using Gym_Community.Data;
using System.Threading;

namespace Gym_Community.Domain.Data.Models.Forum
{
    public class Vote
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public int? ThreadId { get; set; } // Nullable if voting on replies too
        public Thread Thread { get; set; }
        public int? CommentId { get; set; }  // Nullable if voting on threads too
        public Comment Comment { get; set; }
        public bool IsUpvote { get; set; }


    }

    // Rank Posts According to upvotes

    //public async Task<List<Post>> GetTopPostsAsync(int topN)
    //    {
    //        var rankedPosts = await Post
    //            .Select(t => new
    //            {
    //                Post = t,
    //                NetVotes = t.Votes.Sum(v => v.IsUpvote ? 1 : -1)
    //            })
    //            .OrderByDescending(t => t.NetVotes)
    //            .Take(topN)
    //            .Select(t => t.Thread)
    //            .ToListAsync();

    //        return rankedPosts;
    //    }


}
