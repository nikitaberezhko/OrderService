using Asp.Versioning;
using AutoMapper;
using CommonModel.Contracts;
using Microsoft.AspNetCore.Mvc;
using OrderService.Contracts.ApiModels;
using OrderService.Contracts.Request;
using OrderService.Contracts.Response;
using Serilog;
using Serilog.Events;
using SerilogTracing;
using Services.Models.Request;
using Services.Services.Interfaces;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер заказов
/// </summary>
[ApiController]
[Route("api/v{v:apiVersion}/orders")]
[ApiVersion(1)]
public class OrderController(
    IMapper mapper,
    IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CommonResponse<GetAllOrdersResponse>>> GetAll(
        [FromQuery] GetAllOrdersRequest request)
    {
        var orders = await orderService.GetAll(
            new GetAllOrdersModel { Page = request.Page, PageSize = request.PageSize });
        var response = new CommonResponse<GetAllOrdersResponse>
        {
            Data = new GetAllOrdersResponse
                { Orders = mapper.Map<List<OrderApiModel>>(orders) }
        };

        return response;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommonResponse<GetOrderByIdResponse>>> GetById(
        [FromRoute] GetOrderByIdRequest request)
    {
        var order = await orderService.GetById(mapper.Map<GetOrderByIdModel>(request));
        var response = new CommonResponse<GetOrderByIdResponse> 
            { Data = mapper.Map<GetOrderByIdResponse>(order) };
        
        return response;
    }
    
    [HttpGet("clients/{clientId:guid}")]
    public async Task<ActionResult<CommonResponse<GetOrdersByClientIdResponse>>> GetByClientId(
        [FromRoute] GetOrdersByClientIdRequest request)
    {
        var orders = await orderService.GetByClientId(mapper.Map<GetOrdersByClientIdModel>(request));
        var response = new CommonResponse<GetOrdersByClientIdResponse>
            { Data = new GetOrdersByClientIdResponse { Orders = mapper.Map<List<OrderApiModel>>(orders) } };
        
        return response;
    }
    
    [HttpGet("periods")]
    public async Task<ActionResult<CommonResponse<GetOrdersInPeriodResponse>>> GetByPeriod(
        [FromQuery] GetOrdersInPeriodRequest request)
    {
        var orders = await orderService.GetByPeriod(
            mapper.Map<GetOrdersInPeriodModel>(request));
        var response = new CommonResponse<GetOrdersInPeriodResponse>
        {
            Data = new GetOrdersInPeriodResponse
                { Orders = mapper.Map<List<OrderApiFullModel>>(orders) }
        };
        
        return response;
    }

    [HttpPost]
    public async Task<ActionResult<CommonResponse<CreateOrderResponse>>> Create(
        CreateOrderRequest request)
    {
        var id = await orderService.Create(mapper.Map<CreateOrderModel>(request));
        var response = new CreatedResult(
            nameof(Create),
            new CommonResponse<CreateOrderResponse>
                { Data = new CreateOrderResponse { Id = id } }
        );
        
        return response;
    }

    [HttpPut]
    public async Task<ActionResult<CommonResponse<UpdateOrderResponse>>> Update(
        UpdateOrderRequest request)
    {
        var order = await orderService.Update(mapper.Map<UpdateOrderModel>(request));
        var response = new CommonResponse<UpdateOrderResponse> 
            { Data = mapper.Map<UpdateOrderResponse>(order) };
        
        return response;
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<CommonResponse<DeleteOrderResponse>>> Delete(
        [FromRoute] DeleteOrderRequest request)
    {
        var order = await orderService.Delete(mapper.Map<DeleteOrderModel>(request));
        var response = new CommonResponse<DeleteOrderResponse> 
            { Data = mapper.Map<DeleteOrderResponse>(order) };
        
        return response;
    }
}