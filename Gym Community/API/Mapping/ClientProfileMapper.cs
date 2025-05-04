using AutoMapper;
using Gym_Community.API.DTOs;
using Gym_Community.API.DTOs.Client;
using Gym_Community.Domain.Models;
using Gym_Community.Domain.Models.ClientStuff;
using Gym_Community.Domain.Models.Notify;

namespace Gym_Community.API.Mapping
{
    public class ClientProfileMapper: Profile
    {
        public ClientProfileMapper()
        {
            CreateMap<ClientInfo, ClientProfileDTO>()
     .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ClientUser.FirstName))
     .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ClientUser.LastName))
     .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ClientUser.Address))
     .ForMember(dest => dest.ProfileImg, opt => opt.MapFrom(src => src.ClientUser.ProfileImg))
     .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.ClientUser.CreatedAt))
     .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.ClientUser.BirthDate))
     .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.ClientUser.IsActive))
     .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => src.ClientUser.IsPremium))
     .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.ClientUser.Gender))
     .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientUser.Id));




            CreateMap<AppUser, ClientProfileDTO>();

            CreateMap<ClientInfo, ClientProfileDTO>();

            // Map NotificationDto to Notification
            CreateMap<NotificationDto, Notification>()
                .ForMember(dest => dest.isRead, opt => opt.MapFrom(src => src.isRead ?? false)) // Default to false if null
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt ?? DateTime.UtcNow)) // Default to current UTC time if null
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)); // Map UserId directly




            CreateMap<UpdateClientProfileDTO, ClientInfo>();
            CreateMap<UpdateClientProfileDTO, AppUser>();
            
        }
    }

}
