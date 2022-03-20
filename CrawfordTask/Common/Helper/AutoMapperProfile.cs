using AutoMapper;
using CrawfordTask.Common.Entities;
using CrawfordTask.Common.Models.Users;
using CrawfordTask.Common.Models.LossTypes;

namespace CrawfordTask.Common.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserModel>();
            CreateMap<LossTypes, LossTypeModel>();
        }
    }
}