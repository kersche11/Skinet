using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper ;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>>CreateOrder(OrderDto orderDto)
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.Email)?.Value;
            var email = HttpContext.User.RetriveEmailFromPrincipal();
            var address= _mapper.Map<AddressDto,Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email,orderDto.DeliveryMethodId,orderDto.BasketId,address);

            if(order==null) return BadRequest(new ApiResponse(400, "Problem creating Order"));

            return Ok(order);
        }

    }
}