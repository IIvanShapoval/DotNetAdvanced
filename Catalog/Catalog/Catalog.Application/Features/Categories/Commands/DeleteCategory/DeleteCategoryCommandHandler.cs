using AutoMapper;
using Catalog.Application.Contracts.Persistance;
using Catalog.Application.Exceptions;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Categories.Commands.DeleteCategory
{
    internal class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(request.Id);

            if (categoryToDelete == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            await _categoryRepository.DeleteAsync(categoryToDelete);
        }
    }
}
