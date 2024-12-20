using AutoMapper;
using XRS_API.Models.ViewModels;

namespace XRS_API.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelWithOutListsInfo>();


            CreateMap<Room, Room_withOutOverData>();

            CreateMap<Guest, Guest_withOutOverData>();

            

           
        }
    }
}
