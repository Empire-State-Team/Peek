namespace Peek.Web.ViewModels.Products
{
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class ProductPreviewViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagesFolderId { get; set; }

        public string ImageUrl { get; set; }
    }
}
