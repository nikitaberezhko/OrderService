using AutoMapper;
using OrderService.Contracts.ApiModels;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Models.Response;

namespace WebApi.Mapper;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        // Requests -> Request models
        CreateMap<CreateOrderRequest, CreateOrderModel>()
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));


        CreateMap<UpdateOrderRequest, UpdateOrderModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));
        
        
        CreateMap<DeleteOrderRequest, DeleteOrderModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));

        
        CreateMap<GetAllOrdersRequest, GetAllOrdersModel>()
            .ForMember(d => d.Page, map => map.MapFrom(c => c.Page))
            .ForMember(d => d.PageSize, map => map.MapFrom(c => c.PageSize));

        
        CreateMap<GetOrderByIdRequest, GetOrderByIdModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));

        
        CreateMap<GetOrdersByClientIdRequest, GetOrdersByClientIdModel>()
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId));
        
        CreateMap<GetOrdersInPeriodRequest, GetOrdersInPeriodModel>()
            .ForMember(d => d.End, map => map.MapFrom(c => c.End))
            .ForMember(d => d.Period, map => map.MapFrom(c => c.Period));

        
        CreateMap<OrderApiModel, OrderModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));


        CreateMap<ContainerApiModel, ContainerModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        CreateMap<DownTimeApiModel, DownTimeModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.HubId, map => map.MapFrom(c => c.HubId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd));
        

        // Response models -> Responses
        CreateMap<OrderModel, DeleteOrderResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));

        
        CreateMap<OrderModel, GetOrderByIdResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));

        
        CreateMap<OrderModel, UpdateOrderResponse>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));
        
        
        CreateMap<OrderModel, OrderApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.ClientId, map => map.MapFrom(c => c.ClientId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd))
            .ForMember(d => d.HubStartId, map => map.MapFrom(c => c.HubStartId))
            .ForMember(d => d.HubEndId, map => map.MapFrom(c => c.HubEndId))
            .ForMember(d => d.Price, map => map.MapFrom(c => c.Price))
            .ForMember(d => d.Containers, map => map.MapFrom(c => c.Containers))
            .ForMember(d => d.DownTimes, map => map.MapFrom(c => c.DownTimes));
        
        
        CreateMap<OrderFullModel, OrderApiFullModel>()
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


        CreateMap<ContainerModel, ContainerApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id));
        
        
        CreateMap<DownTimeModel, DownTimeApiModel>()
            .ForMember(d => d.Id, map => map.MapFrom(c => c.Id))
            .ForMember(d => d.HubId, map => map.MapFrom(c => c.HubId))
            .ForMember(d => d.DateStart, map => map.MapFrom(c => c.DateStart))
            .ForMember(d => d.DateEnd, map => map.MapFrom(c => c.DateEnd));
    }
}