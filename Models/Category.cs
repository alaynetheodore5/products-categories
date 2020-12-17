using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsCategories.Models
{
    public class Category
    {
        [Key]

            public int CategoryId { get; set; }

            [Required(ErrorMessage="Category Name is required")]
            public string CategoryName {get;set;}

            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            public List<Association> Associations { get; set; }
    }
}