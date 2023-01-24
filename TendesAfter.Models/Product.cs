﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TendesAfter.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ValidateNever]

        public string ImageUrl { get; set; }

        public bool Bought { get; set; } = false;
        [Required]
        [Display(Name = "Category")]


        public int CategoryId { get; set; }
        [Required]
        [ValidateNever]

        public Category Category { get; set; }
        [Required]
        [ValidateNever]

        public CoverType CoverType { get; set; }
        [Required]
        [Display(Name ="Console")]
		
		public int CoverTypeID { get; set; }
        [Required]
        [Display(Name = "Produtora")]

        public int ProducerId { get; set; }
        [Required]
        [ValidateNever]

        public Producer Producer { get; set; }
      

        //      [Required]
        //[ValidateNever]
        //public Producer Producer { get; set; }

        //[Required]
        //[Display(Name = "Producer")]

        //public int ProducerID { get; set; }

    }
}
