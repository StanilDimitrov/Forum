using AutoMapper;
using Forum.Application.PublicUsers.Users;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Details;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.PublicUsers.Repositories
{
    internal class PublicUserRepository : DataRepository<IPublicUsersDbContext, PublicUser>, IPublicUserRepository
    {
        private readonly IMapper mapper;

        public PublicUserRepository(IPublicUsersDbContext dbContext, IMapper mapper)
        : base(dbContext)
        => this.mapper = mapper;

        public async Task<PublicUser> Find(int id, CancellationToken cancellationToken = default)
       => await this
               .All()
               .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
       
        public async Task<PublicUserDetailsOutputModel> GetDetails(int id, CancellationToken cancellationToken = default)
          => await this.mapper
                .ProjectTo<PublicUserDetailsOutputModel>(this
                    .All()
                    .Where(pu => pu.Id == id))
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<PublicUserOutputModel> GetDetailsByPostId(int postId, CancellationToken cancellationToken = default)
         => await this.mapper
                .ProjectTo<PublicUserDetailsOutputModel>(this
                    .All()
                    .Where(pu => pu.Posts.Any(c => c.Id == postId)))
                .SingleOrDefaultAsync(cancellationToken);

        public async Task<Message> GetMessage(int messageId, CancellationToken cancellationToken = default)
      => await this
               .Data
               .Messages
               .FirstOrDefaultAsync(c => c.Id == messageId, cancellationToken);

        public async Task<bool> HasPost(
            int publicUserId,
            int postId,
            CancellationToken cancellationToken = default)
           => await this
                .All()
                .Where(pu => pu.Id == publicUserId)
                .AnyAsync(pu => pu.Posts
                    .Any(pu => pu.Id == postId), cancellationToken);

        public async Task<bool> HasMessage(
            int publicUserId,
            int messageId,
            CancellationToken cancellationToken = default)
          => await this
               .All()
               .Where(pu => pu.Id == publicUserId)
               .AnyAsync(pu => pu.InboxMessages
                   .Any(pu => pu.Id == messageId), cancellationToken);

        public async Task<PublicUser> FindByCurrentUser(string userId, CancellationToken cancellationToken = default)
       => await this.FindByUser(userId, publicUser => publicUser, cancellationToken);

        public async Task<int> GetPublicUserId(string userId, CancellationToken cancellationToken = default)
        => await this.FindByUser(userId, publicUser => publicUser.Id, cancellationToken);

        private async Task<T> FindByUser<T>(
           string userId,
           Expression<Func<PublicUser, T>> selector,
           CancellationToken cancellationToken = default)
        {
            var publicUser = await this
                .All()
                .Where(pu => pu.UserId == userId)
                .Select(selector)
                .SingleOrDefaultAsync(cancellationToken);

            if (publicUser == null)
            {
                throw new InvalidPublicUserException("This user is not a public user.");
            }

            return publicUser;
        }
    }
}
