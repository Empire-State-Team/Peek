namespace Peek.Web.ViewModels
{
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
