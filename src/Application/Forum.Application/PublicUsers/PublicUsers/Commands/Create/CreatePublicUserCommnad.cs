using Forum.Application.Common.Contracts;
using Forum.Application.PublicUsers.Users;
using Forum.Domain.PublicUsers.Factories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Application.PublicUsers.PublicUsers.Commands.Create
{
    public class CreatePublicUserCommnad : IRequest<CreatePublicUserOutputModel>
    {
        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public class CreatePublicUserCommnadHandler : IRequestHandler<CreatePublicUserCommnad, CreatePublicUserOutputModel>
        {
            private readonly ICurrentUser currentUser;
            private readonly IPublicUserFactory publicUserFactory;
            private readonly IPublicUserRepository publicUserRepository;

            public CreatePublicUserCommnadHandler(
                ICurrentUser currentUser,
                IPublicUserFactory publicUserFactory,
                IPublicUserRepository publicUserRepository)
            {
                this.currentUser = currentUser;
                this.publicUserFactory = publicUserFactory;
                this.publicUserRepository = publicUserRepository;
            }

            public async Task<CreatePublicUserOutputModel> Handle(
                CreatePublicUserCommnad request,
                CancellationToken cancellationToken)
            {
                var publicUser = this.publicUserFactory
                    .WithUserName(request.UserName)
                    .WithEmail(request.Email)
                    .FromUser(this.currentUser.UserId)
                    .Build();

                await this.publicUserRepository.Save(publicUser, cancellationToken);

                return new CreatePublicUserOutputModel(publicUser.Id);
            }
        }
    }
}
