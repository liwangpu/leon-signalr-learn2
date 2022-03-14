using AutoMapper;
using IDS.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace IDS.API.Application.Queries.Identities
{
    public class UserProfileQuery : IRequest<UserProfileQueryDTO>
    {

    }


    public class UserProfileQueryDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }


    public class UserProfileQueryProfile : Profile
    {
        public UserProfileQueryProfile()
        {
            CreateMap<Identity, UserProfileQueryDTO>()
             .ForAllOtherMembers(_ => _.Condition((source, destination, sValue, dValue, context) =>
             {
                 if (sValue == null)
                 {
                     return false;
                 }

                 return true;
             }));
        }
    }
}
