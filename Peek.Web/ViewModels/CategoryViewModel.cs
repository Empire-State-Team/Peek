namespace Peek.Web.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Category")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }
    }
}
