using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record FetchProductDataRequest(Guid RequestId, string ProductId);

    public record FetchProductDataResponse(Guid RequestId, OrderItem retrievedProductItem);

}
