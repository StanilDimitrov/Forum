namespace Forum.Application.PublicUsers.Posts.Queries.Common
{
    using System.Collections.Generic;

    public abstract class PostsOutputModel<TPostOutputModel>
    {
        internal PostsOutputModel(
            IEnumerable<TPostOutputModel> posts, 
            int page, 
            int totalPages)
        {
            this.Posts = posts;
            this.Page = page;
            this.TotalPages = totalPages;
        }

        public IEnumerable<TPostOutputModel> Posts { get; }

        public int Page { get; }

        public int TotalPages { get; }
    }
}
