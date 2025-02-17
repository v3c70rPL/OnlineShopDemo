using MassTransit;
using Shared;

namespace ProductsService.Consumers
{
    public class FetchProductDataConsumer : IConsumer<FetchProductDataRequest>
    {
        public async Task Consume(ConsumeContext<FetchProductDataRequest> context)
        {
            Console.WriteLine($"Processing request: {context.Message.ProductId}");

            // Fetch specific document from Mongo DB - access to database should be implemented and hidden behind abstraction layer
            var OrderItem = new OrderItem();

            // Send response back
            await context.RespondAsync(new FetchProductDataResponse(context.Message.RequestId, OrderItem));
        }
    }
}
