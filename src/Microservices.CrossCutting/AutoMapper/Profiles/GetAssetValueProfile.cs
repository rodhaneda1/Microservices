using AutoMapper;
using Microservices.Application.Querys;

namespace Microservices.CrossCutting.AutoMapper.Profiles
{
    public class GetAssetValueProfile : Profile
    {
        public GetAssetValueProfile()
        {
            CreateMap<GetAssetValueRequest, GetAssetValueResponse>(MemberList.None)
                .ReverseMap();
        }
    }
}
