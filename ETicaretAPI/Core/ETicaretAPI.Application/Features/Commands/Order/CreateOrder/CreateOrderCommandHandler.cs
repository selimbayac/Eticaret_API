using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest,
      CreateOrderCommandResponse>
    {
        readonly IOrderService _orderSerivce;
        readonly IBasketService _basketSerivce;
        readonly IOrderHubService _orderHubSerivce;
        public CreateOrderCommandHandler(IOrderService orderSerivce, IBasketService basketSerivce, IOrderHubService orderHubSerivce)
        {
            _orderSerivce = orderSerivce;
            _basketSerivce = basketSerivce;
            _orderHubSerivce = orderHubSerivce;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
           await  _orderSerivce.CreateOrderAsync(new()
            {
                Address = request.Address,
                Description = request.Description,
                BasketId = _basketSerivce.GetUserActiveBasket?.Id.ToString()

            });
            _orderHubSerivce.OrderAddedMessageAsync("Heyy Yeni bir sipariş geldi");
            return new();
        }
    }
}
