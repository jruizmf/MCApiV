using DataAccessLayer.Models.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    [Table("Motorcycles")]
    public class Motorcycle : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debería tener entre 3 y 50 caracteres.")]
        public string Trademark { get; set; }
        [Display(Name = "Linea")]
        [StringLength(50, ErrorMessage = "La linea debería menos de 50 caracteres.")]
        public string Line { get; set; }

        [Display(Name = "Tags")]
        [StringLength(4, MinimumLength = 1)]
        public string Model { get; set; }

        [Display(Name = "Placas")]
        public string Plate { get; set; }

        [Display(Name = "Numero de Serie")]
        public string SerialNumber { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }

        public User User { get; set; }
        public ICollection<MotorcycleImage> MotorcycleImages { get; set; }
    }
}
