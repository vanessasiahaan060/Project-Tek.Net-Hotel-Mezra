﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspnetIdentityRoleBasedTutorial.Data;

namespace ModelClasses
{
    public class PImages
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Kamar kamar { get; set; }

        public string ImageUrl { get; set; }

        // Misalnya, ubah properti ini menjadi ProductName atau sesuai kebutuhan
        public string ProductName { get; set; }
    }
}
