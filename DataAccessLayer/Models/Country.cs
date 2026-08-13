using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.Models.Common;

namespace DataAccessLayer.Models
{
    public class Country : BaseEntity
    {
        [Required]

        [Display(Name = "País")]
        [StringLength(80, ErrorMessage = "El nombre debería menos de 80 caracteres.")]
        public string Description { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
