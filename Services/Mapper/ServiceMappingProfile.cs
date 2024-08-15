using AutoMapper;
using Domain;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Models.Response;

namespace Services.Mapper;

public class ServiceMappingProfile : Profile
{
    public ServiceMappingProfile()
    {
        // Request models -> Domain models
        CreateMap<CreateOrderModel, Order>()
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        
        CreateMap<UpdateOrderModel, Order>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes))
            .ForMember(d => d.ClientId, map => map.Ignore())
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());
        
        
        CreateMap<DeleteOrderModel, Order>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.Ignore())
            .ForMember(d => d.DateStart, map => map.Ignore())
            .ForMember(d => d.DateEnd, map => map.Ignore())
            .ForMember(d => d.HubStartId, map => map.Ignore())
            .ForMember(d => d.HubEndId, map => map.Ignore())
            .ForMember(d => d.Price, map => map.Ignore())
            .ForMember(d => d.Containers, map => map.Ignore())
            .ForMember(d => d.DownTimes, map => map.Ignore())
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        
        CreateMap<GetOrderByIdModel, Order>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.Ignore())
            .ForMember(d => d.DateStart, map => map.Ignore())
            .ForMember(d => d.DateEnd, map => map.Ignore())
            .ForMember(d => d.HubStartId, map => map.Ignore())
            .ForMember(d => d.HubEndId, map => map.Ignore())
            .ForMember(d => d.Price, map => map.Ignore())
            .ForMember(d => d.Containers, map => map.Ignore())
            .ForMember(d => d.DownTimes, map => map.Ignore())
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        
        CreateMap<GetOrdersByClientIdModel, Order>()
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.Id, map => map.Ignore())
            .ForMember(d => d.DateStart, map => map.Ignore())
            .ForMember(d => d.DateEnd, map => map.Ignore())
            .ForMember(d => d.HubStartId, map => map.Ignore())
            .ForMember(d => d.HubEndId, map => map.Ignore())
            .ForMember(d => d.Price, map => map.Ignore())
            .ForMember(d => d.Containers, map => map.Ignore())
            .ForMember(d => d.DownTimes, map => map.Ignore())
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());

        
        CreateMap<OrderModel, Order>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes))
            .ForMember(d => d.Costs, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());


        CreateMap<ContainerModel, Container>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore());

        
        CreateMap<DownTimeModel, DownTime>()
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubId, map => map.MapFrom(c => c.HubId))
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.OrderId, map => map.Ignore())
            .ForMember(d => d.IsDeleted, map => map.Ignore());
        
        
        
        // Response models -> Response models
        CreateMap<Order, OrderModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));

        
        CreateMap<Order, OrderFullModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Costs, map => map.MapFrom(c => c.Costs))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes))
            .ForMember(d => d.IsDeleted, map => map.MapFrom(c => c.IsDeleted));
        

        CreateMap<Container, ContainerModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));

        
        CreateMap<DownTime, DownTimeModel>()
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubId, map => map.MapFrom(c => c.HubId));
    }
}