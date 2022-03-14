using AutoMapper;
using Base.Domain.Common;
using IDS.Domain.AggregateModels.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IDS.API.Application.Queries.Identities
{
    public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, UserProfileQueryDTO>
    {
        private readonly IProfileContext profileContext;
        private readonly IIdentityRepository identityRepository;
        private readonly IMapper mapper;

        public UserProfileQueryHandler(IProfileContext profileContext, IIdentityRepository identityRepository, IMapper mapper)
        {
            this.profileContext = profileContext;
            this.identityRepository = identityRepository;
            this.mapper = mapper;
        }

        public async Task<UserProfileQueryDTO> Handle(UserProfileQuery request, CancellationToken cancellationToken)
        {
            var identity = await identityRepository.FindAsync(profileContext.IdentityId);
            if (identity == null) return null;
            var profile = mapper.Map<UserProfileQueryDTO>(identity);

            return profile;
        }
    }
}
