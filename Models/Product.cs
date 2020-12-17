using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCategories.Models
{
    public class Product
    {
        [Key]

            public int ProductId { get; set; }

            [Required(ErrorMessage="Product Name is required")]
            public string ProductName {get;set;}

            [Required(ErrorMessage="Description is required")]
            public string Description {get;set;}

            [Required(ErrorMessage="Price is required")]
            public int Price {get;set;}

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            public List<Association> Associations { get; set; }
    }
}