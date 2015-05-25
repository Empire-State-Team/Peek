namespace Peek.Web.Areas.Administration.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Peek.Models;
    using Peek.Web.Infrastructure.Mapping;

    public class ProductInputModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Product name")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0.1, 100000)]
        public decimal Price { get; set; }

        [Required]
        public bool InStock { get; set; }

        public IEnumerable<HttpPostedFileBase> Images { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
    }
}
