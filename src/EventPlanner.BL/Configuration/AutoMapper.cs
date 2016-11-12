using AutoMapper;
using EventPlanner.BL.DTO;
using EventPlanner.DAL.Entities;

namespace EventPlanner.BL.Configuration
{
    public static class AutoMapper
    {
        public static void Init()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<User, UserDTO>();

                config.CreateMap<Event, EventDTO>();
                config.CreateMap<EventCreateDTO, Event>();

                config.CreateMap<Place, PlaceDTO>().ReverseMap();

                config.CreateMap<UserEventDTO, UserEvent>().ReverseMap();
            });
        }
    }
}
