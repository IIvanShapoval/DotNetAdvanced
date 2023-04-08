using Catalog.Application.Responses;

namespace Catalog.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryDto Category { get; set; } = default!;
    }
}
