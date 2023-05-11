using AutoMapper;
using Catalog.Application.Features.Categories.Commands.DeleteCategory;
using Catalog.Application.Features.Categories.Commands.UpdateCategory;
using Catalog.Domain.Entities;

namespace Catalog.Application.Mappings
{
    public class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<DeleteCategoryCommand, Category>();
        }
    }
}
