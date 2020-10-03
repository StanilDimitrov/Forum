﻿using AutoMapper;
using Forum.Application.Common.Exceptions;
using Forum.Application.PublicUsers.Messages.Queries.Common;
using Forum.Application.PublicUsers.Users;
using Forum.Application.PublicUsers.Users.Queries.Common;
using Forum.Application.PublicUsers.Users.Queries.Details;
using Forum.Application.PublicUsers.Users.Queries.Posts;
using Forum.Doman.PublicUsers.Exceptions;
using Forum.Doman.PublicUsers.Models.Users;
using Forum.Doman.PublicUsers.Repositories;
using Forum.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.PublicUsers.Repositories
{
    internal class PublicUserRepository : DataRepository<IPublicUsersDbContext, PublicUser>,
        IPublicUserDomainRepository,
        IPublicUserQueryRepository
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
                    .Any(p => p.Id == postId), cancellationToken);

        public async Task<bool> HasMessage(
            int publicUserId,
            int messageId,
            CancellationToken cancellationToken = default)
          => await this
               .All()
               .Where(pu => pu.Id == publicUserId)
               .AnyAsync(pu => pu.InboxMessages
                   .Any(m => m.Id == messageId), cancellationToken);

        public async Task<IEnumerable<MessageOutputModel>> GetInboxMessages(
            int id, 
            int skip,
            int take, 
            CancellationToken cancellationToken = default)
        {
            var publicUser = await Find(id, cancellationToken);
            if (publicUser == null)
            {
                throw new NotFoundException(nameof(PublicUser), id);
            }

            var messages = publicUser.InboxMessages
                 .Skip(skip)
                 .Take(take)
                 .OrderByDescending(m => m.CreatedOn)
                 .ToList();

            var messagesOutputModel = mapper.Map<IEnumerable<MessageOutputModel>>(messages);
            return messagesOutputModel;
        }

        public async Task<IEnumerable<GetPublicUserPostOutputModel>> GetPosts(
           int id,
           int skip,
           int take,
           CancellationToken cancellationToken = default)
        {
            var publicUser = await Find(id, cancellationToken);
            if (publicUser == null)
            {
                throw new NotFoundException(nameof(PublicUser), id);
            }
            var posts = publicUser.Posts
                 .Skip(skip)
                 .Take(take)
                 .OrderByDescending(m => m.CreatedOn)
                 .ToList();

            return mapper.Map<IEnumerable<GetPublicUserPostOutputModel>>(posts);
        }

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
