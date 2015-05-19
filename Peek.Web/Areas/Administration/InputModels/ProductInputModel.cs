using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Peek.Web.Areas.Administration.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [DisplayName("Product name")]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
    }
}
