using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrdersService.Models;
using Shared;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {

        // TODO: Move data operation related logic into separate class - repository to hide internal workings of data handling
        private readonly IRequestClient<FetchProductDataRequest> _requestClient;

        public OrderController(IRequestClient<FetchProductDataRequest> requestClient)
        {
            _requestClient = requestClient;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            // Fetch specific order based on provided it

            // For each order fetch products that were added to shopping card
            var requestId = Guid.NewGuid();
            var request = new FetchProductDataRequest(requestId, "productIdFromMongoDb");

            // Send request and await response
            var response = await _requestClient.GetResponse<FetchProductDataResponse>(request);

            
            return Ok();

        }
    }
}
