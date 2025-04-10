using Gym_Community.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace Gym_Community.Domain.Models.Forum
{
    public class Vote
    {
        public int Id { get; set; }
        
        
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        
        
        [ForeignKey("Post")]
        public int? PostId { get; set; } // Nullable if voting on replies too
        public Post Post { get; set; }



        [ForeignKey("Comment")]
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
