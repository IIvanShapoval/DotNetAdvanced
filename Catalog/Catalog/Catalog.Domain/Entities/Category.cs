using Catalog.Domain.Common;

namespace Catalog.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public Uri? Image { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
