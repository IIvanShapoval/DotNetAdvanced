using MediatR;

namespace Catalog.Application.Features.Products.Queries.GetProducts
{
    public class GetProductQuery : IRequest<List<ProductVm>>
    {
        public Guid Id { get; set; }
    }
}
