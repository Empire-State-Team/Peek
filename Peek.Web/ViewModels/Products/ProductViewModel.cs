namespace Peek.Web.ViewModels.Products
{
    using AutoMapper;
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class ProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Product, ProductViewModel>()
                .ForMember(p => p.CategoryName, options => options.MapFrom(p => p.Category.Name));
        }
    }
}
