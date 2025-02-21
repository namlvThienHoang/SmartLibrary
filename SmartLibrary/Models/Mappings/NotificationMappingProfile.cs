using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Notification;

namespace SmartLibrary.Models.Mappings
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationViewModel>();
            CreateMap<CreateNotificationViewModel, Notification>();
            CreateMap<EditNotificationViewModel, Notification>();
            CreateMap<Notification, EditNotificationViewModel>();
        }
    }
}