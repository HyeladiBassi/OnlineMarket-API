using AutoMapper;
using OnlineMarket.DataTransferObjects.Authentication;
using OnlineMarket.Models;

namespace OnlineMarket.API.MappingProfiles
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<BuyerSignUpDto, SystemUser>();
        }
    }
}