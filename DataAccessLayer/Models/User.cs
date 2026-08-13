using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DataAccessLayer.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required]

        [Display(Name = "Usuario")]
        [StringLength(80, ErrorMessage = "El Email debería menos de 50 caracteres.")]
        public string UserName { get; set; }
        [Required]
        public int Status { get; set; }
        public byte[] Password { get; set; }


        [StringLength(50)]
        public string FacebookAuth { get; set; }
        [StringLength(50)]
        public string GoogleAuth { get; set; }

        public UserProfile UserProfile { get; protected set; }
    }
}
